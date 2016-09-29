namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

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