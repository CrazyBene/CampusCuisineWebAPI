using System.Security.Claims;

namespace CampusCuisine.Services
{
    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserGuid()
        {
            return new Guid(httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value);
        }

    }
}