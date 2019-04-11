using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using static System.Math;
using System.Runtime.Serialization.Formatters.Binary;

namespace Пятнашки
{
    class Basic
    {
        public int[,] mas = new int[4, 4];
        public int[] vol = new int[16];
        public bool Active = false;
        public int stolb = 0;
       public int strok = 0;
        int vol_ = 0;
        public Basic()
        {
            New();
        }
        public void New()
        {
            bool[,] b = new bool[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int g = 0; g < 4; g++)
                {
                    b[i, g] = true;
                }
            }
            Random rand = new Random();
            for (int i = 1; i <= 16;)
            {
                int x, y;
                x = rand.Next(0, 4);
                y = rand.Next(0, 4);
                if (b[x, y] == true)
                {
                    mas[x, y] = i;
                    b[x, y] = false;
                    if (i == 16)
                    {
                        stolb = y;
                        strok = x;
                    }
                    i++;
                }

            }
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int g = 0; g < 4; g++)
                {
                    vol[k] = mas[i, g];
                    if (vol[k] == 16) vol_ = k;
                    k++;
                }
            }
        }
        public void Change()
        {
            int k = 1;
            for (int i = 0; i < 4; i++)
            {
                for (int g = 0; g < 4; g++)
                {
                    mas[i, g] = k;
                    k++;
                }          
            }
            strok = 3;
            stolb = 3;
        }
        public bool IsPossible()
        {
            int sum = 0;

            for (int i = 0; i < 16; i++)
            {
                if (vol[i] == 16) continue;
                for (int g = i; g < 16; g++)
                {
                    if (vol[i] > vol[g]) sum++;
                }
               
            }
            sum += (stolb+1);
            if (sum % 2 == 0) return false;
            else return true;
        }
        public bool Shag()
        {
            Random rand = new Random();
            int x, y, z;
            x = strok;
            y = stolb;
            z = rand.Next(0, 2);
           if(z==0) x = strok+rand.Next(-1, 2);
           else y = stolb + rand.Next(-1, 2);
            if (x >= 0 && x < 4 && y >= 0 && y < 4)
                if (Replace(x, y))
                {
                    return true;
                }
                else return false;
            else return false;

        }
        public bool Replace(int i, int g)
        {
            if ((Abs(strok - i) == 1 && stolb - g == 0) || (Abs(stolb - g) == 1 && strok - i == 0))
            {
                mas[strok, stolb] = mas[i, g];
                mas[i, g] = 16;
                strok = i;
                stolb = g;
                return true;
            }
            else return false;
        }
    }
}