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

namespace equation
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private double GetTextBoxValue(TextBox tb)
        {
            if (tb == null) return 0;
            if (tb.Text == "") { tb.Text = "1"; return 1; }
            double result;
            if (double.TryParse(tb.Text, out result))
            {
                return result;
            }
            else
            {
                tb.Text = "1";
                return 1;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = GetTextBoxValue(this.textBox1);
                double b = GetTextBoxValue(this.textBox2);
                double c = GetTextBoxValue(this.textBox3);
                double d = GetTextBoxValue(this.textBox4);
                this.textBlock5.Text = Utils.ResolveEquation(a,b,c,d);
            }
            catch
            {

            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";

            this.textBlock5.Text = "";
        }

        private void button1_2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double a = GetTextBoxValue(null);
                double b = GetTextBoxValue(this.textBox2_2);
                double c = GetTextBoxValue(this.textBox3_2);
                double d = GetTextBoxValue(this.textBox4_2);
                this.textBlock5_2.Text = Utils.ResolveEquation(a, b, c, d);
            }
            catch
            {

            }

        }

        private void button2_2_Click(object sender, RoutedEventArgs e)
        {
            this.textBox2_2.Text = "";
            this.textBox3_2.Text = "";
            this.textBox4_2.Text = "";

            this.textBlock5_2.Text = "";
        }
    }
}