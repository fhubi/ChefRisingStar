using Newtonsoft.Json;
using System.Numerics;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace ChefRisingStar.Models
{
    public class NutritionInfo
    {
        [JsonProperty("calories")]
        [JsonPropertyName("calories")]
        public string Calories { get; set; }

        [JsonProperty("carbs")]
        [JsonPropertyName("carbs")]
        public string Carbs { get; set; }

        [JsonProperty("fat")]
        [JsonPropertyName("fat")]
        public string Fat { get; set; }

        [JsonProperty("protein")]
        [JsonPropertyName("protein")]
        public string Protein { get; set; }

        [JsonProperty("bad")]
        [JsonPropertyName("bad")]
        public NutritionInfoDetail[] Basics { get; set; }

        [JsonProperty("good")]
        [JsonPropertyName("good")]
        public NutritionInfoDetail[] Details { get; set; }

        [JsonIgnore]
        [JsonProperty("expires")]
        [JsonPropertyName("expires")]
        public BigInteger Expires { get; set; }

        [JsonProperty("isStale")]
        [JsonPropertyName("isStale")]
        public bool IsStale { get; set; }

    }
}
