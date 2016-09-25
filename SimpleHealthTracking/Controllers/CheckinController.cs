namespace SimpleHealthTracking.Controllers
{
    using SimpleHealthTracking.Repository;
    using SimpleHealthTracking.Repository.Factories;
    using SimpleHealthTracking.Repository.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Net;

    public class CheckinController : ApiController
    {
        ISimpleHealthTrackerRepository repository;
        CheckinFactory checkinFactory = new CheckinFactory();

        public CheckinController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public CheckinController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }

        [Route("api/Checkin/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = repository.DeleteCheckin(id);

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