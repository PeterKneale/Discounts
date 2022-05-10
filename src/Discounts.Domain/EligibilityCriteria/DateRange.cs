using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.EligibilityCriteria;

public class DateRange : IEligibilityCriteria
{
    private readonly DateTime _from;

    private readonly DateTime _to;

    public DateRange(DateTime from, DateTime to)
    {
        if (from >= to)
            throw new ArgumentException("From date must be before to date");
        _from = from;
        _to = to;
    }

    public bool IsEligible(Sale sale)
    {
        return _from.Date <= sale.Date && sale.Date <= _to.Date;
    }
}