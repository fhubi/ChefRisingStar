using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ManageAchievementsPage : ContentPage
    {
        private ManageAchievementsViewModel _viewModel;

        public ManageAchievementsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ManageAchievementsViewModel();
            Title = _viewModel.Title;
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text.Length == 0)
            {
                _viewModel.ShowAllAchievements();
                return;
            }

            if (searchBar.Text.Length > 2)
            {
                _viewModel.GetSearchResults(searchBar.Text);
            }
        }

        private void OnAppearing(object sender, EventArgs e)
        {
            _viewModel.OnAppearing();

        }
    }
}