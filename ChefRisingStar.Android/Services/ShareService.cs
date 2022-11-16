using Android.App;
using Android.Content;
using ChefRisingStar.Droid.Services;
using ChefRisingStar.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http;

[assembly: Dependency(typeof(ShareService))]
namespace ChefRisingStar.Droid.Services
{
    internal class ShareService : Activity, IShare
    {
        public Task Share(string text, string imagePath, bool fromWeb)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);

            intent.PutExtra(Intent.ExtraText, text);

            intent.SetType("image/*");
            intent.SetFlags(ActivityFlags.GrantReadUriPermission);

            var file = new Java.IO.File(imagePath);
            var context = Android.App.Application.Context;
            var packageName = Android.App.Application.Context.PackageName;
            if (fromWeb)
            {
                intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse(imagePath));
            }
            else
            {
                intent.PutExtra(Intent.ExtraStream, FileProvider.GetUriForFile(context, packageName + ".provider", file));
            }

            intent.PutExtra(Intent.ExtraTitle, "Share Image");

            var chooserIntent = Intent.CreateChooser(intent, "Share Image" ?? string.Empty);

            var flags = ActivityFlags.ClearTop | ActivityFlags.NewTask;

            chooserIntent.SetFlags(flags);

            Platform.AppContext.StartActivity(chooserIntent);

            return Task.CompletedTask;
        }
    }
}