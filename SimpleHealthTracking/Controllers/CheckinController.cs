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


    }
}