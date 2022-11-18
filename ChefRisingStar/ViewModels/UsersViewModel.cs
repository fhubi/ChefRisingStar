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
    public class UsersViewModel : BaseDataViewModel<User, int>
    {
        private User _selectedItem;
        private School _selectedSchool;

        private bool _isUsernameReadonly = true;

        public bool IsUsernameReadonly
        {
            get { return _isUsernameReadonly; }
            set { SetProperty(ref _isUsernameReadonly, value); }
        }

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

        private List<User> _users;
        public List<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                SetProperty(ref _users, value);
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

        public User SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public School SelectedSchool
        {
            get => _selectedSchool;
            set
            {
                SetProperty(ref _selectedSchool, value);
            }
        }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<User> ItemTapped { get; }

        public override IDataStore<User, int> DataStore { get; protected set; }
        public IDataStore<School, int> SchoolDataStore { get; protected set; }

        public UsersViewModel()
        {
            Title = "Users";
            Users = new List<User>();
            AllUsers = new List<User>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<User>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DataStore = DependencyService.Get<IDataStore<User, int>>();
            SchoolDataStore = DependencyService.Get<IDataStore<School, int>>();
            ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                AllUsers = (await DataStore.GetItemsAsync(true)).ToList();
                AllSchools = (await SchoolDataStore.GetItemsAsync(true)).ToList();
                AllSchools.Sort();

                AllSchools.Insert(0, new School(0, "None", string.Empty, string.Empty, string.Empty, 0));
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
            SelectedItem = null;
        }

        private async void OnAddItem(object obj)
        {
            SearchText = string.Empty;

            IsUsernameReadonly = false;

            User user = new User()
            {
                Username = "Username",
                FirstName = "Firstname",
                LastName = "Lastname",
                EmailAddress = "user@email.com"
            };

            //DataStore.AddItemAsync(user);
            AllUsers.Add(user);
            Users = AllUsers.Where(i => i.Username == user.Username).ToList();
            SelectedItem = user;
        }

        async void OnItemSelected(User item)
        {
            if (item == null)
                return;

            IsUsernameReadonly = true;

            if (item.Id == 0)
                IsUsernameReadonly = false;

            SelectedSchool = AllSchools.Where(x => x.Id == SelectedItem.SchoolId).FirstOrDefault();

            // This will push the ItemDetailPage onto the navigation stack
            //TODO: Implement UserDetailPage & UserDetailViewModel
            //await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.Id)}={user.Id}");
        }

        public void GetSearchResults(string searchText)
        {
            IsBusy = true;
            searchText = searchText.Trim().ToLower();
            Users = AllUsers.Where(i => i.FirstName.ToLower().Contains(searchText) || i.LastName.ToLower().Contains(searchText) || i.Username.ToLower().Contains(searchText)).ToList();
            IsBusy = false;
        }

        public ICommand PerformSearch => new Command<string>((string searchText) =>
        {
            if (searchText.Length > 2)
            {
                GetSearchResults(searchText);
            }
        });

        public ICommand ShowAllUsers => new Command(() =>
        {
            IsBusy = true;
            Users = AllUsers.ToList();
            IsBusy = false;

        });
    }
}