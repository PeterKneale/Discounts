using Grpc.Core;
using Discounts.Application;
using MediatR;

namespace Discounts.Api.Services;

public class GrpcDiscountsService : DiscountsService.DiscountsServiceBase
{
    private readonly IMediator _mediator;
    private static string _currency = "AUD";

    public GrpcDiscountsService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetDiscountsReply> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
    {
        var query = new GetDiscounts.Query(request.SaleAmount.DecimalValue, request.SaleDate.ToDateTime());
        var result = await _mediator.Send(query);
        return new GetDiscountsReply
        {
            Discounts = {result.Discounts.Select(Map)}
        };
    }

    private static DiscountDto Map(GetDiscounts.Discount discount) => new DiscountDto
    {
        Amount = new Google.Type.Money
        {
            CurrencyCode = _currency,
            DecimalValue = discount.Amount
        },
        Description = discount.Description
    };
}