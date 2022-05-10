using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.DiscountStrategies;

public interface IDiscountStrategy
{
    Discount GetDiscount(Sale sale);
}