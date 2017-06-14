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
    [RoutePrefix("api/workouts")]
    public class WorkoutsController : ApiController
    {
        private readonly WorkoutsService _workoutsService = new WorkoutsService(); //TODO DI

        // GET: api/workouts
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<WorkoutDTO>))]
        public async Task<IEnumerable<WorkoutDTO>> GetWorkoutsAsync()
        {
            return await _workoutsService.GetWorkoutsAsync();
        }

        // GET: api/workouts/5
        [HttpGet]
        [Route("{workoutId:int}")]
        [ResponseType(typeof(WorkoutDTO))]
        public async Task<WorkoutDTO> GetWorkoutAsync(int workoutId)
        {
            return await _workoutsService.GetWorkoutAsync(workoutId);
        }
    }
}
