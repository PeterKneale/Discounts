using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.DiscountStrategies;

public class FlatPercentage : IDiscountStrategy
{
    private readonly Percentage _percentage;
    
    public FlatPercentage(Percentage percentage)
    {
        _percentage = percentage;
    }

    public Discount GetDiscount(Sale sale)
    {
        var amount = sale.Price.Value * _percentage.Value / 100;
        return Discount.Create(Money.CreateAmount(amount), $"{amount:C} off");
    }
}