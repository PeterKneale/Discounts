using Discounts.Domain.EligibilityCriteria;
using System;
using System.Globalization;
using Discounts.Domain.ValueTypes;
using FluentAssertions;
using Xunit;

namespace Discounts.Domain.UnitTests.EligibilityCriteria
{
    public class DateRangeTests
    {
        [Theory]
        [InlineData("2020-01-01","2022-01-01", "2021-01-01", true, "date is in range")]
        [InlineData("2020-01-01","2022-01-01", "2023-01-01", false, "date is outside range")]
        public void IsEligibleTests(string fromDate, string toDate, string saleDate, bool expected, string because)
        {
            // Arrange
            var dtFromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dtToDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dtSaleDate = DateTime.ParseExact(saleDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var sale = Sale.CreateInstance(Money.CreateAmount(1), dtSaleDate);
            var criteria = new DateRange(dtFromDate, dtToDate);

            // Act
            var result = criteria.IsEligible(sale);

            // Assert
            result.Should().Be(expected, because);
        }
    }
}