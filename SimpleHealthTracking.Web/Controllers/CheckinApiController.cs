﻿namespace SimpleHealthTracking.Web.Controllers
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
    public class CheckinApiController : ApiController
    {
        ISimpleHealthTrackerRepository repository;
        CheckinFactory checkinFactory = new CheckinFactory();

        public CheckinApiController()
        {
            repository = new SimpleHealthTrackerRepository(new SimpleHealthTrackerContext());
        }

        public CheckinApiController(ISimpleHealthTrackerRepository repo)
        {
            repository = repo;
        }

        [HttpPost]
        public IHttpActionResult AddCheckin(CheckinDto checkinDto)
        {
            var userId = User.Identity.GetUserId();
            Checkin checkin = checkinFactory.CreateCheckin(checkinDto);

            checkin.TimeAdded = DateTime.Now;
            checkin.UpdateTime = DateTime.Now;
            repository.InsertCheckin(checkin);

            return Ok();
        }
    }
}