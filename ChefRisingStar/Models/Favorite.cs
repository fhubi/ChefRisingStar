using System;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Favorite
    {
        public int UserId { get; set; }
        public string ReferenceId { get; set; }
        public int FavoriteTypeID { get; set; }
        public DateTime ActivityTime { get; set; }

        public FavoriteTypes FavoriteType
        {
            get { return (FavoriteTypes)FavoriteTypeID; }
        }

        public Favorite()
        {

        }
        public Favorite(int userId, int favoriteTypeID, string refId)
        {
            UserId = userId;
            FavoriteTypeID = favoriteTypeID;
            ReferenceId = refId ?? throw new ArgumentNullException(nameof(refId));
            ActivityTime = DateTime.UtcNow;
        }
        
        public Favorite(int userId, FavoriteTypes favoriteType, string refId) : this(userId,(int)favoriteType, refId)
        {
            
        }

        public override string ToString()
        {
            return $"{UserId}:{FavoriteTypeID} - {ReferenceId}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{UserId}:{FavoriteTypeID} - {ReferenceId}";
        }
    }

    public enum FavoriteTypes
    {
        Recipe,Ingredient,Bookmark,Cuisine,Person
    }
}
