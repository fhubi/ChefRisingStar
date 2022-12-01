using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockFavoritesDataStore : IDataStore<Favorite, IntStringKey>
    {
        readonly List<Favorite> items;

        public MockFavoritesDataStore()
        {
            items = new List<Favorite>()
            {
                new Favorite(1, FavoriteTypes.Recipe, "spoonacular:123" ),
                new Favorite(1, FavoriteTypes.Recipe, "spoonacular:1234" )
            };
        }

        public async Task<bool> AddItemAsync(Favorite item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Favorite item)
        {
            var oldItem = items.Where(s => s.UserId == item.UserId && s.ReferenceId == item.ReferenceId).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(IntStringKey id)
        {
            var oldItem = items.Where(s => s.UserId == id.IntValue && s.ReferenceId == id.StringValue).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Favorite> GetItemAsync(IntStringKey id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.UserId == id.IntValue && s.ReferenceId == id.StringValue));
        }

        public async Task<IEnumerable<Favorite>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<Favorite> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<Favorite> GetSearchResults(string searchText)
        {
            return items.Where(i => i.ReferenceId.Contains(searchText));
        }
    }
}