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
    public class TeamsViewModel : BaseDataViewModel<Team, int>
    {
        private IDataStore<User, int> _userDataStore;
        private Team _selectedItem;

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

        private List<Team> _allTeams;
        public List<Team> AllTeams
        {
            get
            {
                return _allTeams;
            }
            set
            {
                SetProperty(ref _allTeams, value);
            }
        }

        private List<Team> _teams;
        public List<Team> Teams
        {
            get
            {
                return _teams;
            }
            set
            {
                SetProperty(ref _teams, value);
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

        public Team SelectedItem
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
                SelectedItem.CaptainId = value.Id;
        }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Team> ItemTapped { get; }

        public override IDataStore<Team, int> DataStore { get; protected set; }

        public TeamsViewModel()
        {
            Title = "Teams";
            AllUsers = new List<User>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Team>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DataStore = DependencyService.Get<IDataStore<Team, int>>();
            _userDataStore = DependencyService.Get<IDataStore<User, int>>();
            ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                AllTeams = Teams = (await DataStore.GetItemsAsync(true)).ToList();
                AllUsers = (await _userDataStore.GetItemsAsync(true)).ToList();
                AllUsers.Insert(0, new User(0, "None", string.Empty, "None", string.Empty));

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
            Contacts = AllUsers;

            Team item = new Team(0, "New Team", 0);

            //DataStore.AddItemAsync(item);
            AllTeams.Add(item);
            Teams = AllTeams.Where(i => i.Name == item.Name).ToList();
            SelectedItem = item;
        }

        async void OnItemSelected(Team item)
        {
            if (item == null)
                return;

            Contacts = new List<User>();

            //Filter to only people who go to that school
            var results = AllUsers.Where(i => SelectedItem.Members.Contains(i.Id) || i.Id == 0);

            if (results.Any())
                Contacts = results.AsEnumerable().ToList();

            SelectedContact = AllUsers.Where(x => x.Id == SelectedItem.CaptainId).FirstOrDefault();

            // This will push the ItemDetailPage onto the navigation stack
            //TODO: Implement UserDetailPage & UserDetailViewModel
            //await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.Id)}={user.Id}");
        }

        public void GetSearchResults(string searchText)
        {
            IsBusy = true;
            searchText = searchText.Trim().ToLower();
            Teams = AllTeams.Where(i => i.Name.ToLower().Contains(searchText)).ToList();
            IsBusy = false;
        }

        public ICommand PerformSearch => new Command<string>((string searchText) =>
        {
            if (searchText.Length > 2)
            {
                GetSearchResults(searchText);
            }
        });

        public void ShowAllTeams()
        {
            IsBusy = true;
            Teams = AllTeams;
            IsBusy = false;
        }
    }
}