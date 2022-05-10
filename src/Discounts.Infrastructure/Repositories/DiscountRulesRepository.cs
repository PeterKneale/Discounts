using Discounts.Domain;
using Discounts.Infrastructure.Config;

namespace Discounts.Infrastructure.Repositories;

public class DiscountRulesRepository : IDiscountRulesRepository
{
    private readonly IDiscountRulesConfigLoader _configLoader;

    public DiscountRulesRepository(IDiscountRulesConfigLoader configLoader)
    {
        _configLoader = configLoader;
    }

    public Task<IEnumerable<IDiscountRule>> GetAllAsync()
    {
        var discounts = _configLoader.GetDiscountRules();
        return Task.FromResult(discounts);
    }
}