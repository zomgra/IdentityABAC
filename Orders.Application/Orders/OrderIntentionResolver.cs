using Domain.Authorization;
using Domain.Entities;
using Domain.Extensions;
using Domain.Intentions;

namespace Orders.Application.Orders
{
    public class OrderIntentionResolver : IIntentionResolver<OrderIntention>
    {
        public bool IsAllowed(User subject, OrderIntention intention)
        {
            var result = intention switch
            {
                OrderIntention.Create => subject.IsAuthenticated(),
                _ => false,
            };
            return result;
        }
    }
}
