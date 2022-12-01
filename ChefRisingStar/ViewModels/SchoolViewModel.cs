using ChefRisingStar.Models;
using ChefRisingStar.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class SchoolViewModel : BaseDataViewModel<School, int>
    {
        private IDataStore<User, int> _userDataStore;
        private School _selectedItem;

        private List<User> _allUsers;
        public List<User> AllUsers
        {
            get
            {
                return _allUsers;
            }
            set
            {
                SetProperty(ref _allUsers, value);
            }
        }

        private List<User> _contacts;
        public List<User> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                SetProperty(ref _contacts, value);
            }
        }

        private List<School> _allSchools;
        public List<School> AllSchools
        {
            get
            {
                return _allSchools;
            }
            set
            {
                SetProperty(ref _allSchools, value);
            }
        }

        private List<School> _schools;
        public List<School> Schools
        {
            get
            {
                return _schools;
            }
            set
            {
                SetProperty(ref _schools, value);
            }
        }

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                SetProperty(ref _searchText, value);
            }
        }

        public School SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private User _selectedContact;
        public User SelectedContact
        {
            get => _selectedContact;
            set
            {
                SetProperty(ref _selectedContact, value);
                OnContactSelected(value);
            }
        }

        private void OnContactSelected(User value)
        {
            if (value != null)
                SelectedItem.ContactId = value.Id;
        }

        public ICommand LoadItemsCommand { get; }
        public ICommand AddItemCommand { get; }
        public Command<School> ItemTapped { get; }

        public override IDataStore<School, int> DataStore { get; protected set; }

        public SchoolViewModel()
        {
            Title = "Schools";
            Schools = new List<School>();
            AllUsers = new List<User>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<School>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DataStore = DependencyService.Get<IDataStore<School, int>>();
            _userDataStore = DependencyService.Get<IDataStore<User, int>>();
            ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                AllUsers = (await _userDataStore.GetItemsAsync(true)).ToList();
                AllSchools = (await DataStore.GetItemsAsync(true)).ToList();
                AllSchools.Sort();

                AllUsers.Insert(0, new User(0, "None", string.Empty, "None", string.Empty));
                Schools = AllSchools;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddItem(object obj)
        {
            SearchText = string.Empty;

            School item = new School(0, "New School", string.Empty, string.Empty, string.Empty, 0);

            //DataStore.AddItemAsync(item);
            AllSchools.Add(item);
            _schools = AllSchools.Where(i => i.Name == item.Name).ToList();
            SelectedItem = item;
        }

        async void OnItemSelected(School item)
        {
            if (item == null)
                return;

            Contacts = new List<User>();

            //Filter to only people who go to that school
            var results = AllUsers.Where(i => i.SchoolId == SelectedItem.Id || i.Id == 0);

            if (results.Any())
                Contacts = results.AsEnumerable().ToList();

            SelectedContact = AllUsers.Where(x => x.Id == SelectedItem.ContactId).FirstOrDefault();

            // This will push the ItemDetailPage onto the navigation stack
            //TODO: Implement UserDetailPage & UserDetailViewModel
            //await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.Id)}={user.Id}");
        }

        public void GetSearchResults(string searchText)
        {
            IsBusy = true;
            searchText = searchText.Trim().ToLower();
            Schools = AllSchools.Where(i => i.Name.ToLower().Contains(searchText) || i.Address.ToLower().Contains(searchText) || i.City.ToLower().Contains(searchText)).ToList();
            IsBusy = false;
        }

        public ICommand PerformSearch => new Command<string>((string searchText) =>
        {
            if (searchText.Length > 2)
            {
                GetSearchResults(searchText);
            }
        });

        public void ShowAllSchools()
        {
            IsBusy = true;
            Schools = AllSchools;
            IsBusy = false;
        }
    }
}