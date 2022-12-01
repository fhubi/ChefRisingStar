using ChefRisingStar.Models;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class AchievementsViewModel : BaseDataViewModel<Achievement, int>
    {
        private Achievement _selectedItem;

        public ObservableCollection<Achievement> Achievements { get; }
        public ICommand LoadItemsCommand { get; }
        public ICommand AddItemCommand { get; }
        public Command<Achievement> ItemTapped { get; }

        public override IDataStore<Achievement, int> DataStore { get; protected set; }

        public AchievementsViewModel()
        {
            Title = "Achievements";
            Achievements = new ObservableCollection<Achievement>();

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
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Achievements.Add(item);
                }


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

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(AchievementDetailPage)}?{nameof(AchievementDetailViewModel.Id)}={achievement.Id}");
        }
    }
}