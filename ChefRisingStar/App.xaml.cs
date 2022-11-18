using ChefRisingStar.Services;
using Xamarin.Forms;

namespace ChefRisingStar
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockAchievementConditionDataStore>();
            DependencyService.Register<MockAchievementDataStore>();
            DependencyService.Register<MockUserDataStore>();
            DependencyService.Register<MockSchooDataStore>();
            DependencyService.Register<MockTeamDataStore>();

            DependencyService.Register<SubstitutionCache>();
            DependencyService.Register<IngredientCache>();
            //DependencyService.Register<MockRecipeDataStore>();

            DataLoader.LoadData();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
