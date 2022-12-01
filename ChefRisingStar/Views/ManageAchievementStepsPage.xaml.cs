using ChefRisingStar.ViewModels;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{

    public partial class ManageAchievementStepsPage : ContentPage
    {
        ManageAchievementStepViewModel _viewModel;

        public ManageAchievementStepsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ManageAchievementStepViewModel();
            Title = _viewModel.Title;
        }
    }
}