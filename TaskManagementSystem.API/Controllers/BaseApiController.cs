using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected int LoggedInUserId
        {
            get
            {
                if (HttpContext.Items.TryGetValue("LoggedInUserId", out var value) &&
                    value is int userId)
                {
                    return userId;
                }

                throw new Exception("LoggedInUserId not found in request context");
            }
        }

        protected UserRole LoggedInUserRole
        {
            get
            {
                if (HttpContext.Items.TryGetValue("LoggedInUserRole", out var value) &&
                    value is UserRole role)
                {
                    return role;
                }

                throw new Exception("LoggedInUserRole not found in request context");
            }
        }
    }
}
