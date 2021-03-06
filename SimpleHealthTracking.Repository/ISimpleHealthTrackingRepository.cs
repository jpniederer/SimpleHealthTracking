﻿namespace SimpleHealthTracking.Repository
{
    using Entities;
    using System;
    using System.Linq;

    public interface ISimpleHealthTrackingRepository
    {
        // Checkins
        ActionResult<Checkin> InsertCheckin(Checkin checkin);
        ActionResult<Checkin> UpdateCheckin(Checkin checkin);
        ActionResult<Checkin> DeleteCheckin(int id);
        Checkin GetCheckin(int id);
        IQueryable<Checkin> GetCheckinsForUser(string userId);
        IQueryable<Checkin> GetCheckinsByRange(DateTime startDate, DateTime endDate, string userId);
        IQueryable<Checkin> GetLastNumberOfCheckinsForUser(string userId, int count);
        IQueryable<Checkin> GetLastNumberOfCheckinWeightsForUser(string userId, int count);
        IQueryable<Checkin> GetLastNumberOfCheckinHeartratesForUser(string userId, int count);

        // Medicines
        ActionResult<Medicine> InsertMedicine(Medicine medicine);
        ActionResult<Medicine> UpdateMedicine(Medicine medicine);
        ActionResult<Medicine> DeleteMedicine(int id);
        Medicine GetMedicine(int id);
        IQueryable<Medicine> GetMedicinesForUser(string userId);
        IQueryable<Medicine> GetActiveMedicineForUser(string userId);
        IQueryable<Medicine> GetPublicMedicineForUser(string userId);

        // Medicine Taken
        ActionResult<MedicineTaken> InsertMedicineTaken(MedicineTaken medicineTaken);
        ActionResult<MedicineTaken> UpdateMedicineTaken(MedicineTaken medicineTaken);
        ActionResult<MedicineTaken> DeleteMedicineTaken(int id);
        MedicineTaken GetMedicineTaken(int id);
        IQueryable<MedicineTaken> GetMedicineTakenByMedicineId(int medicineId);
        IQueryable<MedicineTaken> GetMedicineTakenByMedicineIdByDate(int medicineId, DateTime date);
        IQueryable<MedicineTaken> GetMedicineTakenByUser(string userId);
        IQueryable<MedicineTaken> GetMedicineTakenByUserForDate(string userId, DateTime date);


        // Sleep
        ActionResult<Sleep> InsertSleep(Sleep sleep);
        ActionResult<Sleep> UpdateSleep(Sleep sleep);
        ActionResult<Sleep> DeleteSleep(int id);
        Sleep GetSleep(int id);
        IQueryable<Sleep> GetSleepForUser(string userId);
        IQueryable<Sleep> GetSleepForUserInRange(string userId, DateTime startDate, DateTime endDate);
        IQueryable<Sleep> GetLastThirtySleepsForUser(string userId);
        IQueryable<Sleep> GetLastNumberOfSleepsForUser(string userId, int count);
        IQueryable<Sleep> GetLastFullSleepsForUser(string userId, int count);

        // PublicStatsPages
        ActionResult<PublicStatsPage> InsertPublicStatsPage(PublicStatsPage psp);
        ActionResult<PublicStatsPage> UpdatePublicStatsPage(PublicStatsPage psp);
        ActionResult<PublicStatsPage> DeletePublicStatsPage(int id);
        PublicStatsPage GetPublicStatsPage(int id);
        PublicStatsPage GetPublicStatsPageForUser(string userId);

        // Workouts
        ActionResult<Workout> InsertWorkout(Workout workout);
        ActionResult<Workout> UpdateWorkout(Workout workout);
        ActionResult<Workout> DeleteWorkout(int id);
        Workout GetWorkout(int id);
        IQueryable<Workout> GetWorkoutsForUser(string userId);

        // Workout Types
        IQueryable<WorkoutType> GetWorkoutTypes();
        WorkoutType GetWorkoutType(int id);
    }
}