using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1053332_HW07
{
    public partial class Form2 : Form
    {
        private int width = -1, height = -1;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                width = -1;
            else
                width = int.Parse(textBox1.Text);

            if (textBox2.Text == "")
                height = -1;
            else
                height = int.Parse(textBox2.Text);

            this.Close();
        }

        public int getWidth()
        { //For Form1 to get width
            return width;
        }
        public int getHeight()
        {
            return height;
        }
    }
}
