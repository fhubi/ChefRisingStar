using ChefRisingStar.ViewModels;
using ChefRisingStar.Views;
using System;
using Xamarin.Forms;

namespace ChefRisingStar
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        AppShellViewModel _viewModel;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(AchievementDetailPage), typeof(AchievementDetailPage));
            Routing.RegisterRoute(nameof(FoodPrintsDetailPage), typeof(FoodPrintsDetailPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ManageSchoolsPage), typeof(ManageSchoolsPage));
            Routing.RegisterRoute(nameof(ManageUsersPage), typeof(ManageUsersPage));
            Routing.RegisterRoute(nameof(RecipeEntryPage), typeof(RecipeEntryPage));

            BindingContext = _viewModel = new AppShellViewModel();        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
