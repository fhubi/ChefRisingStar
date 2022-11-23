using System;
using Newtonsoft.Json;
using SQLite;

namespace ChefRisingStar.Models
{
    
    public class TempRecipeDetails
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("readyInMinutes")]
        public long ReadyInMinutes { get; set; }

        [JsonProperty("servings")]
        public long Servings { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }
    }

}

