using Discounts.Application;
using Discounts.Domain;
using Discounts.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Discounts.IntegrationTests;

public class DependencyInjectionTests
{
    private readonly ServiceProvider _provider;

    public DependencyInjectionTests()
    {
        var configuration = new ConfigurationBuilder()
            .Build();
        _provider = new ServiceCollection()
            .AddDiscountsApplication()
            .AddDiscountsDomain()
            .AddDiscountsInfrastructure(configuration)
            .BuildServiceProvider();
    }

    [Fact]
    public void Discount_service_resolves() => 
        _provider.GetRequiredService<IDiscountService>();

    [Fact]
    public void Discount_rule_repository_resolves() => 
        _provider.GetRequiredService<IDiscountRulesRepository>();

    [Fact]
    public void Discount_query_handler_resolves() => 
        _provider.GetRequiredService<IRequestHandler<GetDiscounts.Query, GetDiscounts.Response>>();
    
    [Fact]
    public void Discount_query_validator_resolves() => 
        _provider.GetRequiredService<IValidator<GetDiscounts.Query>>();
}