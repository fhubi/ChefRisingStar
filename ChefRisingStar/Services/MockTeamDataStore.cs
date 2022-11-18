using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockTeamDataStore : IDataStore<Team, int>
    {
        readonly List<Team> items;

        public MockTeamDataStore()
        {
            List<int> membersA = new List<int> { 1, 2 };
            List<int> membersB = new List<int> { 3, 4 };

            items = new List<Team>()
            {
                new Team(1, "Avengers", 1)
                {
                    Members = membersA
                },
                new Team(2, "Capcom", 2)
                {
                    Members= membersB
                }
            };
        }

        public async Task<bool> AddItemAsync(Team item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Team item)
        {
            var oldItem = items.Where((Team arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Team arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Team> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Team>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<Team> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<Team> GetSearchResults(string searchText)
        {
            return items.Where(i => i.Name.Contains(searchText));
        }
    }
}