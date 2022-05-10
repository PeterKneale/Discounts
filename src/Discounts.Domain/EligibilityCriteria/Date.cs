using Discounts.Domain.ValueTypes;

namespace Discounts.Domain.EligibilityCriteria;

public class Date : IEligibilityCriteria
{
    private readonly DateTime _at;
    
    public Date(DateTime at)
    {
        _at = at;
    }
    
    public bool IsEligible(Sale sale)
    {
        return _at.Date == sale.Date;
    }
}