using System;

namespace ChefRisingStar.Models
{
    public enum MetricType
    {
        NewUserCreated,
        UserLoggedIn,
        UserLinkedSocialMediaAccount,
        UserCompletedAchievement,
        UserSearchedRecipe,
        UserLikedRecipe,
        UserLikedIngredient,
        UserSharedPhoto,
        UserSharedUpdate,
        UserJoinedBrigade,
        UserSubmittedRecipe,
        UserSentMessage,
        AppError
    }
    public class AppMetric
    {
        public MetricType MetricTypeCaptured { get; set; }
        public string Value { get; set; }
        public DateTime OccuredOn { get; set; }

        public AppMetric()
        {
        }

        public AppMetric(MetricType metricTypeCaptured, string value)
        {
            MetricTypeCaptured = metricTypeCaptured;
            Value = value;
        }
    }
}
