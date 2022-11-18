using ChefRisingStar.Extensions;
using ChefRisingStar.Models;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class ManageAchievementsViewModel : BaseDataViewModel<Achievement, int>
    {
        private Achievement _selectedItem;

        private List<Achievement> _allAchievements;
        public List<Achievement> AllAchievements
        {
            get
            {
                return _allAchievements;
            }
            set
            {
                SetProperty(ref _allAchievements, value);
            }
        }

        private List<Achievement> _achievements;
        public List<Achievement> Achievements
        {
            get
            {
                return _achievements;
            }
            set
            {
                SetProperty(ref _achievements, value);
            }
        }

        public List<string> AchieveTypes
        {
            get
            {
                return Enum.GetNames(typeof(AchievementTypes)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        private AchievementTypes _selectedAchievementType;
        public AchievementTypes SelectedAchievementType
        {
            get
            {
                return _selectedAchievementType;
            }
            set
            {
                SetProperty(ref _selectedAchievementType, value);
            }
        }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Achievement> ItemTapped { get; }

        public override IDataStore<Achievement, int> DataStore { get; protected set; }

        public ManageAchievementsViewModel()
        {
            Title = "Manage Achievements";
            Achievements = new List<Achievement>();
            AllAchievements = new List<Achievement>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Achievement>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DataStore = DependencyService.Get<IDataStore<Achievement, int>>();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Achievements.Clear();
                AllAchievements = (await DataStore.GetItemsAsync(true)).ToList();
                Achievements = AllAchievements;
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
            ExecuteLoadItemsCommand();
        }

        public Achievement SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Achievement achievement)
        {
            if (achievement == null)
                return;

            SelectedAchievementType = achievement.AchievementType;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(AchievementDetailPage)}?{nameof(AchievementDetailViewModel.Id)}={achievement.Id}");
        }

        public void ShowAllAchievements()
        {
            IsBusy = true;
            Achievements = AllAchievements;
            IsBusy = false;
        }

        public void GetSearchResults(string searchText)
        {
            IsBusy = true;
            searchText = searchText.Trim().ToLower();
            Achievements = AllAchievements.Where(i => i.Name.ToLower().Contains(searchText)).ToList();
            IsBusy = false;
        }

        public ICommand PerformSearch => new Command<string>((string searchText) =>
        {
            if (searchText.Length > 2)
            {
                GetSearchResults(searchText);
            }
        });
    }
}