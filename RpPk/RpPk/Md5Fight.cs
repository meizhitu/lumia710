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
using System.Collections.Generic;

namespace RpPk
{
    public class MD5Fight
    {
        private Random rnd;
        private string[] 出招语句 = new string[] { "[%n1]怒发冲冠!", "[%n1]打红了眼，发狂了!", "[%n1]无端的害怕起来。", "[%n1]战意增加，变的勇猛了!", "[%n1]试图吃药，但错吃成脑残片，变成了脑残!", "[%n1]发动了连击!", "[%n1]扑上去撕咬[%n2]!", "[%n1]暗攒一口浓痰，狠狠的吐向[%n2]!", "[%n1]在[%n2]的脸上一通乱抓!", "[%n1]摆出V字胜利姿势，[%n2]正迟疑时，[%n1]使出二龙戏珠，插向[%n2]双眼!", "[%n1]对着[%n2]重重一击!", "[%n1]一巴掌扇向[%n2]!", "[%n1]对着[%n2]破口大骂，内容涉及[%n2]的整个族谱!", "[%n1]用%w狠狠抡向[%n2]!", "[%n1]跪地磕头，向[%n2]求饶，趁机突施偷袭!", "[%n1]对[%n2]发起攻击!" };
        private string 格挡成功语句 = "[%n2]挡住了这次攻击!";
        public int 回合 = 0;
        private int[] 技能概率 = new int[] { 30, 30, 30, 30, 30, 80, 50, 50, 50, 30, 80, 50, 50, 30, 30 };
        private string[] 器械 = new string[] { "棒球棍", "折凳", "一块豆腐", "显示器", "狼牙棒", "流星锤", "步枪托", "原子弹" };
        public Status 人物1;
        public Status 人物2;
        private string 闪避成功语句 = "[%n2]身形一晃，闪了过去!";
        private double[] 伤害倍率 = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, 1.5, 0.5, 1.0, 1.0, 1.5, 0.7, 0.3, 2.0, 1.0, 1.0 };
        private double[] 伤害浮动 = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 3.0, 1.2, 1.8, 0.0, 1.8, 8.0, 1.2, 1.5, 0.0 };
        private string 受伤语句 = "[%n2]受到了%d伤害!";
        private string 死亡语句 = "[%n2]在[%n1]的残忍攻击中倒下了!";
        private double[] 状态浮动 = new double[] { 0.3, 0.4, 0.25, 0.25, 0.4 };
        private int[] 状态正面性 = new int[] { 0, 0, 2, 1, 2 };

        public MD5Fight(string 名字1, string 名字2)
        {
            this.人物1 = new Status(名字1);
            this.人物2 = new Status(名字2);
            int num = Convert.ToInt32(this.人物1.md5.Substring(10, 7), 0x10);
            int num2 = Convert.ToInt32(this.人物2.md5.Substring(10, 7), 0x10);
            this.rnd = new Random(num + num2);
        }

        private int 浮动数字(int num, double 浮动度)
        {
            return this.浮动数字(num, 1.0 - 浮动度, 1.0 + 浮动度);
        }

        private int 浮动数字(int num, double 最小浮动度, double 最大浮动度)
        {
            if ((最小浮动度 == 0.0) || (最大浮动度 < 最小浮动度))
            {
                return num;
            }
            return Utils.GetInRange(this.rnd.Next((int)(num * 最小浮动度), (int)((num * 最大浮动度) + 1.0)), 1, 0x7fffffff);
        }

        private string[] 使用技能(Status s1, Status s2, int 技能)
        {
            List<string> list = new List<string> {
                this.出招语句[技能].Replace("%n1", s1.name).Replace("%n2", s2.name).Replace("%w", this.器械[this.rnd.Next(this.器械.Length)])
            };
            double num = ((double)(s1.rp + 100)) / 150.0;
            double num2 = ((double)(s2.rp + 100)) / 150.0;
            if (技能 < 5)
            {
                s1.buff = true;
                if (技能 == 0)
                {
                    s1.atk = this.向上浮动数字(s1.atk, this.状态浮动[技能] * num);
                    s1.def = this.向下浮动数字(s1.atk, this.状态浮动[技能] / num);
                }
                else if (技能 == 1)
                {
                    s1.atk = this.向上浮动数字(s1.atk, this.状态浮动[技能] * num);
                    s1.spd = this.向上浮动数字(s1.spd, this.状态浮动[技能] * num);
                    s1.def = this.向下浮动数字(s1.def, this.状态浮动[技能] / num);
                    s1.dex = this.向下浮动数字(s1.dex, this.状态浮动[技能] / num);
                }
                else if (技能 == 2)
                {
                    s1.atk = this.向下浮动数字(s1.atk, this.状态浮动[技能] / num);
                    s1.spd = this.向下浮动数字(s1.spd, this.状态浮动[技能] / num);
                    s1.def = this.向下浮动数字(s1.def, this.状态浮动[技能] / num);
                    s1.dex = this.向下浮动数字(s1.dex, this.状态浮动[技能] / num);
                }
                else if (技能 == 3)
                {
                    s1.atk = this.向上浮动数字(s1.atk, this.状态浮动[技能] * num);
                    s1.spd = this.向上浮动数字(s1.spd, this.状态浮动[技能] * num);
                    s1.def = this.向上浮动数字(s1.def, this.状态浮动[技能] * num);
                    s1.dex = this.向上浮动数字(s1.dex, this.状态浮动[技能] * num);
                }
                else
                {
                    s1.atk = this.向下浮动数字(s1.atk, this.状态浮动[技能] / num);
                    s1.spd = this.向下浮动数字(s1.spd, this.状态浮动[技能] / num);
                    s1.def = this.向下浮动数字(s1.def, this.状态浮动[技能] / num);
                    s1.dex = this.向下浮动数字(s1.dex, this.状态浮动[技能] / num);
                }
            }
            else if (this.掷骰子(Utils.GetInRange((int)((100 + ((s2.spd - s1.spd) * 8)) * num2), 100, 900)))
            {
                list.Add(this.闪避成功语句.Replace("%n1", s1.name).Replace("%n2", s2.name));
            }
            else if (this.掷骰子(Utils.GetInRange((int)(((s2.dex - s1.dex) * 8) * num2), 10, 700)))
            {
                list.Add(this.格挡成功语句.Replace("%n1", s1.name).Replace("%n2", s2.name));
            }
            else
            {
                int num3 = 0;
                do
                {
                    num3++;
                    int num4 = (((70 + this.浮动数字(s1.atk, 0.3)) - this.浮动数字(s2.def, 0.3)) * 100) / this.浮动数字(100, 1.0, 2.3);
                    if (num4 > 0)
                    {
                        num4 = this.向上浮动数字((int)(num4 * this.伤害倍率[技能]), this.伤害浮动[技能] * num);
                    }
                    num4 = Utils.GetInRange(num4, 1, 0x7fffffff);
                    list.Add(this.受伤语句.Replace("%n1", s1.name).Replace("%n2", s2.name).Replace("%d", num4.ToString()));
                    s2.hp = Utils.GetInRange(s2.hp - num4, 0, s2.hp);
                    if (s2.hp == 0)
                    {
                        list.Add(this.死亡语句.Replace("%n1", s1.name).Replace("%n2", s2.name));
                    }
                }
                while (((技能 == 5) && (s2.hp > 0)) && ((num3 < 2) || this.掷骰子((int)((((double)s1.spd) / ((double)s2.spd)) * (0x3e8 / num3)))));
            }
            return list.ToArray();
        }

        public bool 下一回合(out string[] 战斗信息)
        {
            战斗信息 = new string[0];
            if (((this.人物1.hp == 0) || (this.人物2.hp == 0)) || (this.回合 >= 20))
            {
                return false;
            }
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; (num2 == num3) && (i < 100); i++)
            {
                int maxValue = this.rnd.Next(0x7fffffff);
                num2 = Utils.GetInRange((int)(this.人物1.spd * 0.3), 1, 0x7fffffff);
                num3 = Utils.GetInRange((int)(this.人物2.spd * 0.3), 1, 0x7fffffff);
                num2 = (this.人物1.spd - num2) + (this.人物1.rnd.Next(maxValue) % (num2 * 2));
                num3 = (this.人物2.spd - num3) + (this.人物2.rnd.Next(maxValue) % (num3 * 2));
            }
            if (num2 < num3)
            {
                战斗信息 = this.战斗(this.人物2, this.人物1);
            }
            else
            {
                战斗信息 = this.战斗(this.人物1, this.人物2);
            }
            this.回合++;
            return true;
        }

        private int 向上浮动数字(int num, double 浮动度)
        {
            if (浮动度 <= 0.0)
            {
                return num;
            }
            int maxValue = (int)(num * 浮动度);
            return (num + this.rnd.Next(maxValue));
        }

        private int 向下浮动数字(int num, double 浮动度)
        {
            if (浮动度 <= 0.0)
            {
                return num;
            }
            int maxValue = (int)(num * 浮动度);
            return Utils.GetInRange(num - this.rnd.Next(maxValue), 1, 0x7fffffff);
        }

        private int 选择技能(bool 是否已有状态, double 技巧, double rp, int cheater)
        {
            int index = 0;
            if (!是否已有状态)
            {
                while (index < 5)
                {
                    if (this.状态正面性[index] == 0)
                    {
                        if (this.掷骰子(this.技能概率[index]))
                        {
                            break;
                        }
                    }
                    else if (this.状态正面性[index] == 1)
                    {
                        if (this.掷骰子((int)(this.技能概率[index] * rp)))
                        {
                            break;
                        }
                    }
                    else if (((this.状态正面性[index] == 2) && (cheater != 1)) && this.掷骰子(((int)(((double)this.技能概率[index]) / rp)) + (cheater * 200)))
                    {
                        break;
                    }
                    index++;
                }
                if (index < 5)
                {
                    return index;
                }
            }
            index = 5;
            while (index < this.技能概率.Length)
            {
                if (this.掷骰子((int)((this.技能概率[index] * 技巧) * rp)))
                {
                    return index;
                }
                index++;
            }
            return index;
        }

        private string[] 战斗(Status s1, Status s2)
        {
            List<string> list = new List<string>();
            double rp = ((double)(s1.rp + 200)) / 245.0;
            double num2 = ((double)(s2.rp + 200)) / 245.0;
            list.AddRange(this.使用技能(s1, s2, this.选择技能(s1.buff, (double)((s1.dex + 30) / 90), rp, s1.cheater)));
            if (s2.hp > 0)
            {
                list.AddRange(this.使用技能(s2, s1, this.选择技能(s2.buff, (double)((s2.dex + 30) / 90), num2, s2.cheater)));
            }
            return list.ToArray();
        }

        private bool 掷骰子(int 千分概率)
        {
            return (this.rnd.Next(0x3e8) < 千分概率);
        }
    }
}
