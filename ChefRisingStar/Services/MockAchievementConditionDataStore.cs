using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockAchievementConditionDataStore : IDataStore<AchievementStep, int>
    {
        readonly List<AchievementStep> items;

        public MockAchievementConditionDataStore()
        {
            items = new List<AchievementStep>()
            {
                new AchievementStep(1, "Post a selfie", "Share a photo of yourself cooking"),
                new AchievementStep(2, "Cook a meal (assisted)", "Prepre a meal with assistance from a parent"),
                new AchievementStep(3, "Cook a meal (unassisted)", "Prepre a meal all on your own"),
                new AchievementStep(4, "Make a new dish", "Try something you have never made before"),
                new AchievementStep(5, "Share a photo", "Share a photo of yourself cooking"),
                new AchievementStep(6, "Watch a video", "Watch an instructional video to learn a new skill"),
                new AchievementStep(7, "Read a new recipe", "Read about a new recipe"),
                new AchievementStep(8, "Teach another", "Show someone else a skill your learned and howto properly do it"),
                new AchievementStep(9, "Log in", "Create an account, log in and complete registration"),
                new AchievementStep(10, "Add Preferences", "Tell us what you like!"),
                new AchievementStep(11, "Favorite a Recipe", "Tell us what you like!"),
                new AchievementStep(12, "Use the recipe search", "Find something new to cook"),
                new AchievementStep(13, "Swap Ingredients", "Find a more sustainable ingredient and substitute into a recipe"),
                new AchievementStep(14, "Post Review", "Post a review for a recipe modification you have made"),
                new AchievementStep(15, "Post Weekly report", "Post your report for the progress you made this week"),
                new AchievementStep(16, "Substitute meat in dish", "Swap out the meat protein in the recipe for a plant based alternative"),
                new AchievementStep(17, "Use less water", "Swap recipe ingredients to reduce water foot print"),
                new AchievementStep(18, "Use less land", "Swap recipe ingredients to reduce land usage foot print"),
                new AchievementStep(19, "Use less packaging", "Swap recipe ingredients to reduce packaging foot print"),
                new AchievementStep(20, "Use less fuel", "Swap recipe ingredients to reduce transportation foot print"),
            };
        }

        public async Task<bool> AddItemAsync(AchievementStep item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(AchievementStep item)
        {
            var oldItem = items.Where((AchievementStep arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((AchievementStep arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<AchievementStep> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<AchievementStep>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<AchievementStep> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<AchievementStep> GetSearchResults(string searchText)
        {
            return items.Where(i => i.Name.Contains(searchText));
        }
    }
}