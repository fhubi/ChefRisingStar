using Newtonsoft.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace ChefRisingStar.Models
{
    public class NutritionInfoDetail : BaseNotifyModel
    {
        private string _title;
        private string _amount;
        private bool _indented;
        private double _percentOfDailyNeeds;

        [JsonProperty("title")]
        [JsonPropertyName("title")]
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        [JsonProperty("amount")]
        [JsonPropertyName("amount")]
        public string Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        [JsonProperty("indented")]
        [JsonPropertyName("indented")]
        public bool Indented
        {
            get { return _indented; }
            set { SetProperty(ref _indented, value); }
        }

        [JsonProperty("percentOfDailyNeeds")]
        [JsonPropertyName("percentOfDailyNeeds")]
        public double PercentOfDailyNeeds
        {
            get { return _percentOfDailyNeeds; }
            set { SetProperty(ref _percentOfDailyNeeds, value); }
        }

        [JsonIgnore]
        public string DailyPercent
        {
            get { return (PercentOfDailyNeeds / 100).ToString("P"); }
        }

        public override string ToString()
        {
            return $"{Title}: {Amount} - {PercentOfDailyNeeds}";
        }
    }
}
