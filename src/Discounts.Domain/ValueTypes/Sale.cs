namespace Discounts.Domain.ValueTypes;

public class Sale
{
    private Sale(Money price, DateTime date)
    {
        Price = price;
        Date = date;
    }
    public Money Price { get; }
    public DateTime Date { get; }

    public static Sale CreateInstance(Money price, DateTime date) => new(price, date);
}