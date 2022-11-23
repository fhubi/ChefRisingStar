using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChefRisingStar.Services
{
    internal class DataLoader
    {
        public static void LoadData()
        {
            IDataStore<Achievement, int> achievmentDs = DependencyService.Get<MockAchievementDataStore>();
            IDataStore<AchievementStep, int> achievmentsConditionDs = DependencyService.Get<MockAchievementStepDataStore>();

            ReadOnlyCollection<Achievement> achievments = achievmentDs.GetItems();
            ReadOnlyCollection<AchievementStep> achievmentConditions = achievmentsConditionDs.GetItems();

            achievments[1].AchievementSteps.Add(achievmentConditions[9]);
            achievments[1].AchievementSteps.Add(achievmentConditions[10]);
            achievments[2].AchievementSteps.Add(achievmentConditions[1]);
            achievments[3].AchievementSteps.Add(achievmentConditions[1]);
            achievments[4].AchievementSteps.Add(achievmentConditions[4]);


            //Not needed
            //achievmentDs.UpdateItemAsync(achievments[2]);
            //achievmentDs.UpdateItemAsync(achievments[3]);

            SubstitutionCache cache = DependencyService.Get<SubstitutionCache>();
            SubstituteIngredient groundChicken = new SubstituteIngredient { Id = 5332, Name = "Ground Chicken", Image = "meat-ground.jpg" };
            SubstituteIngredient groundTurkey = new SubstituteIngredient { Id = 5662, Name = "Ground Turkey", Image = "meat-ground.jpg" };
            SubstituteIngredient groundTofu = new SubstituteIngredient { Id = 98891, Name = "Tofu", Image = "tofu.png" };

            List<SubstituteIngredient[]> subs = new List<SubstituteIngredient[]> { new SubstituteIngredient[] { groundChicken }, new SubstituteIngredient[] { groundTurkey }, new SubstituteIngredient[] { groundTofu } };
            cache.Add("ground beef", subs);
            cache.Add("lean ground beef", subs);
            cache.Add("95 percent lean ground beef", subs);

        }
    }
}
