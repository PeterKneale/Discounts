namespace Discounts.Infrastructure.Config.DiscountStrategies;

public class FlatPercentageConfig : DiscountStrategyConfig
{
    public decimal Amount { get; set; }
}