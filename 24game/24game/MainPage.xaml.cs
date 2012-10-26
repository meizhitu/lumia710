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
using System.Windows.Media.Imaging;

namespace _24game
{
    public partial class MainPage : PhoneApplicationPage
    {
        private int[] data = null;
        private List<BitmapImage> imgs = new List<BitmapImage>();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            for (int i = 1; i <= 13; i++)
            {
                BitmapImage bmp = new BitmapImage(new Uri("img/p" + i + ".png", UriKind.Relative));
                //bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                imgs.Add(bmp);
            }
            ResetPuke();
        }

        private void ResetPuke()
        {
            int d1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, 13);
            int d2 = new Random(Guid.NewGuid().GetHashCode()).Next(1, 13);
            int d3 = new Random(Guid.NewGuid().GetHashCode()).Next(1, 13);
            int d4 = new Random(Guid.NewGuid().GetHashCode()).Next(1, 13);
            data = new int[] { d1, d2, d3, d4 };
            this.img1.Source = imgs[d1 - 1];
            this.img2.Source = imgs[d2 - 1];
            this.img3.Source = imgs[d3 - 1];
            this.img4.Source = imgs[d4 - 1];
            this.btn1.Content = d1;
            this.btn2.Content = d2;
            this.btn3.Content = d3;
            this.btn4.Content = d4;

        }

        private bool isOp(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/')
                return true;
            return false;
        }

        private bool isDigit(char c)
        {
            if (c <= '9' && c >= '0')
                return true;
            return false;
        }

        private bool isValidChar(char c)
        {
            if (c == '+' || c == '-' || c == '*' || c == '/'
                    || (c >= '0' && c <= '9') || c == '(' || c == ')')
                return true;
            return false;
        }

        private bool checkInput(String input)
        {
            input = input.Replace(" ", "");
            int opCount = 0;
            List<int> d = new List<int>();
            string sb = "";
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (isOp(c))
                    opCount++;
                if (!isValidChar(c))
                    return false;
                if (isDigit(c))
                {
                    sb += c;
                }
                else
                {
                    if (!(sb.Length == 0))
                    {
                        d.Add(int.Parse(sb));
                        sb = "";
                    }
                }
            }
            if (!(sb.Length == 0))
            {
                d.Add(int.Parse(sb));
                sb = "";
            }
            if (opCount != 3)
                return false;
            if (d.Count != 4)
                return false;
            d.Sort();
            List<int> lst = new List<int>();
            lst.Add(data[0]);
            lst.Add(data[1]);
            lst.Add(data[2]);
            lst.Add(data[3]);
            lst.Sort();
            for (int i = 0; i < 4; i++)
            {
                if (d[i] != lst[i])
                    return false;
            }
            double ca = Utils.Calc(input);
            if (Math.Abs(ca - 24) < 1e-6)
                return true;
            return false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string inputStr = this.input.Text;
            if (checkInput(inputStr))
            {

                MessageBox.Show("答对了！");
            }
            else
            {
                MessageBox.Show("答错了,再想想吧");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ResetPuke();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Game24 g = new Game24();
            Array.Sort(data);
            List<string> str = g.start(data, true);
            if (str.Count() > 0)
            {
                this.textBlock5.Text = "";
                foreach (string s in str)
                {
                    this.textBlock5.Text += (s + "\r\n");
                }
            }
            else
            {
                this.textBlock5.Text = "无解";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            this.input.Text += btn.Content;
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            Game24 g = new Game24();
            Array.Sort(data);
            List<string> str = g.start(data, false);
            if (str.Count() > 0)
            {
                MessageBox.Show("有答案的，try again");
            }
            else
            {
                MessageBox.Show("答对了，真的是无解的");
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            this.input.Text = "";
        }
    }
}