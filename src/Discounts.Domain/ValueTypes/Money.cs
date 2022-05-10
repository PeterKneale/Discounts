namespace Discounts.Domain.ValueTypes;

public class Money
{
    private Money(decimal value)
    {
        Value = value;
    }
    public decimal Value { get; }

    public static Money Zero => CreateAmount(0);
    public static Money CreateAmount(decimal value) => new(value);
}