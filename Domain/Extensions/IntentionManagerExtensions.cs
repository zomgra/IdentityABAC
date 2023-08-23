using Domain.Authorization;
using Domain.Exceptions;

namespace Domain.Extensions
{
    public static class IntentionManagerExtensions
    {
        public static async void ThrowIfForbidden<TIntention>(this IIntentionManager intentionManager, TIntention intention)
            where TIntention : struct
        {
            if (!await intentionManager.IsAllowed(intention))
            {
                throw new IntentionManagerException();
            }
        }
    }
}
