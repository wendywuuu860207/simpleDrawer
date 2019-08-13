using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace _1053332_HW07
{
    public partial class Form1 : Form
    {
        int w, h, x0, y0, pen_width = 1;
        Bitmap img1, img2, BackupImg, BackupImg2;
        Pen p;
        int tools = 0;
        Color c = Color.Black;
        float r3, g3, b3, a3;
        SolidBrush b = new SolidBrush(Color.Black);
        bool fillThepic = true;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            p = new Pen(c, pen_width);
            toolStripStatusLabel1.Text = "Width: " + w.ToString() + ", Height: " + h.ToString();
            toolStripStatusLabel3.Text = "Pen : " + c.ToString();
            toolStripStatusLabel4.Text = "Brush : " + b.Color.ToString();
            復原ToolStripMenuItem.Enabled = false;
            取消復原ToolStripMenuItem.Enabled = false;
        }

        private void 灰階ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                ColorMatrix cm = new ColorMatrix(
                       new float[][]{ new float[]{0.33f, 0.33f, 0.33f, 0, 0},
                                  new float[]{0.33f, 0.33f, 0.33f, 0, 0},
                                  new float[]{0.33f, 0.33f, 0.33f, 0, 0},
                                  new float[]{  0,    0,    0,  1, 0},
                                  new float[]{  0,    0,    0,  0, 1}});
                pictureBox1.Image = ConvertCM(pictureBox1.Image, cm);
            }
        }

        private void 負片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                ColorMatrix cm = new ColorMatrix(
                   new float[][]{ new float[]{ -1f,    0,    0,  0, 0},
                                  new float[]{  0,   -1f,    0,  0, 0},
                                  new float[]{  0,    0,   -1f,  0, 0},
                                  new float[]{  0,    0,    0,  1, 0},
                                  new float[]{  1,    1,    1,  0, 1}});
                pictureBox1.Image = ConvertCM(pictureBox1.Image, cm);
            }
        }

        private void 自定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Form3 y = new Form3();
                y.TopMost = true;  //移到最上層
                y.Text = "調整畫布的顏色"; //Form3 的標題
                y.ShowDialog();    // Show Form3
                r3 = y.getR();
                g3 = y.getG();
                b3 = y.getB();
                a3 = y.getA();
                ColorMatrix cm = new ColorMatrix(
                    new float[][]{
                    new float[]{ r3,    0,    0,  0, 0},
                    new float[]{  0,   g3,    0,  0, 0},
                    new float[]{  0,    0,   b3,  0, 0},
                    new float[]{  0,    0,    0,  a3, 0},
                    new float[]{  0,    0,    0,  0, 1}     });
                pictureBox1.Image = ConvertCM(pictureBox1.Image, cm);
            }
        }

        private void 一半ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupImg = (Bitmap)pictureBox1.Image.Clone();
            復原ToolStripMenuItem.Enabled = true;
            w = pictureBox1.Image.Width;
            h = pictureBox1.Image.Height;
            img1 = (Bitmap)pictureBox1.Image;
            Bitmap p = new Bitmap(w / 2, h / 2);
            for (int i = 0; i < w; i += 2)
                for (int j = 0; j < h; j += 2)
                {
                    Color c = img1.GetPixel(i, j);
                    int i2 = i / 2;
                    int j2 = j / 2;
                    if ((i2 < w) && (j2 < h))
                        p.SetPixel(i2, j2, c);
                }
            pictureBox1.Image = p;
            img1 = p;
            w = p.Width;
            h = p.Height;
            pictureBox1.Refresh();
            toolStripStatusLabel1.Text = "Width: " + w.ToString() + ", Height: " + h.ToString();
        }

        private void 二倍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupImg = (Bitmap)pictureBox1.Image.Clone();
            復原ToolStripMenuItem.Enabled = true;
            w = pictureBox1.Image.Width;
            h = pictureBox1.Image.Height;
            Bitmap p = new Bitmap(w * 2, h * 2);
            img1 = (Bitmap)pictureBox1.Image;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color c = img1.GetPixel(i, j);
                    for (int ii = 0; ii < 2; ii++)
                        for (int jj = 0; jj < 2; jj++)
                            p.SetPixel(i * 2 + ii, j * 2 + jj, c); //垂直與水平方向都重複畫兩遍
                }
            pictureBox1.Image = p;
            img1 = p;
            w = p.Width;
            h = p.Height;
            pictureBox1.Refresh();
            toolStripStatusLabel1.Text = "Width: " + w.ToString() + ", Height: " + h.ToString();
            if ((this.ClientSize.Width < w) || (this.ClientSize.Height < (h + 56)))
                this.ClientSize = new Size(w, h + 56);
        }

        private void 增亮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                ColorMatrix cm = new ColorMatrix(
                   new float[][]{ new float[]{  1.1f,    0,    0,  0, 0},
                                  new float[]{  0,    1.1f,    0,  0, 0},
                                  new float[]{  0,    0,    1.1f,  0, 0},
                                  new float[]{  0,    0,    0,  1, 0},
                                  new float[]{  0,    0,    0,  0, 1}});
                pictureBox1.Image = ConvertCM(pictureBox1.Image, cm);
            }
        }


        private void 調暗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                ColorMatrix cm = new ColorMatrix(
                   new float[][]{ new float[]{  0.9f,    0,    0,  0, 0},
                                  new float[]{  0,    0.9f,    0,  0, 0},
                                  new float[]{  0,    0,    0.9f,  0, 0},
                                  new float[]{  0,    0,    0,  1, 0},
                                  new float[]{  0,    0,    0,  0, 1}});
                pictureBox1.Image = ConvertCM(pictureBox1.Image, cm);
            }
        }


        private void 點ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            點ToolStripMenuItem.Checked = true;
            直線ToolStripMenuItem.Checked = false;
            矩形ToolStripMenuItem.Checked = false;
            圓形ToolStripMenuItem.Checked = false;
            tools = 0;
        }

        private void 色彩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                c = colorDialog1.Color;
                p = new Pen(c, pen_width);
                toolStripStatusLabel3.Text = "Pen : " + c.ToString();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            pen_width = 1;
            toolStripMenuItem2.Checked = true;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;
            p = new Pen(c, pen_width);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            pen_width = 2;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = true;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;
            p = new Pen(c, pen_width);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            pen_width = 3;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = false;
            p = new Pen(c, pen_width);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            pen_width = 4;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = true;
            toolStripMenuItem6.Checked = false;
            p = new Pen(c, pen_width);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            pen_width = 5;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            toolStripMenuItem6.Checked = true;
            p = new Pen(c, pen_width);
        }

        private void 復原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            復原ToolStripMenuItem.Enabled = false;
            取消復原ToolStripMenuItem.Enabled = true;
            BackupImg2 = (Bitmap)pictureBox1.Image;
            pictureBox1.Image = BackupImg;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                x0 = e.X;
                y0 = e.Y;
                img2 = (Bitmap)pictureBox1.Image.Clone();
                BackupImg = (Bitmap)pictureBox1.Image.Clone();
            }
        }

        private void 顏色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                b = new SolidBrush(colorDialog1.Color);
                toolStripStatusLabel4.Text = "Brush : " + b.Color.ToString();
            }
        }

        private void 填滿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillThepic = true;
            填滿ToolStripMenuItem.Checked = true;
            無填滿ToolStripMenuItem.Checked = false;
        }

        private void 無填滿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillThepic = false;
            填滿ToolStripMenuItem.Checked = false;
            無填滿ToolStripMenuItem.Checked = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if ((e.X < w) && (e.Y < h) && (e.X > 0) && (e.Y > 0))
                {
                    toolStripStatusLabel2.Text = e.Location.ToString();
                }
                else
                {
                    toolStripStatusLabel2.Text = "{,}";
                }
                if (e.Button == MouseButtons.Left)
                {
                    if (tools == 0)
                    {
                        Graphics g = Graphics.FromImage(pictureBox1.Image);
                        //Method 1: 直接改pictureBox1的圖
                        if ((e.X < w) && (e.Y < h) && (e.X > 0) && (e.Y > 0))
                        {
                            g.DrawLine(p, x0, y0, e.X, e.Y);
                            x0 = e.X;
                            y0 = e.Y;
                            pictureBox1.Refresh();
                        }
                    }
                    else if (tools == 1)
                    {
                        if ((e.X < w) && (e.Y < h) && (e.X > 0) && (e.Y > 0))
                        {
                            Bitmap tempImg = (Bitmap)img2.Clone();
                            //Method 2: 複製一份點陣圖，最後再放回pictureBox1
                            Graphics gg = Graphics.FromImage(tempImg);
                            gg.DrawLine(p, x0, y0, e.X, e.Y);
                            pictureBox1.Image = tempImg;
                            pictureBox1.Refresh();
                        }
                    }
                    else if(tools == 2)
                    {
                        if ((e.X < w) && (e.Y < h) && (e.X > 0) && (e.Y > 0))
                        {
                            Bitmap tempImg = (Bitmap)img2.Clone();
                            //Method 2: 複製一份點陣圖，最後再放回pictureBox1
                            Graphics ggg = Graphics.FromImage(tempImg);
                            int theWidth = e.X - x0;
                            int theHeight = e.Y - y0;
                            int recX = x0;
                            int recY = y0;
                            if (e.X < x0)
                            {
                                theWidth = x0 - e.X;
                                recX = e.X;
                            }
                            if (e.Y < y0)
                            {
                                theHeight = y0 - e.Y;
                                recY = e.Y;
                            }
                            if (!fillThepic)
                                ggg.DrawRectangle(p, recX, recY, theWidth, theHeight);
                            else
                            {
                                ggg.FillRectangle(b, recX, recY, theWidth, theHeight);
                                ggg.DrawRectangle(p, recX, recY, theWidth, theHeight);
                            }
                            pictureBox1.Image = tempImg;
                            pictureBox1.Refresh();
                        }
                    }
                    else if(tools == 3)
                    {
                        if ((e.X < w) && (e.Y < h) && (e.X > 0) && (e.Y > 0))
                        {
                            Bitmap tempImg = (Bitmap)img2.Clone();
                            //Method 2: 複製一份點陣圖，最後再放回pictureBox1
                            Graphics gggg = Graphics.FromImage(tempImg);
                            if (!fillThepic)
                                gggg.DrawEllipse(p, x0, y0, e.X - x0, e.Y - y0);
                            else
                            {
                                gggg.FillEllipse(b, x0, y0, e.X - x0, e.Y - y0);
                                gggg.DrawEllipse(p, x0, y0, e.X - x0, e.Y - y0);
                            }
                            pictureBox1.Image = tempImg;
                            pictureBox1.Refresh();
                        }
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            復原ToolStripMenuItem.Enabled = true;
        }

        private void 取消復原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            復原ToolStripMenuItem.Enabled = true;
            取消復原ToolStripMenuItem.Enabled = false;
            pictureBox1.Image = BackupImg2;
            pictureBox1.Refresh();
        }

        private void 直線ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            點ToolStripMenuItem.Checked = false;
            直線ToolStripMenuItem.Checked = true;
            矩形ToolStripMenuItem.Checked = false;
            圓形ToolStripMenuItem.Checked = false;
            tools = 1;
        }

        private void 矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            點ToolStripMenuItem.Checked = false;
            直線ToolStripMenuItem.Checked = false;
            矩形ToolStripMenuItem.Checked = true;
            圓形ToolStripMenuItem.Checked = false;
            tools = 2;
        }

        private void 圓形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            點ToolStripMenuItem.Checked = false;
            直線ToolStripMenuItem.Checked = false;
            矩形ToolStripMenuItem.Checked = false;
            圓形ToolStripMenuItem.Checked = true;
            tools = 3;
        }

        private void 儲存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String output = saveFileDialog1.FileName;
                    pictureBox1.Image.Save(output);
                    img1 = (Bitmap)pictureBox1.Image;
                }
        }

        private void 開啟新檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 x = new Form2();
            x.TopMost = true;  //移到最上層
            x.Text = "設定畫布的寬與高"; //Form2 的標題
            x.ShowDialog();    // Show Form2
            w = x.getWidth();
            h = x.getHeight();
            if (w != -1)
            {
                img1 = new Bitmap(w, h);
                pictureBox1.Image = img1;
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                g.Clear(Color.White);
                pictureBox1.Refresh(); // 要求重畫
                if ((this.ClientSize.Width < w) || (this.ClientSize.Height < (h + 56)))
                    this.ClientSize = new Size(w, h + 56);
                toolStripStatusLabel1.Text = "Width: " + w.ToString() + ", Height: " + h.ToString();
            }
        }

        private void 開啟舊檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)  // 開啟影像檔
            {
                String input = openFileDialog1.FileName;
                img1 = (Bitmap)Image.FromFile(input); // 產生一個Image物件
                w = img1.Width;
                h = img1.Height;
                if ((this.ClientSize.Width < w) || (this.ClientSize.Height < h))
                    this.ClientSize = new Size(w, h + 56);
                pictureBox1.Image = img1;
                pictureBox1.Refresh(); // 要求重畫
                toolStripStatusLabel1.Text = "Width: " + w.ToString() + ", Height: " + h.ToString();
            }
        }

        public Bitmap ConvertCM(Image image, ColorMatrix cm)
        {
            Bitmap dest = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(dest); // 從點陣圖 建立 新的畫布
            // cm 定義含有 RGBA 空間座標的 5 x 5 矩陣
            // (R, G, B, A, 1) 乘上 此矩陣
            // ImageAttributes 類別的多個方法會使用色彩矩陣來調整影像色彩
            ImageAttributes ia = new ImageAttributes();
            // 設定預設分類的色彩調整矩陣。
            ia.SetColorMatrix(cm);  
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ia);
            g.Dispose(); //清掉畫布與點陣圖變數的連結

            BackupImg = (Bitmap)pictureBox1.Image.Clone();
            復原ToolStripMenuItem.Enabled = true;

            return dest;
        }
    }
}
