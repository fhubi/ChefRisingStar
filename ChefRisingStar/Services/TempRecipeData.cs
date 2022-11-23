using System;
using SQLite;
using Xamarin.Forms;
using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
	public class TempRecipeDatabase
	{
        readonly SQLiteAsyncConnection database;

		public TempRecipeDatabase (string dbPath)
		{
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TempRecipeDetails>().Wait();
        }

        public Task<List<TempRecipeDetails>> GetRecipesAsync()
        {
            //Get all Recipes.
            return database.Table<TempRecipeDetails>().ToListAsync();
        }

        public Task<TempRecipeDetails> GetRecipeAsync(int id)
        {
            // Get a specific recipes.
            return database.Table<TempRecipeDetails>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecipeAsync(TempRecipeDetails recipe)
        {
            if (recipe.ID != 0)
            {
                // Update an existing recipe.
                return database.UpdateAsync(recipe);
            }
            else
            {
                // Save a new recipe.
                return database.InsertAsync(recipe);
            }
        }

        public Task<int> DeleteRecipeAsync(TempRecipeDetails recipe)
        {
            // Delete a recipe.
            return database.DeleteAsync(recipe);
        }
    }
}


