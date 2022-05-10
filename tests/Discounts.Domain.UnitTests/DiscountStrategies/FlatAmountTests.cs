using Discounts.Domain.DiscountStrategies;
using Discounts.Domain.ValueTypes;
using System;
using FluentAssertions;
using Xunit;

namespace Discounts.Domain.UnitTests.DiscountStrategies
{
    public class FlatAmountTests
    {
        [Theory]
        [InlineData(100, 10, 10,"10 off 100 is 10")]
        [InlineData(10, 10, 10,"10 off 10 is 0")]
        [InlineData(100, 200, 0,"200 off 100 -100 but is limited to 0")]
        public void GetDiscountTests(decimal price, decimal discount, decimal expected, string because)
        {
            // Arrange
            var sale = Sale.CreateInstance(Money.CreateAmount(price), DateTime.Today);
            var strategy = new FlatAmount(Money.CreateAmount(discount));

            // Act
            var result = strategy.GetDiscount(sale);

            // Assert
            result.Amount.Should().BeEquivalentTo(Money.CreateAmount(expected),because);
        }
    }
}