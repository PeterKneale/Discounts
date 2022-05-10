namespace Discounts.Domain.ValueTypes;

public class Percentage
{
    private Percentage(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Percentage cannot be negative");
        }
        if (value > 100)
        {
            throw new ArgumentException("Percentage cannot be greater than 100");
        }
        Value = value;
    }
    public decimal Value { get; }

    public static Percentage Create(decimal value) => new(value);
}