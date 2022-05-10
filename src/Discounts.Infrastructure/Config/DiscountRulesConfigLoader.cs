using Discounts.Domain;
using Discounts.Domain.DiscountStrategies;
using Discounts.Domain.EligibilityCriteria;
using Discounts.Domain.ValueTypes;
using Discounts.Infrastructure.Config.DiscountStrategies;
using Discounts.Infrastructure.Config.EligibilityCriteria;
using Discounts.Infrastructure.Repositories;

namespace Discounts.Infrastructure.Config;

public interface IDiscountRulesConfigLoader
{
    IEnumerable<IDiscountRule> GetDiscountRules();
}

public class DiscountRulesConfigLoader : IDiscountRulesConfigLoader
{
    private readonly IDiscountRulesConfigSerializer _serializer;

    public DiscountRulesConfigLoader(IDiscountRulesConfigSerializer serializer)
    {
        _serializer = serializer;
    }

    public IEnumerable<IDiscountRule> GetDiscountRules()
    {
        var json = File.ReadAllText("discounts.json");
        var config = _serializer.Deserialize(json);
        var discounts = new List<IDiscountRule>();
        foreach (var discount in config.Discounts)
        {
            discounts.Add(GetDiscountRule(discount));
        }
        return discounts;
    }
    private static DiscountRule GetDiscountRule(DiscountRuleConfig discount)
    {
        if (discount.Eligibility == null || !discount.Eligibility.Any())
        {
            throw new Exception("Eligibility is not set");
        }
        if (discount.Strategy == null)
        {
            throw new Exception("Strategy is not set");
        }
        var eligibility = GetEligibilityCriteria(discount);
        var strategy = GetDiscountStrategy(discount);
        var rule = new DiscountRule(discount.Name, eligibility, strategy);
        return rule;
    }
    private static IEnumerable<IEligibilityCriteria> GetEligibilityCriteria(DiscountRuleConfig discount)
    {
        var eligibility = new List<IEligibilityCriteria>();
        foreach (var rule in discount.Eligibility)
        {
            IEligibilityCriteria criteria = rule switch
            {
                DateConfig c => new Date(c.Date),
                DateRangeConfig c => new DateRange(c.From, c.To),
                _ => throw new Exception("Eligibility Criteria is unknown")
            };
            eligibility.Add(criteria);
        }
        return eligibility;
    }
    private static IDiscountStrategy GetDiscountStrategy(DiscountRuleConfig discount)
    {
        IDiscountStrategy strategy = discount.Strategy switch
        {
            FlatAmountConfig c => new FlatAmount(Money.CreateAmount(c.Amount)),
            FlatPercentageConfig c => new FlatPercentage(Percentage.Create(c.Amount)),
            _ => throw new Exception("Strategy is unknown")
        };
        return strategy;
    }
}