using Vditor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddVditorBlazor(this IServiceCollection services)
        {
            services.AddScoped<VditorJsInterop>();

            return services;
        }
    }
}