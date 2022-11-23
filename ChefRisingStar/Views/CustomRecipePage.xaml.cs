using System;
using System.Linq;
using System.Collections.Generic;
using ChefRisingStar.Models;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{	
	public partial class CustomRecipePage : ContentPage
	{	
		public CustomRecipePage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
			base.OnAppearing();

            // Retrieve all the recipes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetRecipesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the RecipeEntryPage.
            await Shell.Current.GoToAsync(nameof(RecipeEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the RecipeEntryPage, passing the ID as a query parameter.
                TempRecipeDetails recipe = (TempRecipeDetails)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(RecipeEntryPage)}?{nameof(RecipeEntryPage.ItemId)}={recipe.ID.ToString()}");
            }
        }
    }

}

