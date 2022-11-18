using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ManageTeamsPage : ContentPage
    {
        TeamsViewModel _viewModel;

        public ManageTeamsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TeamsViewModel();
            Title = _viewModel.Title;
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text.Length == 0)
            {
                _viewModel.ShowAllTeams();
                return;
            }

            if (searchBar.Text.Length > 2)
            {
                _viewModel.GetSearchResults(searchBar.Text);
            }
        }
    }
}