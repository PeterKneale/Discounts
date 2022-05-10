using Discounts.Domain.EligibilityCriteria;

namespace Discounts.Domain;

public interface IDiscountRulesRepository
{
    Task<IEnumerable<IDiscountRule>> GetAllAsync();
}