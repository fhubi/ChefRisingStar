using ChefRisingStar.Models;
using ChefRisingStar.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using ChefRisingStar.Extensions;

namespace ChefRisingStar.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(ParentAchievementId), nameof(ParentAchievementId))]
    public class ManageAchievementStepViewModel : BaseDataViewModel<AchievementStep, int>
    {
        private int _id;
        private int _parentAchievementId;
        private AchievementStep _selectedItem;

        public AchievementStep SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

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
                    LoadItemId(value);
                }
            }
        }
        public int ParentAchievementId
        {
            get
            {
                return _parentAchievementId;
            }
            set
            {
                if (value != _parentAchievementId)
                {
                    _parentAchievementId = value;
                }
            }
        }

        public List<string> LinkedTypes
        {
            get
            {
                return Enum.GetNames(typeof(AchievementStepLinkedTypes)).Select(b => b.SplitCamelCase()).ToList();
            }
        }

        private AchievementStepLinkedTypes _selectedLinkType;
        public AchievementStepLinkedTypes SelectedLinkType
        {
            get
            {
                return _selectedLinkType;
            }
            set
            {
                SetProperty(ref _selectedLinkType, value);
            }
        }

        public override IDataStore<AchievementStep, int> DataStore { get; protected set; }

        public ManageAchievementStepViewModel()
        {
            Title = "Achievement Step";
            DataStore = DependencyService.Get<IDataStore<AchievementStep, int>>();
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                SelectedItem = item;
            }
            catch (Exception)
            {
                Debug.WriteLine($"Failed to Load Achievement Step: {itemId}");
            }
        }
    }
}