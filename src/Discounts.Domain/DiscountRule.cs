using Discounts.Domain.DiscountStrategies;
using Discounts.Domain.EligibilityCriteria;

namespace Discounts.Domain;

public class DiscountRule : IDiscountRule
{
    public DiscountRule(string name, IEnumerable<IEligibilityCriteria> criteria, IDiscountStrategy strategy)
    {
        Name = name;
        Criteria = criteria;
        Strategy = strategy;
    }
    public string Name { get; }
    public IEnumerable<IEligibilityCriteria> Criteria { get; }
    public IDiscountStrategy Strategy { get; }
}