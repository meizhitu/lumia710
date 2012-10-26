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
using System.Text;

namespace RpPk
{
    public class Utils
    {
        public static int GetInRange(int num, int min, int max)
        {
            if (num > max)
            {
                num = max;
                return num;
            }
            if (num < min)
            {
                num = min;
            }
            return num;
        }

        public static string GetMD5(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] buffer2 = MD5Core.GetHash(bytes);
            StringBuilder builder = new StringBuilder();
            int index = 0;
            int length = buffer2.Length;
            while (index < length)
            {
                builder.Append(buffer2[index].ToString("x").PadLeft(2, '0'));
                index++;
            }
            return builder.ToString();
        }
    }
}
