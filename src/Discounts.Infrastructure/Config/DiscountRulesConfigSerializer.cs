using Discounts.Infrastructure.Config.DiscountStrategies;
using Discounts.Infrastructure.Config.EligibilityCriteria;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Discounts.Infrastructure.Config;

public interface IDiscountRulesConfigSerializer
{
    DiscountRulesConfig Deserialize(string json);
    string Serialize(DiscountRulesConfig config);
}
public class DiscountRulesConfigSerializer : IDiscountRulesConfigSerializer
{
    private readonly JsonSerializerSettings _settings;
    
    public DiscountRulesConfigSerializer()
    {
        _settings = new JsonSerializerSettings();
        
        _settings.Converters.Add(JsonSubtypesConverterBuilder
            .Of(typeof(EligibilityCriteriaConfig), "Type")
            .RegisterSubtype(typeof(DateConfig), nameof(DateConfig))
            .RegisterSubtype(typeof(DateRangeConfig), nameof(DateRangeConfig))
            .SerializeDiscriminatorProperty()
            .Build());
        
        _settings.Converters.Add(JsonSubtypesConverterBuilder
            .Of(typeof(DiscountStrategyConfig), "Type")
            .RegisterSubtype(typeof(FlatAmountConfig), nameof(FlatAmountConfig))
            .RegisterSubtype(typeof(FlatPercentageConfig), nameof(FlatPercentageConfig))
            .SerializeDiscriminatorProperty()
            .Build());
        
        _settings.Formatting = Formatting.Indented;
    }
    
    public DiscountRulesConfig Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<DiscountRulesConfig>(json, _settings);
    }
    
    public string Serialize(DiscountRulesConfig config)
    {
        return JsonConvert.SerializeObject(config, _settings);
    }
}