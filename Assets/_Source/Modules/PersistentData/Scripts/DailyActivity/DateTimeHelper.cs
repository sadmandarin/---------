using System;

namespace PersistentData
{
    public static class DateTimeHelper
    {
        public static DateTime GetNextDay7AM()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime nextDay7AM = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 7, 0, 0).AddDays(1);
            //if (DateTime.UtcNow.TimeOfDay.TotalHours >= 7)
            //{
            //    nextDay7AM = nextDay7AM.AddDays(1);
            //}
            return nextDay7AM;
        }
    }
}
