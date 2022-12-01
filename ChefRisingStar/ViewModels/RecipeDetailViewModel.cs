using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using ChefRisingStar.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChefRisingStar.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        #region Members
        private bool _isSubstitutionVisible;
        private bool _isViewMoreVisible;
        private bool _isContextMenuVisible;
        private bool _isUserFavorite;
        private bool _isUserBookmark;
        private Recipe _recipe;
        private string _selectedSubstitution;

        private IDataStore<Favorite, IntStringKey> FavortiesDatastore;

        private ExtendedIngredient _selectedIngredient;

        private List<ExtendedIngredient> _newIngredients { get; set; }

        private NutritionInfo _nutritionInfo;
        #endregion Members

        #region Properties

        public Recipe Recipe
        {
            get => _recipe;
            set
            {
                if (_recipe == value)
                    return;

                _recipe = value;
                OnPropertyChanged();
            }
        }

        public NutritionInfo RecipeNutritionInfo
        {
            get => _nutritionInfo;
            set
            {
                _nutritionInfo = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsUserFavorite
        {
            get => _isUserFavorite;
            set
            {
                _isUserFavorite = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsUserBookmark
        {
            get => _isUserBookmark;
            set
            {
                _isUserBookmark = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Substitutions { get; protected set; }
        public ObservableCollection<NutritionInfoDetail> BasicNutritionalInfo { get; protected set; }
        public ObservableCollection<NutritionInfoDetail> DetailNutritionalInfo { get; protected set; }

        public bool IsSubstitutionVisible
        {
            get => _isSubstitutionVisible;
            set
            {
                _isSubstitutionVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsViewMoreVisible
        {
            get => _isViewMoreVisible;
            set
            {
                _isViewMoreVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsContextMenuVisible
        {
            get => _isContextMenuVisible;
            set
            {
                _isContextMenuVisible = value;
                OnPropertyChanged();
            }
        }

        public ExtendedIngredient SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSubstitution
        {
            get => _selectedSubstitution;
            set
            {
                _selectedSubstitution = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Step> Instructions { get; protected set; }
        public ObservableCollection<ExtendedIngredient> NewIngredients { get; protected set; }

        public ObservableCollection<FootprintModel> PolarData1 { get; set; }

        public ObservableCollection<FootprintModel> PolarData2 { get; set; }

        public ObservableCollection<FootprintModel> PolarData3 { get; set; }


        #endregion Properties

        #region Commands

        public ICommand LoadUrlCommand { get; }
        public ICommand OpenSubstitutionsCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ToggleBookmarkCommand { get; }

        public ICommand ShareCommand { get; }
        #endregion 

        #region Constructors

        public RecipeDetailViewModel(Recipe recipe)
        {
            Title = recipe.Title;
            Recipe = recipe;

            //TODO: REMOVE When actual user is set
            CurrentUser = new User
            {
                Id = 1,
                Username = "Fallout",
                EmailAddress = "fallout99@hotmail.com",
                IsAdmin = true
            };

            Instructions = new ObservableCollection<Step>();
            Substitutions = new ObservableCollection<string>();
            NewIngredients = new ObservableCollection<ExtendedIngredient>();
            BasicNutritionalInfo = new ObservableCollection<NutritionInfoDetail>();
            DetailNutritionalInfo = new ObservableCollection<NutritionInfoDetail>();

            ToggleFavoriteCommand = new Command(ExecuteToggleFavorite);
            ToggleBookmarkCommand = new Command(ExecuteToggleBookmark);
            FavortiesDatastore = DependencyService.Get<IDataStore<Favorite, IntStringKey>>();

            foreach (AnalyzedInstruction ins in Recipe.AnalyzedInstructions)
            {
                foreach (Step step in ins.Steps)
                    Instructions.Add(step);
            }

            foreach (ExtendedIngredient ingredient in Recipe.ExtendedIngredients)
            {
                NewIngredients.Add(ingredient);
            }


            PolarData1 = new ObservableCollection<FootprintModel>
            {
                new FootprintModel{ LandUsage = 10, TotalCO2 = 15, Farming = 20, Waste = 22, Transport = 50 },
                new FootprintModel{ LandUsage = 12, TotalCO2 = 25, Farming = 30, Waste = 12, Transport = 33 },
                new FootprintModel{ LandUsage = 40, TotalCO2 = 55, Farming = 20, Waste = 30, Transport = 33 }
            };
            //Eventually use commanding
            //OpenSubstitutionsCommand = new Command(async () => await GetSubstitutions<object>(null));
            //OpenSubstitutionsCommand = new Command<Type>(
            //async (Type pageType) =>
            //{
            //    Page page = (Page)Activator.CreateInstance(pageType);
            //    await Navigation.PushAsync(page);
            //});

            ShareCommand = new Command(async () => await ShareRecipe());
        }

        private async void ExecuteToggleFavorite()
        {
            ExecuteToggle(FavoriteTypes.Recipe).ContinueWith(t => IsUserFavorite = t.Result);
        }
        
        private async void ExecuteToggleBookmark()
        {
            ExecuteToggle(FavoriteTypes.Bookmark).ContinueWith(t=> IsUserBookmark = t.Result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="favoriteType"></param>
        /// <returns>If is now FavoriteType</returns>
        private async Task<bool> ExecuteToggle(FavoriteTypes favoriteType)
        {
            try
            {
                List<Favorite> favorites = (await FavortiesDatastore.GetItemsAsync(true)).ToList();

                var foundFave = favorites.FirstOrDefault(f => f.UserId == CurrentUser.Id && f.ReferenceId == Recipe.Id.ToString() && f.FavoriteType == favoriteType);

                if (foundFave != null)
                {
                    FavortiesDatastore.DeleteItemAsync(new IntStringKey(foundFave.UserId, foundFave.ReferenceId));
                    return false;
                }
                else
                {
                    FavortiesDatastore.AddItemAsync(new Favorite(CurrentUser.Id, favoriteType, Recipe.Id.ToString()));
                    return true;
                }
            }
            catch(Exception ex)
            {

            }

            return false;
        }

        #endregion

        #region Methods

        internal async Task GetSubstitutions(string ingredientName)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            SubstitutionCache cache = DependencyService.Get<SubstitutionCache>();

            if (cache.Contains(ingredientName))
            {
                Substitutions.Clear();

                foreach (SubstituteIngredient[] substitutes in cache.Get(ingredientName))
                {
                    Substitutions.Add(SubstitutionHelper.StringFormat(substitutes));
                }

                IsBusy = false;
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.spoonacular.com/food/ingredients/substitutes");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Add an Accept header for JSON format.
                    var streamTask = client.GetStreamAsync($"?apiKey=61f0c9888f5542a6b3604a030707b8ad&ingredientName={ingredientName}");

                    var substitution = await JsonSerializer.DeserializeAsync<Substitution>(await streamTask);

                    // string strResponse = "{\"status\":\"success\",\"ingredient\":\"butter\",\"substitutes\":[\"1 cup = 7 / 8 cup shortening and 1 / 2 tsp salt\",\"1 cup = 7 / 8 cup vegetable oil + 1 / 2 tsp salt\",\"1 / 2 cup = 1 / 4 cup buttermilk +1 / 4 cup unsweetened applesauce\",\"1 cup = 1 cup margarine\"],\"message\":\"Found 4 substitutes for the ingredient.\"}";
                    // var substitution = JsonSerializer.Deserialize<Substitution>(strResponse);

                    Substitutions.Clear();

                    if (substitution.Status.ToLower() == "failure")
                    {
                        Substitutions.Add(substitution.Message);
                        cache.Add(ingredientName, SubstitutionHelper.GetNoSubstituteItem());
                        IsBusy = false;
                        return;
                    }

                    List<SubstituteIngredient[]> subs = new List<SubstituteIngredient[]>();

                    foreach (string s in substitution.Substitutes)
                    {
                        SubstituteIngredient[] substituteIngredients = SubstitutionHelper.ParseSubstitution(s);
                        subs.Add(substituteIngredients);
                        Substitutions.Add(SubstitutionHelper.StringFormat(substituteIngredients));
                    }

                    cache.Add(ingredientName, subs);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting substitutions: {ex}");
                    await Application.Current.MainPage.DisplayAlert("API Error:", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        internal async Task GetNutritionalInfo3()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri($"https://api.spoonacular.com/recipes/{Recipe.Id}/nutritionWidget.json");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Add an Accept header for JSON format.
                    var streamTask = client.GetStreamAsync($"?apiKey=61f0c9888f5542a6b3604a030707b8ad");

                    var temp = await streamTask;

                    var result = streamTask.Result;

                    var nutritionInfo = await JsonSerializer.DeserializeAsync<NutritionInfo>(await streamTask);

                    if (nutritionInfo == null)
                    {
                        return;
                    }

                    RecipeNutritionInfo = nutritionInfo;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting nutritional: {ex}");
                    await Application.Current.MainPage.DisplayAlert("API Error:", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        internal async Task GetNutritionalInfo()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri($"https://api.spoonacular.com/recipes/{Recipe.Id}/nutritionWidget.json");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Add an Accept header for JSON format.
                    var streamTask = client.GetStreamAsync($"?apiKey=61f0c9888f5542a6b3604a030707b8ad");

                    string json = await client.GetStringAsync(client.BaseAddress + "?apiKey=61f0c9888f5542a6b3604a030707b8ad");
                    var nutritionInfo = JsonConvert.DeserializeObject<NutritionInfo>(json);
                    RecipeNutritionInfo = nutritionInfo;

                    BasicNutritionalInfo.Clear();

                    foreach (var i in RecipeNutritionInfo.Basics)
                    {
                        BasicNutritionalInfo.Add(i);
                    }

                    //DetailNutritionalInfo = new ObservableCollection<NutritionInfoDetail>(RecipeNutritionInfo.Details);
                    DetailNutritionalInfo.Clear();

                    foreach (var i in RecipeNutritionInfo.Details)
                    {
                        BasicNutritionalInfo.Add(i);
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting nutritional: {ex}");
                    await Application.Current.MainPage.DisplayAlert("API Error:", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        //internal async Task<IngredientSearch[]> GetIngredientByName(string ingredientName)
        internal async Task<IngredientSearch> GetIngredientByName(string ingredientName)
        {
            IngredientCache cache = DependencyService.Get<IngredientCache>();

            if (cache.Contains(ingredientName))
            {
                IngredientSearch ingredient = cache.Get(ingredientName);
                Substitutions.Clear();
                IsBusy = false;
                return ingredient;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.spoonacular.com/food/ingredients/autocomplete");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Add an Accept header for JSON format.
                    //var streamTask = client.GetStringAsync($"?apiKey=61f0c9888f5542a6b3604a030707b8ad&query={ingredientName}&number=1&metaInformation=true");
                    //var str = await streamTask;
                    var streamTask = client.GetStreamAsync($"?apiKey=61f0c9888f5542a6b3604a030707b8ad&query={ingredientName}&number=1&metaInformation=true");
                    var ingredients = await JsonSerializer.DeserializeAsync<IngredientSearch[]>(await streamTask);

                    if (ingredients != null && ingredients.Length > 0)
                        cache.Add(ingredientName, ingredients[0]);

                    return ingredients[0];
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting IngredientByName: {ex}");
                    await Application.Current.MainPage.DisplayAlert("API Error:", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

            return null;
        }

        internal void ReplaceIngredient()
        {
            //SubstituteIngredient[] substitutes = SubstitutionHelper.ParseSubstitution(SelectedSubstitution);

            //if (substitutes != null)
            //{
            //    ReplaceIngredient(substitutes);
            //}
            try
            {
                IngredientCache cache = DependencyService.Get<IngredientCache>();
                IngredientSearch ing = cache.Get(SelectedSubstitution);
                SelectedIngredient.Name = $"{SelectedSubstitution}";

                if (ing != null)
                {
                    SelectedIngredient.Image = ing.Image;
                    SelectedIngredient.ImageSrc = ing.Image;
                }

                CheckAchievments();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error replacing ingredient '{SelectedIngredient.Name}' with {SelectedSubstitution}: {ex}");
            }
        }

        public async void OnAppearing()
        {
            IsBusy = true;

            try
            {
                List<Favorite> favorites = (await FavortiesDatastore.GetItemsAsync(true)).ToList();
                var foundFave = favorites.Any(f => f.UserId == CurrentUser.Id && f.ReferenceId == Recipe.Id.ToString() && f.FavoriteType == FavoriteTypes.Recipe);

                if(foundFave)
                {
                    IsUserFavorite = true;
                }
                
                var foundBookmark = favorites.Any(f => f.UserId == CurrentUser.Id && f.ReferenceId == Recipe.Id.ToString() && f.FavoriteType == FavoriteTypes.Bookmark);

                if(foundBookmark)
                {
                    IsUserBookmark = true;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error OnAppearing: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void CheckAchievments()
        {
            IDataStore<Achievement, int> achievmentDs = DependencyService.Get<MockAchievementDataStore>();
            IDataStore<AchievementStep, int> achievmentsConditionDs = DependencyService.Get<MockAchievementStepDataStore>();

            ReadOnlyCollection<Achievement> achievments = achievmentDs.GetItems();
            ReadOnlyCollection<AchievementStep> achievmentConditions = achievmentsConditionDs.GetItems();

            achievments[5].AchievementSteps[0].CompletionDate = DateTime.Now;
        }

        internal void ReplaceIngredient(SubstituteIngredient[] substitutes)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            //IngredientSearch[] ing = GetIngredientByName(substitutes[0].Name);

            SelectedIngredient.Name = substitutes[0].Name;

            //NewIngredients.Remove(SelectedIngredient);

            //foreach (var substituteIngredient in substitutes)
            //{
            //    GetIngredientByName(substituteIngredient.Name);
            //    //NewIngredients.Add(newIngredient);
            //    //TODO: continue from here
            //}


            IsBusy = false;
        }

        internal async Task ShareRecipe()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Photo options", "Cancel", null, "Take photo", "Select from gallery", "Use default image");
            ShareType shareType = ShareType.Storage;
            string filePath;

            switch (action)
            {
                case "Cancel":
                    return;
                case "Take photo":
                    var photoFileResult = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions() { Title = Recipe.Title });
                    filePath = photoFileResult.FullPath;
                    break;
                case "Select from gallery":
                    var galleryFileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions() { Title = Recipe.Title });
                    filePath = galleryFileResult.FullPath;
                    break;
                default:
                    filePath = Recipe.Image.OriginalString;
                    shareType = ShareType.Web;
                    break;

            }

            await DependencyService.Get<IShare>().Share(Recipe.Instructions?
                .Replace("<p>", "").Replace("</p>", "")
                .Replace("<ol>", "").Replace("</ol>", "")
                .Replace("<li>", "").Replace("</li>", " "), filePath, "Recipe share", shareType);
        }
        #endregion
    }
}