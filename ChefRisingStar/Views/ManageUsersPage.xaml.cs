using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ManageUsersPage : ContentPage
    {
        UsersViewModel _viewModel;
        public ManageUsersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new UsersViewModel();
            Title = _viewModel.Title;
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text.Length > 2)
            {
                _viewModel.GetSearchResults(searchBar.Text);
            }
        }

        private void SchoolSelectedChanged(object sender, EventArgs e)
        {
            _viewModel.SelectedItem.SchoolId = _viewModel.SelectedSchool.Id;
        }
    }
}