using Domain.Abstract;
using Domain.Authentication;
using Domain.Authorization;
using Domain.Entities;
using Domain.Extensions;
using Domain.Intentions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Orders.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IIntentionManager _intentionManager;
        private readonly IAppDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;

        public CreateOrderCommandHandler(IIntentionManager intentionManager,
            IAppDbContext dbContext,
            IIdentityProvider identityProvider)
        {
            _intentionManager = intentionManager;
            _dbContext = dbContext;
            _identityProvider = identityProvider;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _intentionManager.ThrowIfForbidden(OrderIntention.Create);

            var orderItems = await _dbContext.OrderItems.Where(x => request.OrderItemsIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var order = new Order()
            {
                UserId = _identityProvider.CurentUser.UserId,
                DiscountCode = request.DiscountCode,
            };
            order.OrderItems = orderItems;
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return order.Id;
        }
    }
}
