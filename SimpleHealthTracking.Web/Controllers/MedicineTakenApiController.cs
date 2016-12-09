namespace SimpleHealthTracking.Web.Controllers
{
    using Repository.Entities;
    using Repository.Factories;
    using Repository.DTO;
    using Repository;
    using ViewModels;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class MedicineTakenApiController : ApiController
    {
        ISimpleHealthTrackingRepository repository;
        MedicineTakenFactory medicineTakenFactory = new MedicineTakenFactory();

        public MedicineTakenApiController()
        {
            repository = new SimpleHealthTrackingRepository(new SimpleHealthTrackerContext());
        }

        public MedicineTakenApiController(ISimpleHealthTrackingRepository repo)
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

        [Authorize]
        [HttpGet]
        public IHttpActionResult IsMedicineTakenForDate(DateTime date)
        {
            bool isAllMedicineTakenForDate = false;
            string userId = User.Identity.GetUserId();
            List<Medicine> medicinesForUser = repository.GetActiveMedicineForUser(userId).ToList();
            List<MedicineTaken> medicineTakenToday = repository.GetMedicineTakenByUserForDate(userId, date).ToList();

            foreach (var medicine in medicinesForUser)
            {
                int medsTaken = medicineTakenToday.Where(mt => mt.MedicineId == medicine.Id).Count();

                if (medicine.NumberOfTimesPerDay == medsTaken)
                {
                    isAllMedicineTakenForDate = true;
                }
                else
                {
                    return Json(false);
                }
            }

            return Json(isAllMedicineTakenForDate);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<MedicineTakenNotificationDto> GetNotifications()
        {
            string userId = User.Identity.GetUserId();
            TimeZoneInfo cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime centralDate = TimeZoneInfo.ConvertTime(DateTime.Now, cst);
            List<Medicine> medicinesForUser = repository.GetActiveMedicineForUser(userId).ToList();
            List<MedicineTaken> medicineTakenToday = repository.GetMedicineTakenByUserForDate(userId, centralDate.Date).ToList();
            List<MedicineTakenNotificationDto> notifications = new List<MedicineTakenNotificationDto>();

            foreach (var medicine in medicinesForUser)
            {
                int medsTaken = medicineTakenToday.Where(mt => mt.MedicineId == medicine.Id).Count();

                if (medsTaken < medicine.NumberOfTimesPerDay)
                {
                    notifications.Add(new MedicineTakenNotificationDto
                    {
                        MedicineName = medicine.Name,
                        NumberCompleted = medsTaken,
                        NumberNeeded = medicine.NumberOfTimesPerDay
                    });
                }
            }

            return notifications;
        }
    }
}