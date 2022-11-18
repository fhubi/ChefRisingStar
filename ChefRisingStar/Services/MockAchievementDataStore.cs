using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.Services
{
    public class MockAchievementDataStore : IDataStore<Achievement, int>
    {
        readonly List<Achievement> items;

        public MockAchievementDataStore()
        {
            items = new List<Achievement>()
            {
                new Achievement(1, 10, "First Achievement", "You earn this Achievement just by downloading the App and starting to use it! Well done!", "trophy64.png", AchievementTypes.Progress, DateTime.Now.AddDays(-1)),
                new Achievement(2, 20, "Register Achievement", "Register and add your preferences.", "trophy64.png", AchievementTypes.Progress),
                new Achievement(3, 30, "Post a cooking selfie!", "Show off your cooking outfit and earn this badge", "trophy64.png", AchievementTypes.Social),
                new Achievement(4, 40, "Start cooking!", "Cook a your first meal this week to get this achievement. Start your cooking adventure and make your first meal!", "trophy64.png", AchievementTypes.Skill),
                new Achievement(5, 80, "Continue cooking", "Cook a two meals this week to get this achievement.", "trophy64.png", AchievementTypes.Skill),
                new Achievement(6, 200, "Sustainable cooking", "Cook a cook a meal with a smaller eco footprint to get this achievement.", "trophy64.png", AchievementTypes.Progress),
                new Achievement(7, 400, "Lower your foodprint", "Track your progress for one week by lowering your overall foodprint.", "trophy64.png", AchievementTypes.Progress),
                new Achievement(8, 300, "Cook a Meatless Monday Meal", "Try a vegetarian alternative to a family favorite meal", "trophy64.png", AchievementTypes.Progress),
                new Achievement(9, 300, "Cosume less water", "Try a recipe variation that uses less water for a meal", "trophy64.png", AchievementTypes.Progress),
                new Achievement(10, 300, "Use less land", "Try a recipe variation that uses less land to grow your food", "trophy64.png", AchievementTypes.Progress),
                new Achievement(11, 300, "Use less packaging", "Try a recipe variation that uses less packaging to wrap your food", "trophy64.png", AchievementTypes.Progress),
                new Achievement(12, 300, "Use less fuel", "Try a recipe variation that uses less fuel to transport your food", "trophy64.png", AchievementTypes.Progress),
            };

            IDataStore<AchievementStep, int> achievmentsConditionDs = DependencyService.Get<MockAchievementConditionDataStore>();
            ReadOnlyCollection<AchievementStep> achievmentConditions = achievmentsConditionDs.GetItems();

            items[1].AchievementSteps.Add(achievmentConditions[9]);
            items[1].AchievementSteps.Add(achievmentConditions[10]);
            items[2].AchievementSteps.Add(achievmentConditions[0]);
            items[3].AchievementSteps.Add(achievmentConditions[2]);
            items[4].AchievementSteps.Add(achievmentConditions[2]);
            items[4].AchievementSteps.Add(achievmentConditions[11]);
            items[4].AchievementSteps.Add(achievmentConditions[3]);
            items[5].AchievementSteps.Add(achievmentConditions[12]);
            items[5].AchievementSteps.Add(achievmentConditions[13]);
            items[6].AchievementSteps.Add(achievmentConditions[12]);
            items[6].AchievementSteps.Add(achievmentConditions[12]);
            items[6].AchievementSteps.Add(achievmentConditions[14]);
            items[7].AchievementSteps.Add(achievmentConditions[15]);
            items[7].AchievementSteps.Add(achievmentConditions[13]);
            items[8].AchievementSteps.Add(achievmentConditions[16]);
            items[8].AchievementSteps.Add(achievmentConditions[13]);
            items[9].AchievementSteps.Add(achievmentConditions[17]);
            items[9].AchievementSteps.Add(achievmentConditions[13]);
            items[10].AchievementSteps.Add(achievmentConditions[18]);
            items[10].AchievementSteps.Add(achievmentConditions[13]);
            items[11].AchievementSteps.Add(achievmentConditions[19]);
            items[11].AchievementSteps.Add(achievmentConditions[13]);
        }

        public async Task<bool> AddItemAsync(Achievement item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Achievement item)
        {
            var oldItem = items.Where((Achievement arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Achievement arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Achievement> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Achievement>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<Achievement> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }
        public IEnumerable<Achievement> GetSearchResults(string searchText)
        {
            return items.Where(i => i.Name.Contains(searchText));
        }
    }
}