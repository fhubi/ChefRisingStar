using ChefRisingStar.Models;
using ChefRisingStar.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class AchievementDetailViewModel : BaseDataViewModel<Achievement, int>
    {
        private int _id;
        private string _description;
        private string _selectedSubstitution;
        private string _restResponse;
        private bool _isShareButtonVisible;
        private Achievement _achievement;

        public override IDataStore<Achievement, int> DataStore { get; protected set; }

        public ObservableCollection<AchievementStep> AchievementSteps { get; protected set; }


        //public List<AchievementStep> AchievementSteps
        //{
        //    get => _achievementConditions;
        //    set => SetProperty(ref _achievementConditions, value);
        //}

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string SelectedSubstitution
        {
            get => _selectedSubstitution;
            set => SetProperty(ref _selectedSubstitution, value);
        }

        public string RestResponse
        {
            get => _restResponse;
            set => SetProperty(ref _restResponse, value);
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

        public bool IsShareButtonVisible
        {
            get 
            {
                return _isShareButtonVisible;
            }
            set
            {
                SetProperty(ref _isShareButtonVisible, value);
            }
        }

        public Command ShareCommand { get; }

        public AchievementDetailViewModel()
        {
            DataStore = DependencyService.Get<IDataStore<Achievement, int>>();
            AchievementSteps = new ObservableCollection<AchievementStep>();

            ShareCommand = new Command(async () => await ShareAchievement());
            //MakeRestCall();
        }

        private void MakeRestCall()
        {
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://api.spoonacular.com/recipes/findByIngredients");
                client.BaseAddress = new Uri("https://api.spoonacular.com/food/ingredients/substitutes");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                //HttpResponseMessage response = client.GetAsync("?apiKey=61f0c9888f5542a6b3604a030707b8ad&ingredients=apples,+flour,+sugar&number=2").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                HttpResponseMessage response = client.GetAsync("?apiKey=61f0c9888f5542a6b3604a030707b8ad&ingredientName=butter").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var data = response.Content.ReadAsStringAsync().Result;
                    RestResponse = data;
                    //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    //foreach (var d in dataObjects)
                    //{
                    //    Console.WriteLine("{0}", d.Name);
                    //}
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                _achievement = item;
                Id = item.Id;
                Title = item.Name;
                Description = item.Description;
                IsShareButtonVisible = _achievement.ImageOpacity == 1;

                foreach (AchievementStep ac in item.AchievementSteps)
                    AchievementSteps.Add(ac);
            }
            catch (Exception)
            {
                Debug.WriteLine($"Failed to Load Item: {itemId}");
            }
        }

        internal async Task ShareAchievement()
        {
            await DependencyService.Get<IShare>().Share(Title, _achievement.ImageSrc, "Achievement share", ShareType.Resource);
        }
    }
}
