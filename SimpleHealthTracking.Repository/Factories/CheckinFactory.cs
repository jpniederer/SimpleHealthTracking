namespace SimpleHealthTracking.Repository.Factories
{
    using DTO;
    using Entities;

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
    }
}