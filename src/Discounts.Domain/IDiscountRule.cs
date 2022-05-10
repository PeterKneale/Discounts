using Discounts.Domain.DiscountStrategies;
using Discounts.Domain.EligibilityCriteria;

namespace Discounts.Domain;

public interface IDiscountRule
{
    string Name { get; }
    IEnumerable<IEligibilityCriteria> Criteria { get; }
    IDiscountStrategy Strategy { get; }
}