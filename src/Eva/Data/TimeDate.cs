using Microsoft.AspNetCore.Mvc;
using System;

namespace Eva.Data
{
    public class TimeDate
    {
        public DateTime GetDateTimeNow()
        {
            var myDate = DateTime.Now;

            return (myDate);
        }

        public int GetCurrentYear()
        {
            var myDate = DateTime.Parse(Convert.ToString(DateTime.Now)).Year;

            return (myDate);
        }

        public DateTime GetDay(DateTime d)
        {
            //want to return day of the passed in date or day today
            

            return (d);
        }
    }
}
 