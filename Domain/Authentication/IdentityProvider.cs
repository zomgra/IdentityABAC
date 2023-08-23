using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Authentication
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public IdentityProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public User CurentUser
        {
            get
            {
                // Other logic
                return GetUser();
            }
        }

        private User GetUser()
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = new User()
            {
                Email = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
                UserId = Guid.Parse(userId),
                Scopes = _contextAccessor.HttpContext.User.FindFirstValue("scope"),
                Name = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name),
                Roles = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role),
            };
            return user;
        }
    }
}
