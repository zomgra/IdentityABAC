using Domain.Authentication;

namespace Domain.Authorization
{
    public interface IIntentionManager
    {
        Task<bool> IsAllowed<TIntention>(TIntention intention) where TIntention : struct;
    }
    public class IntentionManager : IIntentionManager
    {
        private readonly IEnumerable<IIntentionResolver> _resolvers;
        private readonly IIdentityProvider identityProvider;

        public IntentionManager(IEnumerable<IIntentionResolver> resolvers,
            IIdentityProvider identityProvider)
        {
            _resolvers = resolvers;
            this.identityProvider = identityProvider;
        }

        public async Task<bool> IsAllowed<TIntention>(TIntention intention) where TIntention : struct
        {
            //await Task.Yield();
            var matchingResolver = _resolvers.OfType<IIntentionResolver<TIntention>>().First();
            return matchingResolver?.IsAllowed(identityProvider.CurentUser, intention) ?? false;
        }
    }
}
