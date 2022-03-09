using App.Metrics;
using App.Metrics.Counter;

namespace JobOffersPortal.API.Metrics
{
    public class MericsRegistry
    {
        public static CounterOptions CreatedCustomerCounter => new CounterOptions()
        {
            Name = "Created Users",
            Context = "CustomersApi",
            MeasurementUnit = Unit.Calls
        };
    }
}
