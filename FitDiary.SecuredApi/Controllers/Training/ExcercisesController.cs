using FitDiary.Contracts.DTOs.Training;
using FitDiary.SecuredApi.Models.Training;
using FitDiary.SecuredApi.Services.Training;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FitDiary.SecuredApi.Controllers.Training
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/excercises")]
    public class ExcercisesController : ApiController
    {
        private readonly ExcercisesService _excercisesService = new ExcercisesService();

        // GET: api/FoodProducts
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<ExcerciseForListingDTO>))]
        public async Task<IEnumerable<ExcerciseForListingDTO>> GetExcercisesAsync()
        {
            return await _excercisesService.GetExcercisesAsync();
        }

        // GET: api/FoodProducts
        [HttpGet]
        [Route("{mainMuscleId:int}")]
        [ResponseType(typeof(IEnumerable<ExcerciseForListingDTO>))]
        public async Task<IEnumerable<ExcerciseForListingDTO>> GetExcercisesAsync(int mainMuscleId, [FromUri] ExcerciseQueryParams queryParams)
        {
            return await _excercisesService.GetExcercisesByMainMuscleAsync(mainMuscleId, queryParams ?? new ExcerciseQueryParams());
        }
    }
}