using Discounts.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscountsDomain(this IServiceCollection services)
    {
        services.AddScoped<IDiscountService,DiscountService>();
        return services;
    }
}