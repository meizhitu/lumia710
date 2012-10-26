namespace System.Globalization
{
    using System;
    using System.Runtime;
    using System.Runtime.InteropServices;
    using CnCalendar;

    public class ChineseLunisolarCalendar
    {

        internal int GetMonth(DateTime solarDateTime)
        {
           DateInfo info = new DateInfo();
           MyCalendar.GetLunarFromDay(solarDateTime.Year, solarDateTime.Month, solarDateTime.Day, info);
           if (GetLeapMonth(solarDateTime.Year) < info.month || info.isRunYue)
           {
               info.month++;
           }
           return info.month;
        }

        internal bool IsLeapMonth(int p, int i)
        {
            return MyCalendar.GetLeapMonth(p) == i;
        }

        internal int GetYear(DateTime solarDateTime)
        {
            DateInfo info = new DateInfo(); ;
            MyCalendar.GetLunarFromDay(solarDateTime.Year, solarDateTime.Month, solarDateTime.Day, info);
            return info.year;
        }

        internal int GetLeapMonth(int p)
        {
            return MyCalendar.GetLeapMonth(p);
        }

        internal int GetDayOfMonth(DateTime solarDateTime)
        {
            DateInfo info = new DateInfo(); ;
            MyCalendar.GetLunarFromDay(solarDateTime.Year, solarDateTime.Month, solarDateTime.Day, info);
            return info.day;
        }
    }
}

