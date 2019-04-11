using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Пятнашки
{
    public partial class Best : Form
    {
        SortedSet<Results>[] mas;
        TextBox[] m = new TextBox[10];
        int X = 0;
        public Best()
        {
            InitializeComponent();
            using (FileStream fr = new FileStream("data.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                mas = (SortedSet<Results>[])bf.Deserialize(fr);
            }
            m[0] = textBox1;
            m[1] = textBox2;
            m[2] = textBox3;
            m[3] = textBox4;
            m[4] = textBox5;
            m[5] = textBox6;
            m[6] = textBox7;
            m[7] = textBox8;
            m[8] = textBox9;
            m[9] = textBox10;
            Show();
        }
        private void Show()
        {
            if (X == 0) label1.Text = "По продолжительности игры";
            else if (X == 1) label1.Text = "По количеству шагов";
            else label1.Text = "По дате";
            for (int i = 0; i < 10; i++)
            {
                m[i].Text = "";
            }
            for (int i = 0; i < mas[X].Count; i++)
            {
                m[i].Text = mas[X].ElementAt(i).ToString();
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            X++;
            if (X == 3) X = 0;
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime.TryParse(textBox11.Text, out DateTime res);
            if (res <= DateTime.Today)
            {
                int k = 0;
                 int l0 = mas[0].Count;
                int l1 = mas[1].Count;
                int l2 = mas[2].Count;
                for (int i = 0; i < l0; i++)
                {
                    if (mas[0].ElementAt(k).Date.Date <= res)
                        mas[0].Remove(mas[0].ElementAt(k));
                    else k++;
                }
                k = 0;
                for (int i = 0; i < l1; i++)
                {
                    if (mas[1].ElementAt(k).Date.Date <= res)
                        mas[1].Remove(mas[1].ElementAt(k));
                    else k++;
                }
                k = 0;
                for (int i = 0; i < l2; i++)
                {
                    if (mas[2].ElementAt(k).Date.Date <= res)
                        mas[2].Remove(mas[2].ElementAt(k));
                    else k++;
                }
            }

            using (FileStream fs = new FileStream("data.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, mas);
            }
            Show();
            
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Best_Load(object sender, EventArgs e)
        {
            Show();
            m[0] = textBox1;
            m[1] = textBox2;
            m[2] = textBox3;
            m[3] = textBox4;
            m[4] = textBox5;
            m[5] = textBox6;
            m[6] = textBox7;
            m[7] = textBox8;
            m[8] = textBox9;
            m[9] = textBox10;
        }
    }
}
