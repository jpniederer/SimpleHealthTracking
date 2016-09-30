namespace SimpleHealthTracking.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class ValidDateOrNull : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "MM/dd/yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime) || value == null;

            return isValid;
        }
    }
}