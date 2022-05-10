namespace Discounts.Infrastructure.Config.EligibilityCriteria;

public class DateRangeConfig : EligibilityCriteriaConfig
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}