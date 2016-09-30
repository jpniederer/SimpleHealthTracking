namespace SimpleHealthTracking.Repository.Factories
{
    public class CheckinFactory
    {
        public CheckinFactory() { }

        public DTO.CheckinDto CreateCheckin(Entities.Checkin checkin)
        {
            return new DTO.CheckinDto()
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

        public Entities.Checkin CreateCheckin(DTO.CheckinDto checkin)
        {
            return new Entities.Checkin
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