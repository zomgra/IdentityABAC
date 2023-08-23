using Domain.Entities;

namespace Domain.Authorization
{
    public interface IIntentionResolver
    {
    }

    public interface IIntentionResolver<TIntention> : IIntentionResolver
    {
        bool IsAllowed(User subject, TIntention intention);
    }
}
