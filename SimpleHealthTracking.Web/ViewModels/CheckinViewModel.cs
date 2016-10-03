namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SimpleHealthTracking.Repository.Entities;

    public class CheckinViewModel
    {
        public string Weight { get; set; }
        public string Heartrate { get; set; }
        public string SystolicBloodPressure { get; set; }
        public string DiastolicBloodPressure { get; set; }
        public string PhysicalFeelingRating { get; set; }
        public string PsychologicalFeelingRating { get; set; }
        public string ExerciseRating { get; set; }
        public string Notes { get; set; }

        public CheckinViewModel() { }

        public CheckinViewModel(Checkin checkin)
        {
            Weight = checkin.Weight.ToString();
            Heartrate = checkin.Heartrate.ToString();
            SystolicBloodPressure = checkin.SystolicBloodPressure.ToString();
            DiastolicBloodPressure = checkin.DiastolicBloodPressure.ToString();
            PhysicalFeelingRating = checkin.PhysicalFeelingRating.ToString();
            PsychologicalFeelingRating = checkin.PsychologicalFeelingRating.ToString();
            ExerciseRating = checkin.ExerciseRating.ToString();
            Notes = checkin.Notes;
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