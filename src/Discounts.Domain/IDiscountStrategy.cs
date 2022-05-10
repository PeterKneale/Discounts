using Discounts.Domain.ValueTypes;

namespace Discounts.Domain;

public interface IDiscountStrategy
{
    Discount GetDiscount(Sale sale);
}