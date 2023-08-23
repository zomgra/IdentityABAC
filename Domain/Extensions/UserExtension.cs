using Domain.Entities;

namespace Domain.Extensions
{
    public static class UserExtension
    {
        public static bool IsAuthenticated(this User user) => user != null && user?.UserId != Guid.Empty && !string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Email);
    }
}
