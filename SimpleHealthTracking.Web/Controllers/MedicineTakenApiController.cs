namespace SimpleHealthTracking.Web.Controllers
{
    using SimpleHealthTracking.Repository.Entities;
    using SimpleHealthTracking.Repository.DTO;
    using SimpleHealthTracking.Repository.Factories;
    using SimpleHealthTracking.Repository;
    using Web.ViewModels;
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

        [Authorize]
        [HttpPost]
        public IHttpActionResult AddMedicineTaken(MedicineTakenViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            MedicineTaken medicineTaken = new MedicineTaken
            {
                MedicineId = viewModel.MedicineId,
                DateAddedFor = viewModel.GetDateTimeAddedFor(),
                TimeAdded = DateTime.Now
            };

            repository.InsertMedicineTaken(medicineTaken);
            return Ok();
        }
    }
}