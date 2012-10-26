using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CnCalendar
{
    public class Utils
    {
        public static DateTime Lunar2Solar(int y, int m, int d)
        {
            DateInfo lunar = new DateInfo();
            lunar.year = y;
            lunar.month = m;
            lunar.day = d;
            DateInfo solar =  MyCalendar.GetGlDate(lunar);
            return new DateTime(solar.year,solar.month,solar.day);
        }
    }
}
