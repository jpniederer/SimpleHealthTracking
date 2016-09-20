namespace SimpleHealthTracking.Repository
{
    using Entities;
    using System;
    using System.Linq;

    public class SimpleHealthTrackerRepository : ISimpleHealthTrackerRepository
    {
        SimpleHealthTrackerContext _context;

        public SimpleHealthTrackerRepository(SimpleHealthTrackerContext context)
        {
            _context = context;
            _context.Configuration.LazyLoadingEnabled = false;
        }
    }
}