using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private int _UserLoggedId;
        protected int UserLoggedId
        {
            get
            {
                if (_UserLoggedId == 0)
                {
                    var claims = User.Claims.FirstOrDefault(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                    if (claims != null)
                    {
                        _UserLoggedId = int.Parse(claims.Value);
                    }
                }

                return _UserLoggedId;
            }

        }
    }
}