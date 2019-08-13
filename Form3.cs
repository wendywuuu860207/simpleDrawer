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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float rf = (float)trackBar1.Value;
            rf /= 10;
            label5.Text = rf.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            float rf = (float)trackBar2.Value;
            rf /= 10;
            label6.Text = rf.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            float rf = (float)trackBar3.Value;
            rf /= 10;
            label7.Text = rf.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            float rf = (float)trackBar4.Value;
            rf /= 10;
            label8.Text = rf.ToString();
        }

        public float getR()
        {
            float rf = (float)trackBar1.Value;
            rf /= 10;
            return rf;
        }
        public float getG()
        {
            float rf = (float)trackBar2.Value;
            rf /= 10;
            return rf;
        }

        public float getB()
        {
            float rf = (float)trackBar3.Value;
            rf /= 10;
            return rf;
        }
        public float getA()
        {
            float rf = (float)trackBar4.Value;
            rf /= 10;
            return rf;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
