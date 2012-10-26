using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Text;

namespace CnCalendar
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ChineseAlmanac ca = new ChineseAlmanac();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            DateTime dt = new DateTime(2012,5,22);
            dt = DateTime.Now;
            this.yearTb.Text = dt.Year+"年" + dt.Month + "月";
            this.dayTb.Text =  dt.Day + "";
            this.weekTb.Text = "星期" + "日一二三四五六"[(int)dt.DayOfWeek];
            this.lunarTb.Text = "农历" + ca.ToStringWithSexagenary(ca.GetChineseEraOfYear(dt)) + ca.ToStringWithChineseMonth(dt) + ca.ToStringWithChineseDay(ca.GetDayOfMonth(dt));
            StringBuilder sb = new StringBuilder();
            AppendInfo(sb,ca.GetChineseHoliday(dt));
            AppendInfo(sb, ca.GetCommemoration(dt));
            AppendInfo(sb, ca.GetConstellation(dt));
            AppendInfo(sb, ca.GetFuJiu(dt));
            AppendInfo(sb, ca.GetHoliday(dt));
            AppendInfo(sb, ca.GetLuckyDeity(dt));
            AppendInfo(sb, ca.GetNaYinWuXing(ca.GetChineseEraOfYear(dt)));
            AppendInfo(sb, ca.GetPengZuBaiJi(dt));
            AppendInfo(sb, ca.GetRiLu(dt));
            AppendInfo(sb, ca.GetSuiSha(dt));
            AppendInfo(sb, ca.GetXiangChong(dt));

            this.tb1.Text = sb.ToString();
        }

        private void AppendInfo(StringBuilder sb,string s)
        {
            if (string.IsNullOrEmpty(s)) return;
            sb.AppendLine(s);
        }
    }
}