using Discounts.Domain.DiscountStrategies;
using System;
using Discounts.Domain.ValueTypes;
using FluentAssertions;
using Xunit;

namespace Discounts.Domain.UnitTests.DiscountStrategies
{
    public class FlatPercentageTests
    {
        [Theory]
        [InlineData(100, 10, 10,"10% off 100 is 10")]
        [InlineData(10, 100, 10,"100% off 10 is 10")]
        [InlineData(100, 10.5, 10.50,"10.5% off 100 is 10.50")]
        public void GetDiscountTests(decimal price, decimal percent, decimal expected, string because)
        {
            // Arrange
            var sale = Sale.CreateInstance(Money.CreateAmount(price));
            var strategy = new  FlatPercentage(Percentage.Create(percent));

            // Act
            var result = strategy.GetDiscount(sale);

            // Assert
            result.Amount.Should().BeEquivalentTo(Money.CreateAmount(expected),because);
        }
    }
}
