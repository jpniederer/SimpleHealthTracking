namespace SimpleHealthTracking.Repository.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtentions
    {
        public static void RemoveRange<T>(this List<T> source, IEnumerable<T> rangeToRemove)
        {
            if (rangeToRemove == null | !rangeToRemove.Any())
            {
                return;
            }

            foreach (T item in rangeToRemove)
            {
                source.Remove(item);
            }
        }
    }
}