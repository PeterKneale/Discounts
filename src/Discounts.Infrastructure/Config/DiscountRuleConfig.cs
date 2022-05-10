using Discounts.Infrastructure.Config.DiscountStrategies;
using Discounts.Infrastructure.Config.EligibilityCriteria;

namespace Discounts.Infrastructure.Config;

public class DiscountRuleConfig
{
    public string Name { get; set; }
    
    public IEnumerable<EligibilityCriteriaConfig> Eligibility { get; set; }
    
    public DiscountStrategyConfig Strategy { get; set; }
}