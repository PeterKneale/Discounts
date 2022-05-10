using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRulesRepository _repository;
    
    public DiscountService(IDiscountRulesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Discount>> GetDiscountsAsync(Sale sale)
    {
        var rules = await _repository.GetAllAsync();
        var discounts = new List<Discount>();
        foreach (var rule in rules)
        {
            if (rule.Criteria.All(c => c.IsEligible(sale)))
            {
                discounts.Add(rule.Strategy.GetDiscount(sale));
            }
        }
        return discounts;
    }
}