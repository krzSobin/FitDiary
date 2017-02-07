using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitDiary.Api.Models;
using FitDiary.Api.DAL;

namespace FitDiary.Api.Controllers
{
    [RoutePrefix("api/excerciseCategories")]
    public class ExcerciseCategoriesController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/ExcerciseCategories
        public IQueryable<ExcerciseCategory> GetExcerciseCategories()
        {
            return db.ExcerciseCategories;
        }

        // GET: api/ExcerciseCategories/5
        [ResponseType(typeof(ExcerciseCategory))]
        public async Task<IHttpActionResult> GetExcerciseCategory(int id)
        {
            ExcerciseCategory excerciseCategory = await db.ExcerciseCategories.FindAsync(id);
            if (excerciseCategory == null)
            {
                return NotFound();
            }

            return Ok(excerciseCategory);
        }

        // PUT: api/ExcerciseCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutExcerciseCategory(int id, ExcerciseCategory excerciseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != excerciseCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(excerciseCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExcerciseCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ExcerciseCategories
        [ResponseType(typeof(ExcerciseCategory))]
        public async Task<IHttpActionResult> PostExcerciseCategory(ExcerciseCategory excerciseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExcerciseCategories.Add(excerciseCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = excerciseCategory.Id }, excerciseCategory);
        }

        // DELETE: api/ExcerciseCategories/5
        [ResponseType(typeof(ExcerciseCategory))]
        public async Task<IHttpActionResult> DeleteExcerciseCategory(int id)
        {
            ExcerciseCategory excerciseCategory = await db.ExcerciseCategories.FindAsync(id);
            if (excerciseCategory == null)
            {
                return NotFound();
            }

            db.ExcerciseCategories.Remove(excerciseCategory);
            await db.SaveChangesAsync();

            return Ok(excerciseCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExcerciseCategoryExists(int id)
        {
            return db.ExcerciseCategories.Count(e => e.Id == id) > 0;
        }
    }
}