using App.Metrics;
using App.Metrics.Counter;

namespace JobOffersPortal.API.Metrics
{
    public class MetricsSendEmail
    {
        public static CounterOptions SentEmailCounter => new CounterOptions()
        {
            Name = "Sent Email",
            Context = "JobOffersApi",
            MeasurementUnit = Unit.Calls
        };
    }
}
