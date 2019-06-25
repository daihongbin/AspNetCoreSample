using System;

namespace ConsoleAppInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            FourthTopic();
        }

        #region 0529 广州福思特面试
        //第一题
        public static void FirstTopic()
        {
            //第一次好像是利用switch输出数字对应的汉字
            for (int i = 1; i <= 5;i++)
            {
                var str = string.Empty;
                switch (i)
                {
                    case 1:
                        str = "1~一";
                        break;
                    case 2:
                        str = "2~二";
                        break;
                    case 3:
                        str = "3~三";
                        break;
                    case 4:
                        str = "4~四";
                        break;
                    case 5:
                        str = "5~五";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(str);
            }
        }

        //第二题
        public static void SecondTopic()
        {
            #region 从小到大
            //第二题好像是有三个变量，a = 5，b = 4，c = 6。然后使用if按顺序进行输出
            int a = 6, b = 4, c = 3, d = 2;

            //此处的思路为确保a为最小的
            int i = 0;
            if(a > b)
            {
                i = a;
                a = b;
                b = i;
            }

            if (a > c)
            {
                i = a;
                a = c;
                c = i;
            }

            if (a > d)
            {
                i = a;
                a = d;
                d = i;
            }

            if (b > c)
            {
                i = b;
                b = c;
                c = i;
            }

            if (b > d)
            {
                i = b;
                b = d;
                d = i;
            }

            if (c > d)
            {
                i = c;
                c = d;
                d = i;
            }

            Console.WriteLine($"abc从小到大按顺序输出为：{a} < {b} < {c} < {d}");
            #endregion

            #region 从大到小
            int e = 4, f = 2, g = 11;

            //同样的思路，确保第一个为最大即可
            int t = 0;
            if (e < f)
            {
                t = e;
                e = f;
                f = t;
            }

            if (e < g)
            {
                t = e;
                e = g;
                g = t;
            }

            if (f < g)
            {
                t = f;
                f = g;
                g = t;
            }

            Console.WriteLine($"abc从大到小按顺序输出为：{e} > {f} > {g}");
            #endregion
        }

        //第三题
        public static void ThirdTopic()
        {
            //第二题是一个取余数的题目，寻找符合一定取余数条件的数目。
            // /为取整数 %为取余数（亦为取模）
            for (int i = 0;i <= 200;i++)
            {
                if (i % 3 == 2 && i % 4 == 2 && i % 5 == 1)
                {
                    Console.WriteLine(i);
                }
            }
        }

        //第四题
        public static void FourthTopic()
        {
            //第四题是找100以内自然数的17的最大倍数
            var divde = 0;
            for (int i = 0;i <= 100;i++)
            {
                if (i % 17 == 0)
                    divde = i;
            }
            Console.WriteLine(divde);

            //另一种思路
            var maxDivde = 0;
            for (int i = 100;i >= 0;i--)
            {
                if (i % 17 == 0)
                {
                    maxDivde = i;
                    break;
                }
            }
            Console.WriteLine(maxDivde);
        }

        //第五题
        public static void FifthTopic()
        {
            //第五题，就是求一个数组内的和
            int[] numArray = { 1, 2, 3, 4, 4, 5, 56 };
            var total = 0;
            for (int i = 0;i < numArray.Length;i++)
            {
                total += numArray[i];
            }
            Console.WriteLine(numArray);
        }

        public static void SixthTopic()
        {

        }
        #endregion
    }
}
