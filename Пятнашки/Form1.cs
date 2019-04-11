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

    public partial class fm1 : Form
    {
        SortedSet<Results>[] mas;

        public string Name;
        Basic game, old;
        Timer time = new Timer();

        int TimerCount = 0;
        public fm1()
        {
            InitializeComponent();
            time.Tick += new EventHandler(Tick_);
            time.Interval = 1000;
        }

        private void fm1_Load(object sender, EventArgs e)
        {
            dg1.RowCount = 4;
            dg1.ColumnCount = 4;

            dg1.Width = 323;
            dg1.Height = 323;
            dg1.Visible = false;
            for (int i = 0; i < 4; i++)
            {
                dg1.Columns[i].Width = 80;
                dg1.Rows[i].Height = 80;
            }
            textBox1.Text = 0.ToString();
            Check();
                }
        private void showdg1()
        {
            for (int i = 0; i < 4; i++)
                for (int g = 0; g < 4; g++)
                {
                    dg1.Rows[i].Cells[g].ReadOnly = true;
                    dg1.Rows[i].Cells[g].Value = game.mas[i, g];
                    dg1.Rows[i].Cells[g].Selected = false;
                    dg1.Rows[game.strok].Cells[game.stolb].Value = "";
                 }
            Check();
        }
        public void SetName(string name)
        {
            Name = name;
        }
        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (game != null)
            {
                if (game.Replace(e.RowIndex, e.ColumnIndex))
                {
                    showdg1();
                    textBox1.Text = (Int32.Parse(textBox1.Text) + 1).ToString();
                }
            }
        }
        private void NewGame()
        {
            button3.Enabled = true;
            dg1.Enabled = true;
            game = new Basic();
            old = new Basic();
            for (int i = 0; i < 4; i++)
            {
                for (int g = 0; g < 4; g++)
                {
                    old.mas[i, g] = game.mas[i, g];
                }
            }
            old.stolb = game.stolb;
            old.strok = game.strok;
            for (int g = 0; g < 16; g++)
            {
                old.vol[g] = game.vol[g];
            }

            dg1.Visible = true ;       
            textBox1.Text = 0.ToString();
            if (game.IsPossible()) label1.Text = "Possible";
            else label1.Text = "Impossible";         
            time.Start();
            TimerCount = 0;
            Check();
            showdg1();
        }
        private void Repeat()
        {
            button3.Enabled = true;
            dg1.Enabled = true;
            for (int i = 0; i < 4; i++)
            {
                for (int g = 0; g < 4; g++)
                {
                    game.mas[i, g] = old.mas[i, g];
                }
            }
            game.stolb = old.stolb;
            game.strok = old.strok;
            for (int g = 0; g < 16; g++)
            {
                game.vol[g] = old.vol[g];
            }

            textBox1.Text = 0.ToString();
            if (game.IsPossible()) label1.Text = "Possible";
            else label1.Text = "Impossible";
            time.Start();
            TimerCount = 0;
            showdg1();
            Check();
        }
        private void NewPossibleGame()
        {
            button3.Enabled = true;
            old = new Basic();
            dg1.Enabled = true;
            dg1.Visible = true;
            do
            {
                game = new Basic();
            } while (!game.IsPossible());

            for (int i = 0; i < 4; i++)
            {
                for (int g = 0; g < 4; g++)
                {
                    old.mas[i, g] = game.mas[i, g];
                }
            }
            old.stolb = game.stolb;
            old.strok = game.strok;
            for (int g = 0; g < 16; g++)
            {
                old.vol[g] = game.vol[g];
            }

            if (game.IsPossible()) label1.Text = "Possible";
            else label1.Text = "Impossible";
            TimerCount = 0;
            time.Start();
            textBox1.Text = 0.ToString();
            showdg1();
            Check();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           NewGame();
        }

        private void label1_Click(object sender, EventArgs e)
        {
                MessageBox.Show("Why did you do this? OK, i will make new POSSIBLE game");
                NewPossibleGame();

        }

        private void button2_Click(object sender, EventArgs e)
        {
              if(game!=null)  Repeat();
          //  game.Change();
          //  showdg1();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (game != null)
            {

                for (int i = 0; i < 10;)
                {
                    if (game.Shag())
                    {
                        Application.DoEvents();
                        showdg1();
                        textBox1.Text = (Int32.Parse(textBox1.Text) + 1).ToString();
                        System.Threading.Thread.Sleep(400);
                        i++;
                    }
                }
            }
        }
        private void Check()
        {
            if (game != null)
            {
                int k = 0;
                int l = 1;

                for (int i = 0; i < 4; i++)
                    for (int g = 0; g < 4; g++)
                    {
                        if (game.mas[i, g] == l)
                            k++;
                        l++;

                    }
                //if(game.mas[0,0]==1)
                if (k == 16)
                {
                    time.Stop();
                    button3.Enabled = false;
                    MessageBox.Show("You won!!!");
                    dg1.Enabled = false;
                    using (FileStream fr = new FileStream("data.dat", FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        mas = (SortedSet<Results>[])bf.Deserialize(fr);
                    }
                    Results f0 = new Results(0, Name, DateTime.Now, TimerCount, Int32.Parse(textBox1.Text));
                    Results f1 = new Results(1, Name, DateTime.Now, TimerCount, Int32.Parse(textBox1.Text));
                    Results f2 = new Results(2, Name, DateTime.Now, TimerCount, Int32.Parse(textBox1.Text));
                    if (mas[0].Count < 10 && mas[1].Count < 10 && mas[2].Count < 10)
                    {
                        mas[0].Add(f0);
                        mas[1].Add(f1);
                        mas[2].Add(f2);
                    }
                    using (FileStream fs = new FileStream("data.dat", FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, mas);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Best f = new Best();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (game != null)
            {
                int k = 0;
                int l = 1;

                for (int i = 0; i < 4; i++)
                    for (int g = 0; g < 4; g++)
                    {
                        if (game.mas[i, g] == l)
                            k++;
                        l++;

                    }
                //if(game.mas[0,0]==1)
                if (k == 16)
                {
                    time.Stop();
                    button3.Enabled = false;
                    MessageBox.Show("You won!!!");
                    dg1.Enabled = false;
                    using (FileStream fr = new FileStream("data.dat", FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        mas = (SortedSet<Results>[])bf.Deserialize(fr);
                    }
                    Results f0 = new Results(0, Name, DateTime.Now, TimerCount, Int32.Parse(textBox1.Text));
                    Results f1 = new Results(1, Name, DateTime.Now, TimerCount, Int32.Parse(textBox1.Text));
                    Results f2 = new Results(2, Name, DateTime.Now, TimerCount, Int32.Parse(textBox1.Text));
                    if (mas[0].Count < 10 && mas[1].Count < 10 && mas[2].Count < 10)
                    {
                        mas[0].Add(f0);
                        mas[1].Add(f1);
                        mas[2].Add(f2);
                    }
                    using (FileStream fs = new FileStream("data.dat", FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, mas);
                    }
                }
            }
        }

        private void Tick_(object sender, EventArgs e)
        {
           textBox2.Text = (TimerCount++).ToString();
        }
    }
}
