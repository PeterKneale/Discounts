using Discounts.Domain;
using Discounts.Domain.Services;
using Discounts.Domain.ValueTypes;
using FluentValidation;
using MediatR;

namespace Discounts.Application;

public static class GetDiscounts
{
    public record Query(decimal SalePrice, DateTime SaleDate) : IRequest<Response> {}
    public record Response(IEnumerable<Discount> Discounts);
    public record Discount(decimal Amount, string Description);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.SalePrice).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IDiscountService _service;
        public Handler(IDiscountService service)
        {
            _service = service;
        }
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var salePrice = Money.CreateAmount(request.SalePrice);
            var saleDate = request.SaleDate;

            var sale = Sale.CreateInstance(salePrice, saleDate);
            var discounts = await _service.GetDiscountsAsync(sale);
            
            var dtos = discounts.Select(d => new Discount(d.Amount.Value, d.Description));
            return new Response(dtos);
        }
    }
}