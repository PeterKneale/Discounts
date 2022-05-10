using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.EligibilityCriteria;

public interface IEligibilityCriteria
{
    bool IsEligible(Sale sale);
}