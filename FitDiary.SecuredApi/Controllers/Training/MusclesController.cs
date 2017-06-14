using FitDiary.Contracts.DTOs.Training;
using FitDiary.SecuredApi.Services.Training;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FitDiary.SecuredApi.Controllers.Training
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/muscles")]
    public class MusclesController : ApiController
    {
        private readonly MusclesService _musclesService = new MusclesService(); //TODO DI

        // GET: api/muscles
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<MuscleDTO>))]
        public async Task<IEnumerable<MuscleDTO>> GetMusclesAsync()
        {
            return await _musclesService.GetMusclesAsync();
        }

        // GET: api/muscles/5
        [HttpGet]
        [Route("{muscleId:int}")]
        [ResponseType(typeof(MuscleDTO))]
        public async Task<MuscleDTO> GetMuscleAsync(int muscleId)
        {
            return await _musclesService.GetMuscleAsync(muscleId);
        }
    }
}
