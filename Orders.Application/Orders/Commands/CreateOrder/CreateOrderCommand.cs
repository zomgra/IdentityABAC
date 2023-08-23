using MediatR;

namespace Orders.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(string DiscountCode,ICollection<Guid> OrderItemsIds) : IRequest<Guid>
    {
    }
}