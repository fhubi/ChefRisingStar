using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChefRisingStar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        private RecipeDetailViewModel _viewModel;
        double x, y;

        public RecipeDetailPage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecipeDetailViewModel(recipe);
        }

        private void IngredientPressed(object sender, EventArgs e)
        {
            //var behave = sender as ButtonLongPressBehaviour;
            var behave = sender as Button;

            if (behave != null)
            {
                ExtendedIngredient ingredient = behave.CommandParameter as ExtendedIngredient;
                _viewModel.SelectedIngredient = ingredient;
                _viewModel.IsContextMenuVisible = true;
                _viewModel.GetSubstitutions(ingredient.NameClean);
            }
            else
            {
                Debug.WriteLine($"Error getting button for ingredient longpress sender: {sender}");
            }
        }

        private void UseSubstituteClicked(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = false;
            _viewModel.ReplaceIngredient();
        }

        private void ViewMoreClicked(object sender, EventArgs e)
        {
            _viewModel.IsContextMenuVisible = false;
            _viewModel.IsViewMoreVisible = true;
            _viewModel.GetNutritionalInfo();
        }

        private void CancelSubstituteClicked(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = false;
            _viewModel.IsViewMoreVisible = false;
            _viewModel.SelectedIngredient = null;
        }

        private void CloseContextMenu(object sender, EventArgs e)
        {
            _viewModel.IsContextMenuVisible = false;
        }

        private void ViewSubstitutesClicked(object sender, EventArgs e)
        {
            _viewModel.IsContextMenuVisible = false;
            _viewModel.IsSubstitutionVisible = true;
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    imgFootprint.TranslationX =
                     Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(imgFootprint.Width - Application.Current.MainPage.Width));
                    imgFootprint.TranslationY =
                     Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(imgFootprint.Height - Application.Current.MainPage.Height));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = imgFootprint.TranslationX;
                    y = imgFootprint.TranslationY;
                    break;
            }
        }
    }
}
