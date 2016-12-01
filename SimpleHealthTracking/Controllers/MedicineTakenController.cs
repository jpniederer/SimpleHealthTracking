namespace SimpleHealthTracking.Controllers
{
    using SimpleHealthTracking.Repository;
    using SimpleHealthTracking.Repository.Entities;
    using SimpleHealthTracking.Repository.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Net;

    public class MedicineTakenController : ApiController
    {
        ISimpleHealthTrackingRepository repository;
        MedicineTakenFactory medicineTakenFactory = new MedicineTakenFactory();

        public MedicineTakenController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        public MedicineTakenController(ISimpleHealthTrackingRepository repo)
        {
            repository = repo;
        }

        [Route("api/MedicineTaken/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = repository.DeleteMedicineTaken(id);

                if (result.Status == ActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == ActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}