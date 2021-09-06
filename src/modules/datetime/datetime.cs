using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pycs.modules.datetime
{
    public class datetime
    {
        public datetime(DateTime DT)
        {
            _DateTime = DT;
            
        }
        private static DateTime _DateTime;
        public int year { get { return _DateTime.Year; } }
        private  int centuryFromYear(int year)
        {
            return (int)(year / 100) + ((year % 100 == 0) ? 0 : 1);
        }
        private  string month_str { get { return _DateTime.ToString("MMMM"); } }
        public  int month { get { return _DateTime.Month; } }
        private  string day_str { get { return _DateTime.ToString("dddd"); } }
        private  int day_year { get { return _DateTime.DayOfYear; } }
        private  int day
        {
            get { switch(day_str.ToLower())
                {
                    case "monday": return 1;
                    case "tuesday": return 2;
                    case "wednesday": return 3;
                    case "thursday": return 4;
                    case "friday": return 5;
                    case "saturday": return 6;
                    case "sunday": return 7;
                    default: return 0;
                }
              
            }
        }
        private  int hour_12
        {
            get
            {
                switch(hour)
                {
                    case 23: return 11;
                    case 22: return 10;
                    case 21: return 9;
                    case 20: return 8;
                    case 19: return 7;
                    case 18: return 6;
                    case 17: return 5;
                    case 16: return 4;
                    case 15: return 3;
                    case 14: return 2;
                    case 13: return 1;
                    default: return hour;
                }
            }
        }
        public  int hour { get { return _DateTime.Hour; } }
        public  string AMPM
        {
            get { return hour > 12 ? "PM" : "AM"; }
        }
        public  int minute { get { return _DateTime.Minute; } }
        public  int second { get { return _DateTime.Second; } }
        public  int millisecond { get { return _DateTime.Millisecond; } }
        public string strftime(string format)
        {

            string str = "";
            char[] chArr = format.ToCharArray();
            for (int i = 0; i < chArr.Length; i++)
            {
                //keys[i] = "%" + keys[i];
                char ch = chArr[i];
                
                switch (ch)
                {
                    case '%':
                        {
                            char next = chArr[i+1];
                            
                            switch (next)
                            {
                                case 'a':
                                    str += day_str.Substring(0, 3);
                                    break;
                                case 'A':
                                    str += day_str;
                                    break;
                                case 'w':
                                    str += day;
                                    break;
                                case 'd':
                                    str += _DateTime.Day.ToString();
                                    break;
                                case 'b':
                                    str += month_str.Substring(0, 3);
                                    break;
                                case 'B':
                                    str += month_str;
                                    break;
                                case 'm':
                                    str += month;
                                    break;
                                case 'y':
                                    str += year.ToString().Length == 4 ? year.ToString().Substring(2, 4) : year;
                                    break;
                                case 'Y':
                                    str += year;
                                    break;
                                case 'H':
                                    str += hour;
                                    break;
                                case 'I':
                                    str += hour_12;
                                    break;
                                case 'p':
                                    str += AMPM;
                                    break;
                                case 'M':
                                    str += minute;
                                    break;
                                case 'S':
                                    str += second;
                                    break;
                                case 'f':
                                    str += (second * 1000000);
                                    break;
                                case 'z':
                                    str += TimeZoneInfo.Local.GetUtcOffset(_DateTime);
                                    break;
                                case 'Z':
                                    {
                                        string[] split = TimeZoneInfo.Local.StandardName.Split(' ');
                                        string timezone = "";
                                        foreach (var word in split)
                                            timezone += word[0];
                                        str += timezone;
                                    }
                                    break;
                                case 'j':
                                    str += day_year;
                                    break;
                                case 'W':
                                    {
                                        CultureInfo myCI = new CultureInfo("en-US");
                                        Calendar myCal = myCI.Calendar;
                                        CalendarWeekRule CWR = myCI.DateTimeFormat.CalendarWeekRule;
                                        DayOfWeek DOW = myCI.DateTimeFormat.FirstDayOfWeek;
                                        str += new CultureInfo("en-US").Calendar.GetWeekOfYear(_DateTime, CWR, DOW);
                                        break;
                                    }
                                case 'C':
                                    str += centuryFromYear(year);
                                    break;
                                case 'c':
                                    str += _DateTime.ToLongDateString() + " " + _DateTime.ToLongTimeString();
                                    break;
                                case 'x':
                                    str += _DateTime.ToShortDateString();
                                    break;
                                case 'X':
                                    str += _DateTime.ToShortTimeString();
                                    break;
                                default:
                                    str += next;
                                    break;
                            }
                            i++;
                            break;
                        }
                    default:
                        str += ch;
                        break;
                }
            }
            return str;
        }
       
        
        public static datetime now() => new datetime(DateTime.Now);
        public static datetime utcnow() => new datetime(DateTime.UtcNow);
    }
}
