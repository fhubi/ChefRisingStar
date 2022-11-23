using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ManageAchievementStepsPage : ContentPage
    {
        TeamsViewModel _viewModel;

        public ManageAchievementStepsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TeamsViewModel();
            Title = _viewModel.Title;
        }
    }
}