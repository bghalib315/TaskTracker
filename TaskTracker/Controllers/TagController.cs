using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        // هنا دالة GetTenantId
        private string GetTenantId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
        }
    }
}
