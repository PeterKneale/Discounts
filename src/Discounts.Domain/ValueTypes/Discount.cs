namespace Discounts.Domain.ValueTypes;

public class Discount
{
    private Discount(Money amount, string description)
    {
        Amount = amount;
        Description = description;
    }

    public Money Amount { get; }
    public string Description { get; }

    public static Discount None => new(Money.Zero, "No discount");
    public static Discount Create(Money discount, string description) => new(discount, description);
}