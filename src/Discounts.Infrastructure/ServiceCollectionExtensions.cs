using Discounts.Domain;
using Discounts.Infrastructure.Config;
using Discounts.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscountsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDiscountRulesConfigLoader, DiscountRulesConfigLoader>();
        services.AddSingleton<IDiscountRulesRepository, DiscountRulesRepository>();
        services.AddSingleton<IDiscountRulesConfigSerializer, DiscountRulesConfigSerializer>();
        
        return services;
    }
}