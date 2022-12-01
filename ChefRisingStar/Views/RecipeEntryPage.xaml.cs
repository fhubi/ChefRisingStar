using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using System.Text;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class RecipeEntryPage : ContentPage
    {
        TempRecipeViewModel _viewModel;

        public string ItemId
        {
            set
            {
                LoadRecipe(value);
            }
        }

        public RecipeEntryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TempRecipeViewModel();
            // Set the BindingContext of the page to a new Recipe.
            //BindingContext = new TempRecipeDetails();
        }

        private void CloseCuisineClicked(object sender, EventArgs e)
        {
            //BindingContext = _viewModel = new TempRecipeViewModel();
            StringBuilder selectedCuisined = new StringBuilder();

            foreach (SelectableFilter filter in _viewModel.Cuisines)
            {
                if (filter.IsSelected)
                    selectedCuisined.Append($"{filter.Text}, ");
            }

            _viewModel.SelectedCuisines = selectedCuisined.ToString();
            _viewModel.IsSelectCuisineVisible = false;
        }

        private void SelectCuisineClicked(object sender, EventArgs e)
        {
            //BindingContext = _viewModel = new TempRecipeViewModel();
            _viewModel.IsSelectCuisineVisible = true;
        }

        private void cuisineList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SelectableFilter item = e.Item as SelectableFilter;
            item.IsSelected = !item.IsSelected;
        }

        async void LoadRecipe(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the recipe and set it as the BindingContext of the page.
                TempRecipeDetails recipe = await App.Database.GetRecipeAsync(id);
                BindingContext = recipe;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load recipe.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var recipe = (TempRecipeDetails)BindingContext;
            if (!string.IsNullOrWhiteSpace(recipe.Title))
            {
                await App.Database.SaveRecipeAsync(recipe);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var recipe = (TempRecipeDetails)BindingContext;
            await App.Database.DeleteRecipeAsync(recipe);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}

