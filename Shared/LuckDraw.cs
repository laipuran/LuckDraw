using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LuckDrawWindow
{
    interface LuckDraw
    {
        public string LuckDrawer(int number, int max)
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

            string output;
            output = "被抽中的幸运同学：\n" + string.Join("\n", array);
            return output;
        }
    }
}
