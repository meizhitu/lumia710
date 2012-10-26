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

namespace _24game
{
    public class Utils
    {
        public static bool NextPerm<T>(T[] t) where T : IComparable
        {
            for (int i = t.Length - 2; i >= 0; i--)
            {
                if (t[i].CompareTo(t[i + 1]) < 0)
                {
                    Array.Reverse(t, i + 1, t.Length - i - 1);
                    for (int j = i + 1; j < t.Length; j++)
                    {
                        if (t[i].CompareTo(t[j]) < 0)
                        {
                            T temp = t[i]; t[i] = t[j]; t[j] = temp;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static double Calc(string s)
        {
            int lastpos = -1;
            char lastOps = ' ';
            int rightBracket = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                switch (s[i])
                {
                    case ')':
                        rightBracket++;
                        break;
                    case '(':
                        rightBracket--;
                        break;
                }
                if (rightBracket > 0)
                    continue;

                switch (s[i])
                {
                    case '+':
                        return Calc(s.Substring(0, i))
                                + Calc(s.Substring(i + 1, s.Length-i-1));
                    case '-':
                        return Calc(s.Substring(0, i))
                                - Calc(s.Substring(i + 1, s.Length - i - 1));
                    case '*':
                        if (lastpos < 0)
                        {
                            lastpos = i;
                            lastOps = '*';
                        }
                        break;
                    case '/':
                        if (lastpos < 0)
                        {
                            lastpos = i;
                            lastOps = '/';
                        }
                        break;
                }
            }
            if (lastOps == '*')
            {
                return Calc(s.Substring(0, lastpos))
                        * Calc(s.Substring(lastpos + 1, s.Length-lastpos-1));
            }
            if (lastOps == '/')
            {
                return Calc(s.Substring(0, lastpos))
                        / Calc(s.Substring(lastpos + 1, s.Length-lastpos-1));
            }
            if (s[0] == '(' && s[s.Length - 1] == ')')
                return Calc(s.Substring(1, s.Length - 1-1));

            return Double.Parse(s);
        }
    }
}
