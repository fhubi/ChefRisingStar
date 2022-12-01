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

        public Achievement SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
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

        private bool _isAddEnabled;
        public bool IsAddEnabled
        {
            get
            {
                return _isAddEnabled;
            }
            set
            {
                SetProperty(ref _isAddEnabled, value);
            }
        }

        public ICommand LoadItemsCommand { get; }
        public ICommand AddItemCommand { get; }

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

        public async void ExecuteEditAchievementStep(AchievementStep step)
        {
            await Shell.Current.GoToAsync($"{nameof(ManageAchievementStepsPage)}?{nameof(ManageAchievementStepViewModel.Id)}={step.Id}&{nameof(ManageAchievementStepViewModel.ParentAchievementId)}={SelectedItem.Id}");
        }

        public async void ExecuteEditAchievement()
        {
            await Shell.Current.GoToAsync($"{nameof(ManageAchievementsPage02)}?{nameof(ManageAchievementViewModel.Id)}={SelectedItem.Id}");
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

            if (!AllAchievements.Any())
                ExecuteLoadItemsCommand();
        }

        private async void OnAddItem(object obj)
        {
            Achievement item = new Achievement();
            AllAchievements.Add(item);
            SelectedItem = item;
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Achievement achievement)
        {
            IsAddEnabled = true;

            if (achievement == null)
                return;

            IsAddEnabled = SelectedItem.Id != 0;
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