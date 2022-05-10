namespace Discounts.Domain.ValueTypes;

public class Sale
{
    private Sale(Money price)
    {
        Price = price;
    }
    public Money Price { get; }

    public static Sale CreateInstance(Money price) => new(price);
}