using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class Player : Form
    {
        public Player()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                fm1 f = new fm1();
                f.SetName(textBox1.Text.ToString());
                Hide();
                f.FormClosed += new FormClosedEventHandler(form_FormClosed);
                f.Show();
            }
           else  MessageBox.Show("Enter the name");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
