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

namespace RpPk
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MD5Fight mf;
        private System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer timer2 = new System.Windows.Threading.DispatcherTimer();
        private int roundCount;
        private string[] outputStr = new string[0];
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            roundCount = 0;
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 0x4b0); // 500 Milliseconds
            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Interval = new TimeSpan(0, 0, 0, 0, 0x4b0); // 500 Milliseconds
            timer2.Tick += new EventHandler(timer2_Tick);
        }

        private void 初始化控件属性()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("[" + this.mf.人物1.name + "]与[" + this.mf.人物2.name + "]偶然相遇，一言不合大打出手。");
            builder.Append("[" + this.mf.人物1.name + "]");
            builder.Append(" HP " + this.mf.人物1.hp.ToString());
            builder.Append(" 攻 " + this.mf.人物1.atk.ToString());
            builder.Append(" 防 " + this.mf.人物1.def.ToString());
            builder.Append(" 命中 " + this.mf.人物1.dex.ToString());
            builder.Append(" 速度 " + this.mf.人物1.spd.ToString());
            builder.Append(" 运气 " + this.mf.人物1.rp.ToString());
            builder.Append("\r\n");
            builder.Append("[" + this.mf.人物2.name + "]");
            builder.Append(" HP " + this.mf.人物2.hp.ToString());
            builder.Append(" 攻 " + this.mf.人物2.atk.ToString());
            builder.Append(" 防 " + this.mf.人物2.def.ToString());
            builder.Append(" 命中 " + this.mf.人物2.dex.ToString());
            builder.Append(" 速度 " + this.mf.人物2.spd.ToString());
            builder.Append(" 运气 " + this.mf.人物2.rp.ToString());
            builder.Append("\r\n");
            this.tb.Text = builder.ToString();
        }


        private void UpdatePkResult()
        {
            this.tb2_1.Text = this.mf.人物1.hp.ToString() + "/" + this.mf.人物1.mhp.ToString();
            this.tb2_2.Text = this.mf.人物2.hp.ToString() + "/" + this.mf.人物2.mhp.ToString();
            this.tb3_1.Text = this.mf.人物1.atk.ToString();
            this.tb3_2.Text = this.mf.人物2.atk.ToString();
            this.tb4_1.Text = this.mf.人物1.def.ToString();
            this.tb4_2.Text = this.mf.人物2.def.ToString();
            this.tb5_1.Text = this.mf.人物1.dex.ToString();
            this.tb5_2.Text = this.mf.人物2.dex.ToString();
            this.tb6_1.Text = this.mf.人物1.spd.ToString();
            this.tb6_2.Text = this.mf.人物2.spd.ToString();
            this.tb7_1.Text = this.mf.人物1.rp.ToString();
            this.tb7_2.Text = this.mf.人物2.rp.ToString();
        }

        private void 结束清算()
        {
            string str2;
            string str = "";
            if (this.mf.回合 == 1)
            {
                if (this.mf.人物1.hp == 0)
                {
                    str = "被";
                }
                str = str + "秒杀!";
            }
            else if (this.mf.回合 < 5)
            {
                if (this.mf.人物1.hp == 0)
                {
                    str = "实力悬殊!";
                }
                else
                {
                    str = "霸气十足!";
                }
            }
            else if (this.mf.回合 < 10)
            {
                if (this.mf.人物1.hp == 0)
                {
                    str = "实力不济!";
                }
                else
                {
                    str = "轻松摆平!";
                }
            }
            else if (this.mf.回合 < 15)
            {
                if (this.mf.人物1.hp == 0)
                {
                    str = "尚且不敌!";
                }
                else
                {
                    str = "技高一筹!";
                }
            }
            else if (this.mf.回合 < 20)
            {
                if (this.mf.人物1.hp == 0)
                {
                    str = "难解难分!";
                }
                else
                {
                    str = "难解难分!";
                }
            }
            else if (this.mf.人物1.hp == 0)
            {
                str = "势均力敌!";
            }
            else
            {
                str = "势均力敌!";
            }
            if (this.mf.人物1.hp == 0)
            {
                str2 = "胜方是[" + this.mf.人物2.name + "]";
            }
            else if (this.mf.人物2.hp == 0)
            {
                str2 = "胜方是[" + this.mf.人物1.name + "]";
            }
            else
            {
                str2 = "不分胜负";
            }
            this.tb.Text += ("战斗结束，共" + this.mf.回合.ToString() + "回合，" + str2 + "\r\n我方的战斗评价:" + str);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            if (this.mf != null)
            {
                if (this.mf.下一回合(out this.outputStr))
                {
                    this.tb.Text += ("\r\n");
                    this.roundCount = 0;
                    this.timer2.Stop();
                    this.timer2.Start();
                }
                else
                {
                    this.结束清算();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.roundCount < this.outputStr.Length)
            {
                this.tb.Text += (this.outputStr[this.roundCount] + "\r\n");
                this.roundCount++;
            }
            else
            {
                this.UpdatePkResult();
                this.timer2.Stop();
                this.timer1.Start();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = this.tb1_1.Text.Trim();
            string str2 = this.tb1_2.Text.Trim();
            if ((str != "") && (str2 != ""))
            {
                if (((!str.Contains("[") && !str.Contains("]")) && !str2.Contains("[")) && !str2.Contains("]"))
                {
                    if (str != str2)
                    {
                        this.mf = new MD5Fight(str, str2);
                        this.初始化控件属性();
                        this.UpdatePkResult();
                        this.timer1.Stop();
                        this.timer1.Start();
                    }
                    else
                    {
                        MessageBox.Show("请不要在两边填入相同名字");
                    }
                }
                else
                {
                    MessageBox.Show("请不要在名字中加入方括号");
                }
            }
            else
            {
                MessageBox.Show("请输入双方名字，名字前后加空格或纯空格无效");
            }
        }
    }
}