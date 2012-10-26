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
using System.IO;

namespace AirPm25
{
    public class ForecastUpdateState
    {
        public HttpWebRequest AsyncRequest { get; set; }
        public HttpWebResponse AsyncResponse { get; set; }
    }

    public class Utils
    {
        
    }
}
