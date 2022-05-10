using System;
using System.Globalization;
using Discounts.Domain.EligibilityCriteria;
using Discounts.Domain.ValueTypes;
using FluentAssertions;
using Xunit;

namespace Discounts.Domain.UnitTests.EligibilityCriteria
{
    public class DateTests
    {
        [Theory]
        [InlineData("2022-01-01", "2022-01-01", true, "date is within")]
        [InlineData("2021-01-01", "2022-01-01", false, "dates is not within")]
        public void IsEligibleTests(string ruleDate, string saleDate, bool expected, string because)
        {
            // Arrange
            var dtDate = DateTime.ParseExact(ruleDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dtSaleDate = DateTime.ParseExact(saleDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var sale = Sale.CreateInstance(Money.CreateAmount(1), dtSaleDate);
            var criteria = new Date(dtDate);

            // Act
            var result = criteria.IsEligible(sale);

            // Assert
            result.Should().Be(expected, because);
        }
    }
}