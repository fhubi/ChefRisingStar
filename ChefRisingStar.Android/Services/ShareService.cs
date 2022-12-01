using Android.App;
using Android.Content;
using ChefRisingStar.Droid.Services;
using ChefRisingStar.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using System.Net;
using System.Linq;
using Android.Graphics.Drawables;

[assembly: Dependency(typeof(ShareService))]
namespace ChefRisingStar.Droid.Services
{
    internal class ShareService : Activity, IShare
    {
        public async Task Share(string text, string imagePath, string title, ShareType shareType)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);

            intent.PutExtra(Intent.ExtraText, text);

            intent.SetType("image/*");
            intent.SetFlags(ActivityFlags.GrantReadUriPermission);

            Java.IO.File file = null;

            switch (shareType)
            {
                case ShareType.Resource:
                    string path = Path.Combine(FileSystem.CacheDirectory, imagePath);
                    var drawableBytes = GetDrawableBytes(imagePath);
                    await File.WriteAllBytesAsync(path, drawableBytes);
                    file = new Java.IO.File(path);
                    break;
                case ShareType.Web:
                    string absPath = Path.Combine(FileSystem.CacheDirectory, imagePath.Split('/').Last());
                    var bytes = await GetWebImageBytes(imagePath);
                    await File.WriteAllBytesAsync(absPath, bytes);
                    file = new Java.IO.File(absPath);
                    break;
                case ShareType.Storage:
                    file = new Java.IO.File(imagePath);
                    break;
            }

            var context = Android.App.Application.Context;
            var packageName = Android.App.Application.Context.PackageName;

            intent.PutExtra(Intent.ExtraStream, FileProvider.GetUriForFile(context, packageName + ".provider", file));

            intent.PutExtra(Intent.ExtraTitle, title);

            var chooserIntent = Intent.CreateChooser(intent, title);

            var flags = ActivityFlags.ClearTop | ActivityFlags.NewTask;

            chooserIntent.SetFlags(flags);

            Platform.AppContext.StartActivity(chooserIntent);
        }

        private async Task<byte[]> GetWebImageBytes(string url)
        {
            byte[] byteArray;
            using (var webClient = new WebClient())
            {
                byteArray = await webClient.DownloadDataTaskAsync(url);
            }
            return byteArray;
        }

        private byte[] GetDrawableBytes(string drawableImage)
        {
            try
            {
                int drawableId = Platform.AppContext.Resources.GetIdentifier(drawableImage.Split('.')[0], "drawable", Platform.AppContext.PackageName);
                Drawable drawable = Platform.AppContext.GetDrawable(drawableId);
                Android.Graphics.Bitmap bitMap = ((BitmapDrawable)drawable).Bitmap;
                using var memoryStream = new MemoryStream();
                bitMap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, memoryStream);
                return memoryStream.ToArray();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine($"Cannot find drawable: {drawableImage}");
                return null;
            }
        }
    }
}