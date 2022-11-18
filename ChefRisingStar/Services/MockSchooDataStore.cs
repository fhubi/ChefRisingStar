using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockSchooDataStore : IDataStore<School, int>
    {
        readonly List<School> items;

        public MockSchooDataStore()
        {
            items = new List<School>()
            {
                new School(1, "John Rennie", "123 St. Johns Pointe Claire", "514 666-5152", "Point Claire", 0),
                new School(2, "Morgan Institute", "700 Wellington, Montreal H4C","514 555-5152", "Montreal", 0)
            };
        }

        public async Task<bool> AddItemAsync(School item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(School item)
        {
            var oldItem = items.Where((School arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((School arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<School> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<School>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<School> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<School> GetSearchResults(string searchText)
        {
            return items.Where(i => i.Name.Contains(searchText));
        }
    }
}