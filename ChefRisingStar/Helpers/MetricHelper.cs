using ChefRisingStar.Models;

namespace ChefRisingStar.Helpers
{
    internal class MetricHelper
    {
        private const string Route = "/metrics/";
        public static void SendMetric(AppMetric metric)
        {
            RestHelper.MakePost(metric, Route);
        }

        public static void SendMetric(MetricType metricType, string value)
        {
            AppMetric metric = new AppMetric(metricType, value);
            SendMetric(metric);
        }
    }
}
