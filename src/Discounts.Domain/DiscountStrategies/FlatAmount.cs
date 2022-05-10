using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.DiscountStrategies;

public class FlatAmount : IDiscountStrategy
{
    private readonly Money _amount;
    
    public FlatAmount(Money amount)
    {
        _amount = amount;
    }

    public Discount GetDiscount(Sale sale)
    {
        return sale.Price.Value < _amount.Value
            ? Discount.None 
            : Discount.Create(_amount, $"{_amount:C} off");
    }
}