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
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ManageAchievementViewModel : BaseDataViewModel<Achievement, int>
    {
        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    LoadAchievement(value);
                }
            }
        }

        private Achievement _selectedItem;

        public Achievement SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        private AchievementStep _selectedAchievementStep;

        public AchievementStep SelectedAchievementStep
        {
            get
            {
                return _selectedAchievementStep;
            }
            set
            {
                SetProperty(ref _selectedAchievementStep, value);
                OnItemSelected(value);
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

        //public ObservableCollection<Language> Languages { get; private set; }
        private List<Language> _languages;

        private Language _selectedLanguage;

        public Language SelectedLanguage
        {
            get
            {
                return _selectedLanguage;
            }
            set
            {
                SetProperty(ref _selectedLanguage, value);
            }
        }

        public List<Language> Languages
        {
            get
            {
                return _languages;
            }
            private set
            {
                SetProperty(ref _languages, value);
            }
        }

        public ICommand LoadItemsCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand SaveItemCommand { get; }

        public Command<AchievementStep> ItemTapped { get; }

        public override IDataStore<Achievement, int> DataStore { get; protected set; }

        public ManageAchievementViewModel()
        {
            Title = "Edit Achievement";

            SaveItemCommand = new Command(async () => await ExecuteSaveItemsCommand());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<AchievementStep>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DataStore = DependencyService.Get<IDataStore<Achievement, int>>();
        }

        private Task ExecuteSaveItemsCommand()
        {
            //TODO: Validate locally
            if (SelectedItem.Id == 0)
                return DataStore.AddItemAsync(SelectedItem);

            return DataStore.UpdateItemAsync(SelectedItem);
        }

        private async void LoadAchievement(int value)
        {
            var achievement = await DataStore.GetItemAsync(value);
            SelectedItem = achievement;
        }

        public async void ExecuteEditAchievementStep(AchievementStep step)
        {
            await Shell.Current.GoToAsync($"{nameof(ManageAchievementStepsPage)}?{nameof(ManageAchievementStepViewModel.Id)}={step.Id}&{nameof(ManageAchievementStepViewModel.ParentAchievementId)}={SelectedItem.Id}");
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {

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

            if (Languages == null || !Languages.Any())
                LoadLanguages();

            SelectedAchievementType = SelectedItem.AchievementType;
            SelectedLanguage = Languages.Where(l => l.Code == SelectedItem.LanguageCode).FirstOrDefault();
        }

        private void LoadLanguages()
        {
            var datastore = DependencyService.Get<IDataStore<Language, string>>();
            List<Language> languages = datastore.GetItems().ToList();
            Languages = languages;
        }

        private async void OnAddItem(object obj)
        {
            AchievementStep item = new AchievementStep();
            SelectedItem.AchievementSteps.Add(item);
            SelectedAchievementStep = item;
            ExecuteEditAchievementStep(item);
        }

        async void OnItemSelected(AchievementStep step)
        {
            IsAddEnabled = true;

            if (step == null)
                return;

            IsAddEnabled = SelectedAchievementStep.Id != 0;
        }
    }
}