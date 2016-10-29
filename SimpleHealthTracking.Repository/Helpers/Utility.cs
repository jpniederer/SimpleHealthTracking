namespace SimpleHealthTracking.Repository.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Utility
    {
        public static string GetTimeString(string excelDateTime)
        {
            int indexOfSpace = excelDateTime.IndexOf(" ");
            return excelDateTime.Substring(indexOfSpace + 1,
                excelDateTime.Length - (indexOfSpace + 1));
        }

        public static string GetDateString(string excelDateTime)
        {
            int indexOfSpace = excelDateTime.IndexOf(" ");
            return excelDateTime.Substring(0, indexOfSpace);
        }
    }
}