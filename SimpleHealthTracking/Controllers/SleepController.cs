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

    public class SleepController : ApiController
    {
        ISimpleHealthTrackerRepository repository;
        SleepFactory sleepFactory = new SleepFactory();

        const int maxPageSize = 10;

        public SleepController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public SleepController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }

        [Route("sleep/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = repository.DeleteSleep(id);

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