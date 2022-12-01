using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockLanguageDataStore : IDataStore<Language, string>
    {
        readonly List<Language> items;

        public MockLanguageDataStore()
        {
            items = new List<Language>()
            {
                new Language("en", "English"),
                new Language("fr", "Francais")
            };
        }

        public async Task<bool> AddItemAsync(Language item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Language item)
        {
            var oldItem = items.Where((Language arg) => arg.Code == item.Code).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Language arg) => arg.Code == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Language> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Code == id));
        }

        public async Task<IEnumerable<Language>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<Language> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<Language> GetSearchResults(string searchText)
        {
            return items.Where(i => i.Name.Contains(searchText));
        }
    }
}