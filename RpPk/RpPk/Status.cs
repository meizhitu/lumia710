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

namespace RpPk
{
    public class Status
    {
        public int atk;
        public bool buff = false;
        public int cheater = 0;
        public int def;
        public int dex;
        public int hp;
        public string md5;
        public int mhp;
        public string name;
        public Random rnd;
        public int rp;
        public int spd;

        public Status(string Name)
        {
            this.name = Name;
            this.md5 = Utils.GetMD5(Name);
            this.rnd = new Random(Convert.ToInt32(this.md5.Substring(0x19, 7), 0x10));
            this.Initialize();
            if (Name == "中国" || Name.Contains("飞信"))
            {
                this.cheater = 1;
                this.hp = this.mhp = 500;
                this.atk = this.def = this.dex = this.spd = this.rp = 130;
            }
            else if (Name.Contains("日本"))
            {
                this.cheater = 2;
                this.hp = this.mhp = 50;
                this.atk = this.def = this.dex = this.spd = 30;
                this.rp = 1;
            }
        }

        private void Initialize()
        {
            this.hp = this.mhp = this.Md5toNum(100, 500);
            this.atk = this.Md5toNum(30, 100);
            this.def = this.Md5toNum(30, 100);
            this.dex = this.Md5toNum(30, 100);
            this.spd = this.Md5toNum(30, 100);
            this.rp = this.Md5toNum(1, 100);
        }

        private int Md5toNum(int min, int max)
        {
            return (min + (this.rnd.Next(0x7fffffff) % (max - min)));
        }
    }
}
