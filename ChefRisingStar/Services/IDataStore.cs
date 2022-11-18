using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public interface IDataStore<T, K>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(K id);
        Task<T> GetItemAsync(K id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        ReadOnlyCollection<T> GetItems(bool forceRefresh = false);
        IEnumerable<T> GetSearchResults(string searchText);
    }
}
