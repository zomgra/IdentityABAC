using Domain.Entities;

namespace Domain.Authentication
{
    public interface IIdentityProvider
    {
        User CurentUser { get; }
    }
}
