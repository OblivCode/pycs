using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pycs.modules.datetime
{
    public class date
    {
        public date(int year = 0, int month = 0, int day = 0)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
       
        public date(DateTime DT)
        {
            day = int.Parse(DT.DayOfWeek.ToString());
            month = int.Parse(DT.Month.ToString());
            year = int.Parse(DT.Year.ToString());
           
        }

        public date(datetime DT)
        {
            day = DT.day;
            month = DT.month;
            year = DT.year;
        }

        public static date today()
        {
            DateTime dt = DateTime.Now;
            return new date(dt.Year, dt.Month, dt.Day);
        }
        public static date utctoday()
        {
            DateTime dt = DateTime.UtcNow;
            return new date(dt.Year, dt.Month, dt.Day);
        }
        public int day;
        public int month;
        public int year;
    }
}
