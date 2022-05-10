using Discounts.Domain.ValueTypes;

namespace Discounts.Domain;

public interface IDiscountService
{
    Task<IEnumerable<Discount>> GetDiscountsAsync(Sale sale);
}