namespace SimpleHealthTracking.Web.Controllers
{
    using SimpleHealthTracking.Repository.Entities;
    using SimpleHealthTracking.Repository.DTO;
    using SimpleHealthTracking.Repository.Factories;
    using SimpleHealthTracking.Repository;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize]
    public class MedicineTakenApiController : ApiController
    {
        ISimpleHealthTrackerRepository repository;
        MedicineTakenFactory medicineTakenFactory = new MedicineTakenFactory();

        public MedicineTakenApiController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public MedicineTakenApiController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }

        [HttpPost]
        public IHttpActionResult MedicineTaken(MedicineTakenDto mtd)
        {
            var userId = User.Identity.GetUserId();
            Medicine medicine = repository.GetMedicine(mtd.MedicineId);
            
            if (medicine == null)
            {
                return BadRequest("No such medicine exists.");
            }

            var medicineTakenForDate = repository.GetMedicineTakenByMedicineIdByDate(mtd.MedicineId,
                mtd.DateAddedFor).ToList();

            if (medicineTakenForDate.Count >= medicine.NumberOfTimesPerDay)
            {
                return BadRequest(String.Format("You've already taken {0} of {1} today.", 
                    medicine.NumberOfTimesPerDay, 
                    medicine.Name));
            }

            var medicineTaken = new MedicineTaken
            {
                MedicineId = mtd.MedicineId,
                DateAddedFor = mtd.DateAddedFor,
                TimeAdded = DateTime.Now
            };

            repository.InsertMedicineTaken(medicineTaken);

            return Ok();
        }
    }
}