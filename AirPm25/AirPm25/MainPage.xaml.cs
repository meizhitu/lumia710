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
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;
using System.Windows.Threading;

namespace AirPm25
{
    public partial class MainPage : PhoneApplicationPage
    {
        private List<string> urlList = new List<string>();
        private int curIndex = 0;
        public PeriodicTask _tskPeriodic;
        private DispatcherTimer dt = new DispatcherTimer();
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            urlList.Add("http://wt.china-we8.com/json.php");
            urlList.Add("http://www.beijing-air.com/");
            urlList.Add("http://iphone.bjair.info/m/beijing/mobile");

            dt.Interval = TimeSpan.FromSeconds(1800);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            curIndex = 0;
            GetPm(urlList[0]);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            curIndex = 0;
            GetPm(urlList[0]);

            ScheduledAction tTask = ScheduledActionService.Find("pm25___");
            if (tTask != null)
            {
                _tskPeriodic = tTask as PeriodicTask;
            }
            else
            {
                _tskPeriodic = new PeriodicTask("pm25___");
                _tskPeriodic.Description = "后台更新PM2.5数据"; 
            }
            if (_tskPeriodic != null && !_tskPeriodic.IsScheduled)
            {
                ScheduledActionService.Add(_tskPeriodic);
                ScheduledActionService.LaunchForTest("pm25___", TimeSpan.FromSeconds(10));
            }
        }

        public void GetPm(string url)
        {
            UriBuilder fullUri = new UriBuilder(url);
            // initialize a new WebRequest
            HttpWebRequest forecastRequest = (HttpWebRequest)WebRequest.Create(fullUri.Uri);
            // set up the state object for the async request
            ForecastUpdateState forecastState = new ForecastUpdateState();
            forecastState.AsyncRequest = forecastRequest;
            // start the asynchronous request
            forecastRequest.BeginGetResponse(new AsyncCallback(HandleForecastResponse),
                forecastState);
        }

        private void HandleForecastResponse(IAsyncResult asyncResult)
        {
            // get the state information
            ForecastUpdateState forecastState = (ForecastUpdateState)asyncResult.AsyncState;
            HttpWebRequest forecastRequest = (HttpWebRequest)forecastState.AsyncRequest;
            // end the async request
            forecastState.AsyncResponse = (HttpWebResponse)forecastRequest.EndGetResponse(asyncResult);
            if (forecastState.AsyncResponse.StatusCode != HttpStatusCode.OK)
            {
                if (curIndex >= 2)
                {
                    curIndex = 0;
                }
                else
                {
                    //GetPm(urlList[++curIndex]);
                }
                return;
            }
            Stream streamResult;
            try
            {
                // get the stream containing the response from the async call
                streamResult = forecastState.AsyncResponse.GetResponseStream();
                StreamReader sr = new StreamReader(streamResult);
                string all = sr.ReadToEnd();
                if (curIndex == 0)
                {
                    //{"location":"北京","date":"2012-05-09","time":"17:00:00","PM_low":"","PM_high":"172","rank":"轻度污染","Ozone":"0","resource":"美国大使馆"} 
                    string re = ".+\"PM_high\":\"(\\d+)\",\"rank\":\"([^\"]+)\"";
                    Regex regex = new Regex(re, RegexOptions.Compiled);
                    Match m = regex.Match(all);
                    if (m.Success)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                   {
    //                   <tr><td  class="aqi_green" >Good </td><td class="keyrange">0-50</td></tr>
    //<tr><td  class="aqi_yellow" >Moderate	</td><td class="keyrange">51-100</td></tr>
    //<tr><td  class="aqi_orange" >Unhealthy for Sensitive Groups</td><td class="keyrange">101-150</td></tr>
    //<tr><td  class="aqi_red" >Unhealthy</td><td class="keyrange">151-200</td></tr>
    //<tr><td  class="aqi_purple" >Very Unhealthy</td><td class="keyrange">201-300</td></tr>
    //<tr><td  class="aqi_maroon" >Hazardous</td><td class="keyrange"> &gt;300</td></tr>
                       this.highTb.Text = m.Result("$1");
                       int high;
                       Color color = Colors.White;
                       if (int.TryParse(this.highTb.Text, out high))
                       {
                           if (high < 50)
                               color = Colors.Green;
                           else if (high < 100)
                               color = Colors.Yellow;
                           else if (high < 150)
                               color = Colors.Orange;
                           else if (high < 200)
                               color = Colors.Red;
                           else
                               color = Colors.Purple;
                       }
                       this.rankTb.Text = m.Result("$2");
                       this.dateTb.Text = "更新时间：" + DateTime.Now;
                       if (color != Colors.White)
                       {
                           this.highTb.Foreground = new SolidColorBrush(color);
                           this.rankTb.Foreground = new SolidColorBrush(color);
                           ShellTile tile = ShellTile.ActiveTiles.First();
                           if (tile != null)
                           {
                               // 设置要更新的一些属性
                               StandardTileData tileData = new StandardTileData
                               {
                                   Title = high + "",
                                   BackgroundImage = new Uri("SplashScreenImage.jpg", UriKind.Relative),
                                   Count = 0,
                                   BackTitle = "北京",
                                   BackBackgroundImage = new Uri("ApplicationIcon.png", UriKind.Relative),
                                   BackContent = high + ""
                               };
                               // 更新
                               tile.Update(tileData);
                           }
                       }
                   });
                    }

                }
            }
            catch (FormatException)
            {
                // there was some kind of error processing the response from the web
                // additional error handling would normally be added here
                return;
            }
            curIndex = 0;
        }
    }
}