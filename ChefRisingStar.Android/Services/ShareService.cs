using Android.App;
using Android.Content;
using ChefRisingStar.Droid.Services;
using ChefRisingStar.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Linq;

[assembly: Dependency(typeof(ShareService))]
namespace ChefRisingStar.Droid.Services
{
    internal class ShareService : Activity, IShare
    {
        public async Task Share(string text, string imagePath, bool fromWeb)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);

            intent.PutExtra(Intent.ExtraText, text);

            intent.SetType("image/*");
            intent.SetFlags(ActivityFlags.GrantReadUriPermission);

            Java.IO.File file;
            if (fromWeb)
            {
                string absPath = Path.Combine(FileSystem.CacheDirectory, imagePath.Split('/').Last());
                var bytes = await GetBytes(imagePath);
                await File.WriteAllBytesAsync(absPath, bytes);
                file = new Java.IO.File(absPath);
            }
            else
            {
                file = new Java.IO.File(imagePath);
            }
            
            var context = Android.App.Application.Context;
            var packageName = Android.App.Application.Context.PackageName;

            intent.PutExtra(Intent.ExtraStream, FileProvider.GetUriForFile(context, packageName + ".provider", file));

            intent.PutExtra(Intent.ExtraTitle, "Share Image");

            var chooserIntent = Intent.CreateChooser(intent, "Share Image" ?? string.Empty);

            var flags = ActivityFlags.ClearTop | ActivityFlags.NewTask;

            chooserIntent.SetFlags(flags);

            Platform.AppContext.StartActivity(chooserIntent);
        }

        public async Task<byte[]> GetBytes(string url)
        {
            byte[] byteArray;
            using (var webClient = new WebClient())
            {
                byteArray = await webClient.DownloadDataTaskAsync(url);
            }
            return byteArray;
        }
    }
}