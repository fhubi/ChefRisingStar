using ChefRisingStar.ViewModels;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class AchievementsPage : ContentPage
    {
        AchievementsViewModel _viewModel;

        public AchievementsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AchievementsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}