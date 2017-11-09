using FitDiary.Contracts.DTOs.User;
using FitDiary.SecuredApi.Models;
using FitDiary.SecuredApi.Services.User;
using FitDiary.SecuredApi.User.DAL;
using FitDiary.SecuredApi.User.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FitDiary.SecuredApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly UsersService _userSrv = new UsersService();
        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository(new ApplicationDbContext());
        }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/FoodProducts
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<ApplicationUser> GetUserDataAsync()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var userData = _userRepository.GetUserData(userId);

            return userData;
        }

        [HttpGet]
        [Route("goals")]
        public async Task<BodyGoalsDTO> GetUserBodyGoals()
        {
            //var userId = HttpContext.Current.User.Identity.GetUserId();
            var userData = await _userSrv.GetUserBodyGoals(2);

            return userData;
        }

        [HttpGet]
        [Route("full")]
        public async Task<UserFullInfoDTO> GetUserFullInfo()
        {
            //var userId = HttpContext.Current.User.Identity.GetUserId();
            var userData = await _userSrv.GetUserFullInfo(2);

            return userData;
        }
    }
}
