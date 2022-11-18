using ChefRisingStar.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Diagnostics;
//using Newtonsoft.Json;
using System.Text.Json;
using Xamarin.UITest;
using Platform = Xamarin.UITest.Platform;

namespace ChefRisingStar.UnitTests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class RecipeDetailViewModelTests
    {
        IApp app;
        Platform platform;

        public RecipeDetailViewModelTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void SuccessDeserializeNuitritionalInfo()
        {
            string json = "	{\"calories\":\"322k\",\"carbs\":\"34g\",\"fat\":\"6g\",\"protein\":\"33g\",\"bad\":[{\"title\":\"Calories\",\"amount\":\"322k\",\"indented\":false,\"percentOfDailyNeeds\":16.14},{\"title\":\"Fat\",\"amount\":\"6g\",\"indented\":false,\"percentOfDailyNeeds\":10.25},{\"title\":\"Saturated Fat\",\"amount\":\"2g\",\"indented\":true,\"percentOfDailyNeeds\":17.16},{\"title\":\"Carbohydrates\",\"amount\":\"34g\",\"indented\":false,\"percentOfDailyNeeds\":11.63},{\"title\":\"Sugar\",\"amount\":\"14g\",\"indented\":true,\"percentOfDailyNeeds\":15.88},{\"title\":\"Cholesterol\",\"amount\":\"70mg\",\"indented\":false,\"percentOfDailyNeeds\":23.44},{\"title\":\"Sodium\",\"amount\":\"717mg\",\"indented\":false,\"percentOfDailyNeeds\":31.19}],\"good\":[{\"title\":\"Protein\",\"amount\":\"33g\",\"indented\":false,\"percentOfDailyNeeds\":66.25},{\"title\":\"Vitamin C\",\"amount\":\"48mg\",\"indented\":false,\"percentOfDailyNeeds\":58.4},{\"title\":\"Zinc\",\"amount\":\"7mg\",\"indented\":false,\"percentOfDailyNeeds\":46.82},{\"title\":\"Vitamin B3\",\"amount\":\"9mg\",\"indented\":false,\"percentOfDailyNeeds\":46.03},{\"title\":\"Potassium\",\"amount\":\"1557mg\",\"indented\":false,\"percentOfDailyNeeds\":44.49},{\"title\":\"Vitamin B6\",\"amount\":\"0.86mg\",\"indented\":false,\"percentOfDailyNeeds\":43.18},{\"title\":\"Vitamin B12\",\"amount\":\"2µg\",\"indented\":false,\"percentOfDailyNeeds\":42.34},{\"title\":\"Phosphorus\",\"amount\":\"408mg\",\"indented\":false,\"percentOfDailyNeeds\":40.83},{\"title\":\"Iron\",\"amount\":\"6mg\",\"indented\":false,\"percentOfDailyNeeds\":36.57},{\"title\":\"Fiber\",\"amount\":\"8g\",\"indented\":false,\"percentOfDailyNeeds\":34.34},{\"title\":\"Vitamin A\",\"amount\":\"1707IU\",\"indented\":false,\"percentOfDailyNeeds\":34.15},{\"title\":\"Manganese\",\"amount\":\"0.68mg\",\"indented\":false,\"percentOfDailyNeeds\":33.93},{\"title\":\"Selenium\",\"amount\":\"23µg\",\"indented\":false,\"percentOfDailyNeeds\":33.37},{\"title\":\"Copper\",\"amount\":\"0.54mg\",\"indented\":false,\"percentOfDailyNeeds\":26.78},{\"title\":\"Magnesium\",\"amount\":\"100mg\",\"indented\":false,\"percentOfDailyNeeds\":25.22},{\"title\":\"Vitamin E\",\"amount\":\"3mg\",\"indented\":false,\"percentOfDailyNeeds\":21.97},{\"title\":\"Vitamin B2\",\"amount\":\"0.36mg\",\"indented\":false,\"percentOfDailyNeeds\":21.27},{\"title\":\"Folate\",\"amount\":\"78µg\",\"indented\":false,\"percentOfDailyNeeds\":19.56},{\"title\":\"Vitamin B1\",\"amount\":\"0.25mg\",\"indented\":false,\"percentOfDailyNeeds\":16.87},{\"title\":\"Vitamin B5\",\"amount\":\"1mg\",\"indented\":false,\"percentOfDailyNeeds\":13.97},{\"title\":\"Vitamin K\",\"amount\":\"13µg\",\"indented\":false,\"percentOfDailyNeeds\":13.31},{\"title\":\"Calcium\",\"amount\":\"97mg\",\"indented\":false,\"percentOfDailyNeeds\":9.77}],\"expires\":1634304861019,\"isStale\":true}";

            try
            {
                //var nutritionInfo = JsonSerializer.Deserialize<NutritionInfo>(json);
                //var nutritionInfo = JsonConvert.DeserializeObject<NutritionInfo>(json);
                //Assert.False(nutritionInfo == null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting nutritional: {ex}");
                Assert.True(ex == null);
            }
        }

        [Test]
        public void SuccessSerializeNuitritionalInfo()
        {
            string json = "	{\"calories\":\"322k\",\"carbs\":\"34g\",\"fat\":\"6g\",\"protein\":\"33g\",\"bad\":[{\"title\":\"Calories\",\"amount\":\"322k\",\"indented\":false,\"percentOfDailyNeeds\":16.14},{\"title\":\"Fat\",\"amount\":\"6g\",\"indented\":false,\"percentOfDailyNeeds\":10.25},{\"title\":\"Saturated Fat\",\"amount\":\"2g\",\"indented\":true,\"percentOfDailyNeeds\":17.16},{\"title\":\"Carbohydrates\",\"amount\":\"34g\",\"indented\":false,\"percentOfDailyNeeds\":11.63},{\"title\":\"Sugar\",\"amount\":\"14g\",\"indented\":true,\"percentOfDailyNeeds\":15.88},{\"title\":\"Cholesterol\",\"amount\":\"70mg\",\"indented\":false,\"percentOfDailyNeeds\":23.44},{\"title\":\"Sodium\",\"amount\":\"717mg\",\"indented\":false,\"percentOfDailyNeeds\":31.19}],\"good\":[{\"title\":\"Protein\",\"amount\":\"33g\",\"indented\":false,\"percentOfDailyNeeds\":66.25},{\"title\":\"Vitamin C\",\"amount\":\"48mg\",\"indented\":false,\"percentOfDailyNeeds\":58.4},{\"title\":\"Zinc\",\"amount\":\"7mg\",\"indented\":false,\"percentOfDailyNeeds\":46.82},{\"title\":\"Vitamin B3\",\"amount\":\"9mg\",\"indented\":false,\"percentOfDailyNeeds\":46.03},{\"title\":\"Potassium\",\"amount\":\"1557mg\",\"indented\":false,\"percentOfDailyNeeds\":44.49},{\"title\":\"Vitamin B6\",\"amount\":\"0.86mg\",\"indented\":false,\"percentOfDailyNeeds\":43.18},{\"title\":\"Vitamin B12\",\"amount\":\"2µg\",\"indented\":false,\"percentOfDailyNeeds\":42.34},{\"title\":\"Phosphorus\",\"amount\":\"408mg\",\"indented\":false,\"percentOfDailyNeeds\":40.83},{\"title\":\"Iron\",\"amount\":\"6mg\",\"indented\":false,\"percentOfDailyNeeds\":36.57},{\"title\":\"Fiber\",\"amount\":\"8g\",\"indented\":false,\"percentOfDailyNeeds\":34.34},{\"title\":\"Vitamin A\",\"amount\":\"1707IU\",\"indented\":false,\"percentOfDailyNeeds\":34.15},{\"title\":\"Manganese\",\"amount\":\"0.68mg\",\"indented\":false,\"percentOfDailyNeeds\":33.93},{\"title\":\"Selenium\",\"amount\":\"23µg\",\"indented\":false,\"percentOfDailyNeeds\":33.37},{\"title\":\"Copper\",\"amount\":\"0.54mg\",\"indented\":false,\"percentOfDailyNeeds\":26.78},{\"title\":\"Magnesium\",\"amount\":\"100mg\",\"indented\":false,\"percentOfDailyNeeds\":25.22},{\"title\":\"Vitamin E\",\"amount\":\"3mg\",\"indented\":false,\"percentOfDailyNeeds\":21.97},{\"title\":\"Vitamin B2\",\"amount\":\"0.36mg\",\"indented\":false,\"percentOfDailyNeeds\":21.27},{\"title\":\"Folate\",\"amount\":\"78µg\",\"indented\":false,\"percentOfDailyNeeds\":19.56},{\"title\":\"Vitamin B1\",\"amount\":\"0.25mg\",\"indented\":false,\"percentOfDailyNeeds\":16.87},{\"title\":\"Vitamin B5\",\"amount\":\"1mg\",\"indented\":false,\"percentOfDailyNeeds\":13.97},{\"title\":\"Vitamin K\",\"amount\":\"13µg\",\"indented\":false,\"percentOfDailyNeeds\":13.31},{\"title\":\"Calcium\",\"amount\":\"97mg\",\"indented\":false,\"percentOfDailyNeeds\":9.77}],\"expires\":1634304861019,\"isStale\":true}";

            NutritionInfoDetail[] bad = new NutritionInfoDetail[] { new NutritionInfoDetail() { Title = "Calories", Amount = "322k", Indented = false, PercentOfDailyNeeds = 16.14 } };
            NutritionInfoDetail[] good = new NutritionInfoDetail[] { new NutritionInfoDetail() { Title = "Calories", Amount = "322k", Indented = false, PercentOfDailyNeeds = 16.14 } };

            NutritionInfo nutrition = new NutritionInfo()
            {
                Calories = "322k",
                Carbs = "34g",
                Fat = "6g",
                Protein = "33g",
                Basics = bad,
                Details = good
            };

            try
            {
                //var nutritionInfo = JsonSerializer.Deserialize<NutritionInfo>(json);
                //byte[] byteArray = Encoding.ASCII.GetBytes(test);
                //MemoryStream stream = new MemoryStream(byteArray);
                //using (TextWriter writer = new MemoryStream(byteArray))
                {
                    string nutritionInfo = JsonSerializer.Serialize(nutrition);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting nutritional: {ex}");
                Assert.True(ex == null);
            }
        }
    }
}
