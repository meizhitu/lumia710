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

namespace RpCalc
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private int ConvertToUtf(string s, int index)
        {
            int num = s[index] - 0xd800;
            if ((num < 0) || (num > 0x7ff))
            {
                return s[index];
            }
            int num2 = s[index + 1] - 0xdc00;
            return (((num * 0x400) + num2) + 0x10000);
        }

        private void calcBtn_Click(object sender, RoutedEventArgs e)
        {
            string strUserName = rtb_name.Text.Trim();
            if (strUserName == String.Empty && strUserName.Length == 0)
            {
                rtb_result.Text = "请先输入要计算的名字";
                rtb_name.Text = "";
            }
            else if (rtb_name.Text == "飞信" || rtb_name.Text == "fetion")
            {
                rtb_result.Text = "评价:天啦！你不是人！你是神！！！！";
                nameTb.Text = rtb_name.Text;
                scoreTb.Text = "1e6";
                rtb_name.Text = "";
            }
            else if (rtb_name.Text == "日本人" || rtb_name.Text == "小日本" || rtb_name.Text == "日本" || rtb_name.Text == "日本鬼子")
            {
                rtb_result.Text = "评价:你的人品竟然负溢出了...我对你无语...";
                nameTb.Text = rtb_name.Text;
                scoreTb.Text = "-1";
                rtb_name.Text = "";
            }
            else
            {
                string name = rtb_name.Text;
                int sum = 0;
                for (int i = 0; i < name.Length; i++)
                {
                    sum += ConvertToUtf(name, i);
                }
                int rp = sum % 100;
                if (rp == 0)
                {
                    rtb_result.Text = "评价:你一定不是人吧？怎么一点人品都没有？！";
                }
                if (rp >= 1 && rp < 5)
                {
                    rtb_result.Text = "评价:算了,跟你没什么人品好谈的...";
                }
                if (rp >= 5 && rp < 10)
                {
                    rtb_result.Text = "评价:是我不好...不应该跟你谈人品问题的...";
                }
                if (rp >= 10 && rp < 15)
                {
                    rtb_result.Text = "评价:杀过人没有?放过火没有?你应该无恶不做吧?";
                }
                if (rp >= 15 && rp < 20)
                {
                    rtb_result.Text = "评价:你貌似应该三岁就偷看隔壁大妈洗澡的吧...";
                }
                if (rp >= 20 && rp < 25)
                {
                    rtb_result.Text = "评价:你的人品之低下实在让人惊讶啊...";
                }
                if (rp >= 25 && rp < 30)
                {
                    rtb_result.Text = "评价:你的人品太差了。你应该有干坏事的嗜好吧?";
                }
                if (rp >= 30 && rp < 35)
                {
                    rtb_result.Text = "评价:你的人品真差!肯定经常做偷鸡摸狗的事...";
                }
                if (rp >= 35 && rp < 40)
                {
                    rtb_result.Text = "评价:你拥有如此差的人品请经常祈求佛祖保佑你吧...";
                }
                if (rp >= 40 && rp < 45)
                {
                    rtb_result.Text = "评价:老实交待..那些论坛上面经常出现的偷拍照是不是你的杰作?";
                }
                if (rp >= 45 && rp < 50)
                {
                    rtb_result.Text = "评价:你随地大小便之类的事没少干吧?";
                }
                if (rp >= 50 && rp < 55)
                {
                    rtb_result.Text = "评价:你的人品太差了..稍不小心就会去干坏事了吧?";
                }
                if (rp >= 55 && rp < 60)
                {
                    rtb_result.Text = "评价:你的人品很差了..要时刻克制住做坏事的冲动哦..";
                }
                if (rp >= 60 && rp < 65)
                {
                    rtb_result.Text = "评价:你的人品比较差了..要好好的约束自己啊..";
                }
                if (rp >= 65 && rp < 70)
                {
                    rtb_result.Text = "评价:你的人品勉勉强强..要自己好自为之..";
                }
                if (rp >= 70 && rp < 75)
                {
                    rtb_result.Text = "评价:有你这样的人品算是不错了..";
                }
                if (rp >= 75 && rp < 80)
                {
                    rtb_result.Text = "评价:你有较好的人品..继续保持..";
                    rtb_name.Text = "";
                }
                if (rp >= 80 && rp < 85)
                {
                    rtb_result.Text = "评价:你的人品不错..应该一表人才吧?";
                }
                if (rp >= 85 && rp < 90)
                {
                    rtb_result.Text = "评价:你的人品真好..做好事应该是你的爱好吧..";

                }
                if (rp >= 90 && rp < 95)
                {
                    rtb_result.Text = "评价:你的人品太好了..你就是当代活雷锋啊...";
                }
                if (rp >= 95 && rp < 100)
                {
                    rtb_result.Text = "评价:你是世人的榜样！";
                }
                nameTb.Text = rtb_name.Text;
                scoreTb.Text = rp+"";
                rtb_name.Text = "";
            }
        }
    }
}