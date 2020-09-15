using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fun_SoundLean
{
    public partial class Form1 : Form
    {
        private float x0 = 200, y0 = 100;
        private float r = 5;
        private float l = 100, d = 1;

        private float x1 = 200, y1 = 100;//鼠标位置
        private float x2 = 300, y2 = 100;//直线一端位置
        private float x3 = 100, y3 = 100;//直线另一端的位置
        private float flag;
        private float L;

        private float x4 = 200, y4 = 100;//球的位置
        private float LL = 10000;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.FillEllipse(new SolidBrush(BackColor), x4 - r, y4 - r, 2 * r, 2 * r);
            if (Math.Abs(x0 - x1) > Math.Abs(y0 - y1))
            {
                LL = (x2 - x4) * (x2 - x4) + (y2 - y4) * (y2 - y4);
                g.DrawLine(new Pen(BackColor), x2, y2, x3, y3);
                L = (float)Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
                x2 = x0 + ((x1 - x0) * l) / L;
                y2 = y0 + ((y1 - y0) * l) / L;
                x3 = 2 * x0 - x2;
                y3 = 2 * y0 - y2;
                if (x2 < x3)
                {
                    flag = x2; x2 = x3; x3 = flag;
                    flag = y2; y2 = y3; y3 = flag;
                }
                x4 = x2 + (x3 - x2) * (float)Math.Sqrt(LL) / 200;
                y4 = y2 + (y3 - y2) * (float)Math.Sqrt(LL) / 200;
            }
            if ((x0 - x4) * (x0 - x4) + (y0 - y4) * (y0 - y4) <= 10000)
            {
                if (y3 > y2)
                {
                    x4 = x4 - (x2 - x3) * (float)0.01;
                    y4 = y4 + (y3 - y2) * (float)0.01;
                }
                else if (y3 < y2)
                {
                    x4 = x4 + (x2 - x3) * (float)0.01;
                    y4 = y4 + (y2 - y3) * (float)0.01;
                }
            }
            else//若没有以下代码，当圆运动到或超过端点时，它会一直停留在该端点
            {
                if (x4 < x0)
                {
                    if (y3 > y2)
                    {
                        x4 = x4 + (x2 - x3) * (float)0.001;
                        y4 = y4 - (y3 - y2) * (float)0.001;
                    }
                    else if (y3 < y2)
                    {
                        x4 = x4 + (x2 - x3) * (float)0.001;
                        y4 = y4 + (y2 - y3) * (float)0.001;
                    }
                }
                else if(x4>x0)
                {
                    if (y3 > y2)
                    {
                        x4 = x4 - (x2 - x3) * (float)0.01;
                        y4 = y4 + (y3 - y2) * (float)0.01;
                    }
                    else if(y3<y2)
                    {
                        x4 = x4 - (x2 - x3) * (float)0.001;
                        y4 = y4 - (y2 - y3) * (float)0.001;
                    }
                }
            }
            g.DrawLine(Pens.Gray, x2, y2, x3, y3);
            g.FillEllipse(new SolidBrush(Color.Red), x4 - r, y4 - r, 2 * r, 2 * r);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
