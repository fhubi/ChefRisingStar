using ChefRisingStar.iOS.Services;
using ChefRisingStar.Services;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ShareService))]
namespace ChefRisingStar.iOS.Services
{
    internal class ShareService : IShare
    {
        public async Task Share(string text, string imagePath, bool fromWeb)
        {
            ImageSource imageSource = fromWeb ? ImageSource.FromUri(new Uri(imagePath)) : ImageSource.FromFile(imagePath);
            var handler = new ImageLoaderSourceHandler();
            var uiImage = await handler.LoadImageAsync(imageSource);

            var image = NSObject.FromObject(uiImage);
            var message = NSObject.FromObject(text);

            var activityItems = new[] { message, image };
            var activityController = new UIActivityViewController(activityItems, null);

            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            topController.PresentViewController(activityController, true, () => { });
        }
    }
}