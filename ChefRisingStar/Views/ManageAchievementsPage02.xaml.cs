using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ManageAchievementsPage02 : ContentPage
    {
        private ManageAchievementViewModel _viewModel;

        public ManageAchievementsPage02()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ManageAchievementViewModel();
            Title = _viewModel.Title;
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            _viewModel.OnAppearing();
        }

        private void EditAchievementStep(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            AchievementStep step = (AchievementStep)button.Parent.BindingContext;

            _viewModel.ExecuteEditAchievementStep(step);
        }
    }
}