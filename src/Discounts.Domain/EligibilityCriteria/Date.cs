using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.EligibilityCriteria;

public class Date : IEligibilityCriteria
{
    private readonly DateTime _date;
    
    public Date(DateTime date)
    {
        _date = date;
    }
    
    public bool IsEligible(Sale sale)
    {
        return _date.Date == sale.Date;
    }
}