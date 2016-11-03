namespace SimpleHealthTracking.Repository
{
    using Entities;
    using System;
    using System.Linq;
    using System.Data.Entity;

    public class SimpleHealthTrackerRepository : ISimpleHealthTrackerRepository
    {
        SimpleHealthTrackerContext _context;

        public SimpleHealthTrackerRepository(SimpleHealthTrackerContext context)
        {
            _context = context;
            _context.Configuration.LazyLoadingEnabled = false;
        }

        // Checkins
        public ActionResult<Checkin> InsertCheckin(Checkin checkin)
        {
            try
            {
                _context.Checkins.Add(checkin);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<Checkin>(checkin, ActionStatus.Created);
                }
                else
                {
                    return new ActionResult<Checkin>(checkin, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Checkin>(null, ActionStatus.Error, ex);
            }
        }

        public ActionResult<Checkin> UpdateCheckin(Checkin checkin)
        {
            try
            {
                var currentCheckin = _context.Checkins.FirstOrDefault(c => c.Id == checkin.Id);

                if (currentCheckin == null)
                {
                    return new ActionResult<Checkin>(checkin, ActionStatus.NotFound);
                }

                _context.Entry(currentCheckin).State = EntityState.Detached;
                _context.Checkins.Attach(checkin);
                _context.Entry(checkin).State = EntityState.Modified;

                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<Checkin>(checkin, ActionStatus.Updated);
                }
                else
                {
                    return new ActionResult<Checkin>(checkin, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Checkin>(null, ActionStatus.Error, ex);
            }
        }

        public ActionResult<Checkin> DeleteCheckin(int id)
        {
            try
            {
                var currentCheckin = _context.Checkins.FirstOrDefault(c => c.Id == id);

                if (currentCheckin != null)
                {
                    _context.Checkins.Remove(currentCheckin);
                    _context.SaveChanges();

                    return new ActionResult<Checkin>(null, ActionStatus.Deleted);
                }

                return new ActionResult<Checkin>(null, ActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new ActionResult<Checkin>(null, ActionStatus.Error, ex);
            }
        }

        public Checkin GetCheckin(int id)
        {
            return _context.Checkins.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Checkin> GetCheckinsForUser(string userId)
        {
            var checkins = _context.Checkins.Where(c => c.UserId.Equals(userId));

            if (checkins != null)
            {
                return checkins;
            }
            else
            {
                return null;
            }
        }
        
        public IQueryable<Checkin> GetCheckinsByRange(DateTime startDate, DateTime endDate, string userId)
        {
            var checkins = _context.Checkins.Where(c => c.UserId.Equals(userId) &&
                                                c.TimeAdded.Ticks >= startDate.Ticks &&
                                                c.TimeAdded.Ticks <= startDate.Ticks);

            if (checkins != null)
            {
                return checkins;
            }
            else
            {
                return null;
            }
        }

        // Medicines
        public ActionResult<Medicine> InsertMedicine(Medicine medicine)
        {
            try
            {
                _context.Medicines.Add(medicine);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<Medicine>(medicine, ActionStatus.Created);
                }
                else
                {
                    return new ActionResult<Medicine>(medicine, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Medicine>(null, ActionStatus.Error, ex);
            }
        }

        public ActionResult<Medicine> UpdateMedicine(Medicine medicine)
        {
            try
            {
                var currentMedicine = _context.Medicines.FirstOrDefault(m => m.Id == medicine.Id);

                if (currentMedicine == null)
                {
                    return new ActionResult<Medicine>(medicine, ActionStatus.NotFound);
                }

                _context.Entry(currentMedicine).State = EntityState.Detached;
                _context.Medicines.Attach(medicine);
                _context.Entry(medicine).State = EntityState.Modified;
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<Medicine>(medicine, ActionStatus.Updated);
                }
                else
                {
                    return new ActionResult<Medicine>(medicine, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Medicine>(medicine, ActionStatus.Error, ex);
            }
        }

        public ActionResult<Medicine> DeleteMedicine(int id)
        {
            try
            {
                var currentMedicine = _context.Medicines.FirstOrDefault(m => m.Id == id);

                if (currentMedicine != null)
                {
                    var medicinesTaken = _context.MedicineTakens.Where(mt => mt.MedicineId == id);

                    // Delete all MedicineTaken records with this Medicine as the foreign key.
                    if (medicinesTaken != null)
                    {
                        foreach (var mt in medicinesTaken)
                        {
                            _context.MedicineTakens.Remove(mt);
                        }
                    }

                    _context.Medicines.Remove(currentMedicine);
                    _context.SaveChanges();

                    return new ActionResult<Medicine>(null, ActionStatus.Deleted);
                }

                return new ActionResult<Medicine>(null, ActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new ActionResult<Medicine>(null, ActionStatus.Error, ex);
            }
        }

        public Medicine GetMedicine(int id)
        {
            return _context.Medicines.FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<Medicine> GetMedicinesForUser(string userId)
        {
            var medicines = _context.Medicines.Where(m => m.UserId.Equals(userId));

            if (medicines != null)
            {
                return medicines;
            }
            else
            {
                return null;
            }
        }

        // Medicine Taken
        public ActionResult<MedicineTaken> InsertMedicineTaken(MedicineTaken medicineTaken)
        {
            try
            {
                _context.MedicineTakens.Add(medicineTaken);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<MedicineTaken>(medicineTaken, ActionStatus.Created);
                }
                else
                {
                    return new ActionResult<MedicineTaken>(medicineTaken, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<MedicineTaken>(medicineTaken, ActionStatus.NothingModified, ex);
            }
        }

        public ActionResult<MedicineTaken> DeleteMedicineTaken(int id)
        {
            try
            {
                var currentMedicineTaken = _context.MedicineTakens.FirstOrDefault(mt => mt.Id == id);

                if (currentMedicineTaken != null)
                {
                    _context.MedicineTakens.Remove(currentMedicineTaken);
                    _context.SaveChanges();

                    return new ActionResult<MedicineTaken>(null, ActionStatus.Deleted);
                }

                return new ActionResult<MedicineTaken>(null, ActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new ActionResult<MedicineTaken>(null, ActionStatus.Error, ex);
            }
        }

        public MedicineTaken GetMedicineTaken(int id)
        {
            return _context.MedicineTakens.FirstOrDefault(mt => mt.Id == id);
        }

        public IQueryable<MedicineTaken> GetMedicineTakenByMedicineId(int medicineId)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.Id == medicineId);

            if (medicine != null)
            {
                return _context.MedicineTakens.Where(mt => mt.MedicineId == medicineId);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<MedicineTaken> GetMedicineTakenByMedicineIdByDate(int medicineId, DateTime date)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.Id == medicineId);

            if (medicine != null)
            {
                return _context.MedicineTakens.Where(mt => mt.MedicineId == medicineId && mt.DateAddedFor.Date == date);
            }
            else
            {
                return null;
            }
        }

        // Sleep
        public ActionResult<Sleep> InsertSleep(Sleep sleep)
        {
            try
            {
                _context.Sleeps.Add(sleep);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<Sleep>(sleep, ActionStatus.Created);
                }
                else
                {
                    return new ActionResult<Sleep>(sleep, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Sleep>(sleep, ActionStatus.Error, ex);
            }
        }

        public ActionResult<Sleep> UpdateSleep(Sleep sleep)
        {
            try
            {
                var currentSleep = _context.Sleeps.FirstOrDefault(s => s.Id == sleep.Id);

                if (currentSleep == null)
                {
                    return new ActionResult<Sleep>(sleep, ActionStatus.NotFound);
                }

                _context.Entry(currentSleep).State = EntityState.Detached;
                _context.Sleeps.Attach(sleep);
                _context.Entry(sleep).State = EntityState.Modified;
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new ActionResult<Sleep>(sleep, ActionStatus.Updated);
                }
                else
                {
                    return new ActionResult<Sleep>(sleep, ActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<Sleep>(sleep, ActionStatus.Error, ex);
            }
        }

        public ActionResult<Sleep> DeleteSleep(int id)
        {
            try
            {
                var currentSleep = _context.Sleeps.FirstOrDefault(s => s.Id == id);

                if (currentSleep != null)
                {
                    _context.Sleeps.Remove(currentSleep);
                    _context.SaveChanges();

                    return new ActionResult<Sleep>(null, ActionStatus.Deleted);
                }

                return new ActionResult<Sleep>(null, ActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new ActionResult<Sleep>(null, ActionStatus.Error, ex);
            }
        }

        public Sleep GetSleep(int id)
        {
            return _context.Sleeps.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Sleep> GetSleepForUser(string userId)
        {
            var sleeps = _context.Sleeps.Where(s => s.UserId.Equals(userId));

            if (sleeps != null)
            {
                return sleeps;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Sleep> GetSleepForUserInRange(string userId, DateTime startDate, DateTime endDate)
        {
            return _context.Sleeps.Where(s => s.UserId.Equals(userId) &&
                                        s.StartTime.Value.Ticks >= startDate.Ticks &&
                                        s.EndTime.Value.Ticks <= endDate.Ticks);
        }

        public IQueryable<Sleep> GetLastThirtySleepsForUser(string userId)
        {
            return _context.Sleeps.Where(s => s.UserId.Equals(userId))
                                         .OrderByDescending(s => s.Id)
                                         .Take(30);
        }

        public IQueryable<Sleep> GetLastNumberOfSleepsForUser(string userId, int count)
        {
            return _context.Sleeps.Where(s => s.UserId.Equals(userId))
                                        .OrderByDescending(s => s.StartTime)
                                        .Take(count);
        }
    }
}