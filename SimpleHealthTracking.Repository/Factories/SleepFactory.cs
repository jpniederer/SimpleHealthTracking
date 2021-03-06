﻿namespace SimpleHealthTracking.Repository.Factories
{
    using DTO;
    using Entities;
    using Helpers;
    using System;

    public class SleepFactory
    {
        public SleepFactory() { }

        public SleepDto CreateSleep(Sleep sleep)
        {
            return new SleepDto()
            {
                Id = sleep.Id,
                UserId = sleep.UserId,
                StartTime = sleep.StartTime,
                EndTime = sleep.EndTime,
                SleepQuality = sleep.SleepQuality,
                MinutesSlept = sleep.MinutesSlept,
                TimeAdded = sleep.TimeAdded,
                UpdateTime = sleep.UpdateTime
            };
        }

        public Sleep CreateSleep(SleepDto sleep)
        {
            return new Sleep()
            {
                Id = sleep.Id,
                UserId = sleep.UserId,
                StartTime = sleep.StartTime,
                EndTime = sleep.EndTime,
                SleepQuality = sleep.SleepQuality,
                MinutesSlept = sleep.MinutesSlept,
                TimeAdded = sleep.TimeAdded,
                UpdateTime = sleep.UpdateTime
            };
        }

        public Sleep CreateSleep(ExcelImportDto excelImportDtoAwake, ExcelImportDto excelImportDtoBed)
        {
            Sleep sleep = new Sleep()
            {
                UserId = excelImportDtoAwake.UserId,
                StartTime = DateTime.Parse(string.Format("{0} {1}",
                    Utility.GetDateString(excelImportDtoAwake.DateEntry), 
                    Utility.GetTimeString(excelImportDtoAwake.TimeEntry))),
                EndTime = DateTime.Parse(string.Format("{0} {1}", 
                    Utility.GetDateString(excelImportDtoBed.DateEntry),
                    Utility.GetTimeString(excelImportDtoBed.TimeEntry))),
                TimeAdded = DateTime.Now,
                UpdateTime = DateTime.Now
            };

            sleep.SetMinutesSlept();
            return sleep;
        }
    }
}