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

namespace equation
{
    public class Utils
    {
        public static bool DoubleEquals(double a,double b)
        {
            if (Math.Abs(a - b) < 1e-10)
            {
                return true;
            }
            return false;
        }

        public static double FitDouble(double a)
        {
            if (Math.Abs(a) < 1e-10)
            {
                return 0;
            }
            return a;
        }

        public static double CubeRoot(double a, double b)
        {
            if (a >= 0)
            {
                return Math.Pow(a, b);
            }
            else
            {
                return -Math.Pow(-a, b);
            }
        }

        public static string ResolveEquation(double a, double b, double c, double d)
        {
            string result = "";
            if (!Utils.DoubleEquals(a, 0))
            {
                //三次方程
                double A = b * b - 3 * a * c;
                double B = b * c - 9 * a * d;
                double C = c * c - 3 * b * d;
                double delta = B * B - 4 * A * C;

                if (Utils.DoubleEquals(A, 0) && Utils.DoubleEquals(B, 0))
                {
                    double x = -b / (3 * a);
                    result = "x1=x2=x3=" + Utils.FitDouble(x);
                }
                else if (Utils.DoubleEquals(delta, 0))
                {
                    if (!Utils.DoubleEquals(A, 0))
                    {
                        double K = B / A;
                        double x1 = -b / a + K;
                        double x2 = -K / 2;
                        result = "x1=" + Utils.FitDouble(x1) + "\r\nx2=x3=" + Utils.FitDouble(x2);
                    }
                }
                else if (delta > 0)
                {
                    double Y1 = A * b + 3 * a * (-B + Math.Sqrt(B * B - 4 * A * C)) / 2;
                    double Y2 = A * b + 3 * a * (-B - Math.Sqrt(B * B - 4 * A * C)) / 2;
                    double x1 = (-b - Utils.CubeRoot(Y1, 1.0 / 3) - Utils.CubeRoot(Y2, 1.0 / 3)) / (3 * a);
                    double x2_1 = (-2 * b + Utils.CubeRoot(Y1, 1.0 / 3) + Utils.CubeRoot(Y2, 1.0 / 3)) / (6 * a);
                    double x2_2 = Math.Sqrt(3) * (Utils.CubeRoot(Y1, 1.0 / 3) - Utils.CubeRoot(Y2, 1.0 / 3)) / (6 * a);
                    result = "x1=" + Utils.FitDouble(x1) + "\r\nx2 = " + Utils.FitDouble(x2_1) + "+" + Utils.FitDouble(x2_2) + "i\r\nx3=" + +Utils.FitDouble(x2_1) + "-" + Utils.FitDouble(x2_2) + "i";
                }
                else if (delta < 0)
                {
                    if (A > 0)
                    {
                        double T = (2 * A * b - 3 * a * B) / Utils.CubeRoot(2 * A, 3.0 / 2);
                        double ceta = Math.Atan(Math.Cos(T));
                        double x1 = (-b - 2 * Math.Sqrt(A) * Math.Cos(ceta / 3)) / (3 * a);
                        double x2 = (-b + Math.Sqrt(A) * Math.Cos(ceta / 3) + Math.Sqrt(3) * Math.Sin(ceta / 3)) / (3 * a);
                        double x3 = (-b + Math.Sqrt(A) * Math.Cos(ceta / 3) - Math.Sqrt(3) * Math.Sin(ceta / 3)) / (3 * a);
                        result = "x1=" + Utils.FitDouble(x1) + "\r\nx2 = " + Utils.FitDouble(x2) + "\r\nx3=" + Utils.FitDouble(x3);
                    }
                }
            }
            else if (!Utils.DoubleEquals(b, 0))
            {
                //二次
                double A = b;
                double B = c;
                double C = d;
                double delta = B * B - 4 * A * C;
                if (Utils.DoubleEquals(delta, 0))
                {
                    double x = -B / (2 * A);
                    result = "x1=x2=" + x;
                }
                else if (delta >= 0)
                {
                    double x1 = (-B + Math.Sqrt(delta)) / (2 * A);
                    double x2 = (-B - Math.Sqrt(delta)) / (2 * A);
                    result = "x1=" + x1 + "\r\nx2=" + x2;
                }
                else
                {
                    double x_1 = -B / (2 * A);
                    double x_2 = Math.Sqrt(-delta) / (2 * A);
                    result = "x1=" + x_1 + "+" + x_2 + "i\r\nx2=" + x_1 + "-" + x_2 + "i";
                }
            }
            else if (!Utils.DoubleEquals(c, 0))
            {
                //一次
                double x = -d / c;
                result = "x=" + x;
            }
            else if (!Utils.DoubleEquals(d, 0))
            {
                result = "无解";
            }
            else if (Utils.DoubleEquals(d, 0))
            {
                result = "任意解";
            }
            return result;
        }
    }
}
