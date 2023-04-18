using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.Utilities
{
    public class CommonUtility
    {

    }
    public static class DateTimeExtensions
    {
        public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck <= endDate;
        }
    }
}
