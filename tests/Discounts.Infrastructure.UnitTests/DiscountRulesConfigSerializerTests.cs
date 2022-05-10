using System;
using Discounts.Infrastructure.Config;
using Discounts.Infrastructure.Config.DiscountStrategies;
using Discounts.Infrastructure.Config.EligibilityCriteria;
using Discounts.Infrastructure.Repositories;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Discounts.Infrastructure.UnitTests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void CanSerializeAndDeserialize()
    {
        // assert
        var config = new DiscountRulesConfig
        {
            Discounts = new[]
            {
                new DiscountRuleConfig
                {
                    Name = "$5 off today only",
                    Eligibility = new[]
                    {
                        new DateConfig
                        {
                            Date = DateTime.Today
                        }
                    },
                    Strategy = new FlatAmountConfig
                    {
                        Amount = 5
                    }
                },
                new DiscountRuleConfig
                {
                    Name = "5% off this week only",
                    Eligibility = new[]
                    {
                        new DateRangeConfig
                        {
                            From = DateTime.Today,
                            To = DateTime.Today.AddDays(7),
                        }
                    },
                    Strategy = new FlatPercentageConfig
                    {
                        Amount = 5
                    }
                }
            },
        };

        // act
        var serializer = new DiscountRulesConfigSerializer();
        var json = serializer.Serialize(config);
        var loaded = serializer.Deserialize(json);

        // assert
        _output.WriteLine(json);
        config.Should().BeEquivalentTo(loaded);
    }
}