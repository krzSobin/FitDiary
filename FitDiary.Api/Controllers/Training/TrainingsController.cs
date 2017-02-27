﻿using System.Data.Entity;
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
    public class TrainingsController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/Trainings
        public IQueryable<Training> GetTrainings()
        {
            return db.Trainings;
        }

        // GET: api/Trainings/5
        [ResponseType(typeof(Training))]
        public async Task<IHttpActionResult> GetTraining(int id)
        {
            Training training = await db.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        // PUT: api/Trainings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTraining(int id, Training training)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != training.Id)
            {
                return BadRequest();
            }

            db.Entry(training).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(id))
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

        // POST: api/Trainings
        [ResponseType(typeof(Training))]
        public async Task<IHttpActionResult> PostTraining(Training training)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trainings.Add(training);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = training.Id }, training);
        }

        // DELETE: api/Trainings/5
        [ResponseType(typeof(Training))]
        public async Task<IHttpActionResult> DeleteTraining(int id)
        {
            Training training = await db.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            db.Trainings.Remove(training);
            await db.SaveChangesAsync();

            return Ok(training);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainingExists(int id)
        {
            return db.Trainings.Count(e => e.Id == id) > 0;
        }
    }
}