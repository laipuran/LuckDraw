using System;
using System.Threading;

namespace LuckDraw
{
    public class Union
    {
        public int number;
        public string message;
    };
    public class Algorithm
    {
        public static Union Parser(string str, int max)            // String转换成Int
        {
            Union union = new Union();
            union.number = 0;
            union.message = "";
            int number;
            try
            {
                number = int.Parse(str);
                if (number <= 0)
                {
                    throw new MyEx("输入的数字不合法！");
                }
                if (number > max)
                {
                    throw new MyEx("输入的数字超过总人数！");
                }
            }
            catch (Exception Ex)
            {
                union.message = Ex.Message;
                return union;
            }
            union.number = int.Parse(str);
            return union;
        }
        public static string Getter(int number, int max)
        {

            int[] array = new int[number];
            int[] check = new int[max];
            Array.Clear(array, 0, number);
            Array.Clear(check, 0, max);

            Random r = new Random();
            for (int i = 0; i < number; i++)
            {
                int temp = r.Next(1, max + 1);
                check[temp - 1]++;
                Thread.Sleep(10);
            }

            bool chk = true;
            while (chk == true)
            {
                for (int i = 0; i < max; i++)
                {
                    if (check[i] > 1)
                    {
                        check[i]--;
                        int temp = r.Next(1, max + 1);
                        check[temp - 1]++;
                    }
                }
                int index = 0;
                for (int i = 0; i < max; i++)
                {
                    if (check[i] == 1 || check[i] == 0)
                    {
                        if (check[i] == 1)
                        {
                            array[index] = i + 1;
                            index++;
                        }
                        if (i == max - 1)
                        {
                            chk = false;
                        }
                    }
                    else break;
                }

            }

            for (int i = number - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp;
                        temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
            
            return "被抽中的幸运同学：\n" + string.Join("\n", array);
        }
    }
    public class MyEx : Exception
    {
        public MyEx(string message) : base(message) { }
    }
}
