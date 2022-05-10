using Discounts.Domain.Services;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discounts.Domain.DiscountStrategies;
using Discounts.Domain.EligibilityCriteria;
using Discounts.Domain.ValueTypes;
using FluentAssertions;
using Xunit;

namespace Discounts.Domain.UnitTests.Services
{
    public class DiscountServiceTests
    {
        private readonly IDiscountService _service;
        private readonly Mock<IDiscountRulesRepository> _repository;
        private readonly Sale _sale;

        public DiscountServiceTests()
        {
            _repository = new Mock<IDiscountRulesRepository>();
            _service = new DiscountService(_repository.Object);
            _sale = Sale.CreateInstance(Money.CreateAmount(100), DateTime.Now);
        }

        [Fact]
        public async Task When_criteria_is_eligible_then_discount_should_be_applied()
        {
            // arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new[]
            {
                new DiscountRule("x", new[] {new AlwaysEligible()}, new OneDollarDiscountStrategy())
            });

            // act
            var results = await _service.GetDiscountsAsync(_sale);

            // assert
            results.Should().HaveCount(1);
            results.Sum(x => x.Amount.Value).Should().Be(1);
        }
        
        [Fact]
        public async Task When_multiple_discount_rules_are_eligible_then_discount_should_accumulate()
        {
            // arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new[]
            {
                new DiscountRule("x1", new[] {new AlwaysEligible()}, new OneDollarDiscountStrategy()),
                new DiscountRule("x2", new[] {new AlwaysEligible()}, new OneDollarDiscountStrategy())
            });

            // act
            var results = await _service.GetDiscountsAsync(_sale);

            // assert
            results.Should().HaveCount(2);
            results.Sum(x => x.Amount.Value).Should().Be(2);
        }

        [Fact]
        public async Task When_criteria_is_not_eligible_then_discount_should_not_be_applied()
        {
            // arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new[]
            {
                new DiscountRule("x", new[] {new NotEligible()}, new OneDollarDiscountStrategy())
            });

            // act
            var results = await _service.GetDiscountsAsync(_sale);

            // assert
            results.Should().BeEmpty();
        }

        [Fact]
        public async Task When_multiple_criteria_are_eligible_then_discounts_should_be_applied()
        {
            // arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new[]
            {
                new DiscountRule("x1", new[] {new AlwaysEligible(), new AlwaysEligible()}, new OneDollarDiscountStrategy())
            });

            // act
            var results = await _service.GetDiscountsAsync(_sale);

            // assert
            results.Should().HaveCount(1);
            results.Sum(x => x.Amount.Value).Should().Be(1);
        }

        [Fact]
        public async Task When_one_of_multiple_criteria_is_ineligible_then_that_discount_should_not_be_applied()
        {
            // arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(new[]
            {
                new DiscountRule("x1", new IEligibilityCriteria[]
                    {
                        new AlwaysEligible(), 
                        new NotEligible(), 
                        new AlwaysEligible()
                    },
                    new OneDollarDiscountStrategy()),
            });

            // act
            var results = await _service.GetDiscountsAsync(_sale);

            // assert
            results.Should().BeEmpty();
        }
    }
    class AlwaysEligible : IEligibilityCriteria
    {
        public bool IsEligible(Sale sale) => true;
    }

    class NotEligible : IEligibilityCriteria
    {
        public bool IsEligible(Sale sale) => false;
    }
    class OneDollarDiscountStrategy : IDiscountStrategy
    {
        public Discount GetDiscount(Sale sale) => Discount.Create(Money.CreateAmount(1), "X");
    }
}