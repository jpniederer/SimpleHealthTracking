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

    public class MedicineController : ApiController
    {
        ISimpleHealthTrackingRepository repository;
        MedicineFactory medicineFactory = new MedicineFactory();

        public MedicineController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        public MedicineController(ISimpleHealthTrackingRepository repo)
        {
            repository = repo;
        }

        [Route("api/Medicine/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = repository.DeleteMedicine(id);

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