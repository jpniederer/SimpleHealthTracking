namespace SimpleHealthTracking.Repository.Factories
{
    using DTO;
    using Entities;
    using Helpers;
    using System;

    public class CheckinFactory
    {
        public CheckinFactory() { }

        public CheckinDto CreateCheckin(Checkin checkin)
        {
            return new CheckinDto()
            {
                Id = checkin.Id,
                UserId = checkin.UserId,
                Weight = checkin.Weight,
                Heartrate = checkin.Heartrate,
                SystolicBloodPressure = checkin.SystolicBloodPressure,
                DiastolicBloodPressure = checkin.DiastolicBloodPressure,
                PhysicalFeelingRating = checkin.PhysicalFeelingRating,
                PsychologicalFeelingRating = checkin.PsychologicalFeelingRating,
                ExerciseRating = checkin.ExerciseRating,
                Notes = checkin.Notes,
                TimeAdded = checkin.TimeAdded,
                UpdateTime = checkin.UpdateTime
            };
        }

        public Checkin CreateCheckin(CheckinDto checkin)
        {
            return new Checkin
            {
                Id = checkin.Id,
                UserId = checkin.UserId,
                Weight = checkin.Weight,
                Heartrate = checkin.Heartrate,
                SystolicBloodPressure = checkin.SystolicBloodPressure,
                DiastolicBloodPressure = checkin.DiastolicBloodPressure,
                PhysicalFeelingRating = checkin.PhysicalFeelingRating,
                PsychologicalFeelingRating = checkin.PsychologicalFeelingRating,
                ExerciseRating = checkin.ExerciseRating,
                Notes = checkin.Notes,
                TimeAdded = checkin.TimeAdded,
                UpdateTime = checkin.UpdateTime
            };
        }

        public Checkin CreateCheckin(ExcelImportDto excelImportDto)
        {
            Checkin checkin = new Checkin
            {
                UserId = excelImportDto.UserId,
                Notes = excelImportDto.Notes,
                UpdateTime = DateTime.Now
            };

            checkin.Weight = GetFloatValue(excelImportDto.Weight);
            checkin.Heartrate = GetFloatValue(excelImportDto.Heartrate);
            checkin.PhysicalFeelingRating = GetFloatValue(excelImportDto.Feel);
            checkin.PsychologicalFeelingRating = GetFloatValue(excelImportDto.Mind);
            checkin.ExerciseRating = GetFloatValue(excelImportDto.Body);
            checkin.TimeAdded = DateTime.Parse(string.Format("{0} {1}",
                Utility.GetDateString(excelImportDto.DateEntry),
                Utility.GetTimeString(excelImportDto.TimeEntry)));
            return checkin;
        }

        public float? GetFloatValue(string possibleValue)
        {
            float value;

            if (float.TryParse(possibleValue, out value))
            {
                return value;
            }

            return null;
        }
    }
}