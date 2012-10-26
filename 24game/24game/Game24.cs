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

namespace _24game
{
    public class TOperData
    {
        public int operType = -1;
        public double data = 0;
    }
    public class Game24
    {


        // 检查计算的参数是否合法，也可以用于过滤一些重复的表达式
        private bool checkjisuan(double a, double b, int op)
        {
            if (op == '/' && b < 1e-6 && b > -1e-6)
                return false;
            if (op == '-' && b < 1e-6 && b > -1e-6)
                return false;
            if (op == '/'
                    && (Math.Abs(b - 1.0f) < 1e-6 || Math.Abs(b + 1.0f) < 1e-6))
                return false;
            if (op == '+' || op == '*')
                return a <= b;
            return true;
        }

        // 消除重复
        private bool checkOp(TOperData[] computeData)
        {
            for (int i = 0; i < 6; i++)
            {
                int type1 = computeData[i].operType;
                int type2 = computeData[i + 1].operType;
                if (type1 == '-' && (type2 == '+' || type2 == '-'))
                {
                    return false;
                }
                else if (type1 == '/' && (type2 == '*' || type2 == '/'))
                {
                    return false;
                }
            }
            return true;
        }

        // 求值
        private double jisuan(double a, double b, int op)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    return -1;
            }
        }

        // 计算表达式的值
        private double process(TOperData[] data)
        {
            int len = 7;
            Stack<TOperData> suffix = new Stack<TOperData>();

            for (int i = 0; i < len; i++)
            {
                if (data[i].operType == 0)
                {
                    suffix.Push(data[i]);
                }
                else if (data[i].operType > 0)
                {
                    if (suffix.Count == 0)
                        return -1;
                    if (suffix.Peek().operType == 0)
                    {
                        double a = suffix.Peek().data;
                        suffix.Pop();
                        if (!(suffix.Count == 0) && suffix.Peek().operType == 0)
                        {
                            double b = suffix.Peek().data;
                            suffix.Pop();
                            if (!checkjisuan(b, a, data[i].operType))
                                return -1;
                            double c = jisuan(b, a, data[i].operType);
                            TOperData opdata = new TOperData();
                            opdata.operType = 0;
                            opdata.data = c;
                            suffix.Push(opdata);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (suffix.Count == 0)
                return -1;
            if (suffix.Peek().operType == 0)
            {
                double r = suffix.Peek().data;
                suffix.Pop();
                if (suffix.Count == 0)
                    return r;
            }
            return -1;
        }

        int op(int x)
        {
            if (x == '+')
                return 1;
            else if (x == '-')
                return 1;
            else if (x == '*')
                return 2;
            else if (x == '/')
                return 2;
            else
                return 0;
        }

        // 后缀转中缀
        private String posttomid(TOperData[] data)
        {
            // 后缀转回中缀
            Stack<TOperData> ope = new Stack<TOperData>();
            Stack<String> opn = new Stack<String>();
            String tmp1, tmp2;
            for (int i = 0; i < 7; i++)
            {
                if (data[i].operType == 0)
                {
                    String tt = "";
                    tt += (int)data[i].data;
                    opn.Push(tt);
                    ope.Push(data[i]);
                }
                else
                {
                    if (op(ope.Peek().operType) != 0
                            && (op(data[i].operType) > op(ope.Peek().operType) || (op(data[i].operType) == op(ope
                                    .Peek().operType) && (data[i].operType == '-' || data[i].operType == '/'))))
                    {
                        tmp2 = "(";
                        tmp2 += opn.Peek();
                        tmp2 += ")";
                    }
                    else
                    {
                        tmp2 = opn.Peek();
                    }

                    opn.Pop();
                    ope.Pop();
                    if (op(ope.Peek().operType) != 0
                            && op(data[i].operType) > op(ope.Peek().operType))
                    {
                        tmp1 = "(";
                        tmp1 += opn.Peek();
                        tmp1 += ")";
                    }
                    else
                    {
                        tmp1 = opn.Peek();
                    }
                    ope.Pop();
                    opn.Pop();
                    tmp1 += (char)data[i].operType;
                    tmp1 += tmp2;
                    opn.Push(tmp1);
                    ope.Push(data[i]);
                }
            }
            String result = opn.Peek();
            return result;
        }


        public List<string> start(int[] data, bool bAll)
        {
            List<string> result = new List<string>();
            int[][] shunxu = new int[][] { new int[] { 1, 1, 1, 1, 2, 2, 2 },
				new int[] { 1, 1, 1, 2, 1, 2, 2 },
				new int[] { 1, 1, 1, 2, 2, 1, 2 },
				new int[] { 1, 1, 2, 1, 1, 2, 2 },
				new int[] { 1, 1, 2, 1, 2, 1, 2 } };
            int[] operAll = { '+', '-', '*', '/' };
            TOperData[] computeData = new TOperData[7];
            int[] copydata = new int[4];
            copydata[0] = data[0];
            copydata[1] = data[1];
            copydata[2] = data[2];
            copydata[3] = data[3];
            // 5个后缀表达式遍历
            for (int m = 0; m < 5; m++)
            {
                data[0] = copydata[0];
                data[1] = copydata[1];
                data[2] = copydata[2];
                data[3] = copydata[3];
                do
                {
                    int[] oper = new int[3];
                    for (int n = 0; n < 4; n++)
                    {
                        oper[0] = operAll[n];
                        for (int nn = 0; nn < 4; nn++)
                        {
                            oper[1] = operAll[nn];
                            for (int nnn = 0; nnn < 4; nnn++)
                            {
                                oper[2] = operAll[nnn];
                                int j = 0;
                                int k = 0;
                                for (int i = 0; i < 7; i++)
                                {
                                    computeData[i] = new TOperData();
                                    if (shunxu[m][i] == 1)
                                    {
                                        computeData[i].operType = 0;
                                        computeData[i].data = data[j++];
                                    }
                                    else if (shunxu[m][i] == 2)
                                    {
                                        computeData[i].operType = oper[k++];
                                    }
                                }
                                if (!checkOp(computeData))
                                    continue;
                                double r = process(computeData);
                                if (r - 24 > -1e-3 && r - 24 < 1e-3)
                                {
                                    result.Add(posttomid(computeData));
                                    if (!bAll)
                                        return result;
                                }
                            }
                        }
                    }
                } while (Utils.NextPerm<int>(data));
            }
            return result;
        }

    }

}
