using AwBll.Implementations.Sales;
using AwBll.Interfaces.Sales;

namespace WebApiAwLayers.Configuration
{
    public static class DependencyInjections
    {
        public static void AddBll(this IServiceCollection services)
        {
            services.AddScoped<ISalesReasonBLL, SalesReasonBLL>();
        }

    }
}
