using FitDiary.Contracts.DTOs;
using FitDiary.SecuredApi.Services;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FitDiary.SecuredApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UsersService _userSrv = new UsersService();

        // GET: api/FoodProducts
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<UserDTO> GetUserDataAsync()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var userData = await _userSrv.GetUserData(userId);

            return userData;
        }
    }
}
