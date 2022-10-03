using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pycs.modules.datetime
{
    public class time
    {
        public time(int hours = 0, int minutes = 0, int seconds = 0)
        {
            this.seconds = seconds;
            this.hours = hours;
            this.minutes = minutes;
        }
        public time(DateTime DT)
        {
            hours = DT.Hour;
            minutes = DT.Minute;
            seconds = DT.Second;
                
        }

        public time(datetime DT)
        {
            hours = DT.hour;
            minutes = DT.minute;
            seconds = DT.second;
        }

        public static time now()
        {
            DateTime dt = DateTime.Now;
            return new time(dt);
        }
        public static time utcnow()
        {
            DateTime dt = DateTime.Now;
            return new time(dt);
        }
        public int seconds;
        public int minutes;
        public int hours;
        
    }
}
