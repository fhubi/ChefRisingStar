using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockUserDataStore : IDataStore<User, int>
    {
        readonly List<User> items;

        public MockUserDataStore()
        {
            items = new List<User>()
            {
                new User(1, "John", "Wick", "john23", "john@wick.com"),
                new User(2, "John", "Cena", "johnCNA", "john@cena.com"),
                new User(3, "John", "Ham", "hammer", "john@hamjam.com"),
                new User(4, "Coutrney", "Cox", "moxcox", "ccox@jam.com"),
                new User(5, "Jennifer", "Anniston", "jenjen", "jenny@friends.com"),
                new User(6, "Thor", "God", "Thorny", "thor@marvel.com"),
                new User(7, "Iron", "Man", "Iron man", "ironman@marvel.com"),
                new User(8, "Captain", "Marvel", "cappy", "captain@marvel.com"),
            };
        }

        public async Task<bool> AddItemAsync(User item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            var oldItem = items.Where((User arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((User arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<User> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<User> GetSearchResults(string searchText)
        {
            return items.Where(i => i.FirstName.Contains(searchText) || i.LastName.Contains(searchText) || i.Username.Contains(searchText));
        }
    }
}