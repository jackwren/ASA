using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Fern2 : Form
    {
        
        float xmin, ymin, xmax, ymax;
        float[] f1, f2, f3, f4;
        int w, h;

        protected override void OnPaint(PaintEventArgs pea)
        {
            float r, u0, un, v0, vn, x0, y0, xn, yn;
            float a = ((float) w) / (xmax - xmin);
            float b = -xmin * a;
            float c = ((float) h) / (ymin - ymax);
            float d = -ymax * c;
            int f1e = 0, f2e = 0, f3e = 0, f4e = 0;
            Graphics g = pea.Graphics;
            Pen pen = new Pen(Color.Green);
            Random random = new Random();
            DateTime dateTime0 = DateTime.Now;

            x0 = 0.0f;
            y0 = 0.0f;
            u0 = a * x0 + b;
            v0 = c * y0 + d;

            for (int i = 0; i < 100000; i++)
            {
                r = (float)random.NextDouble();
                if (r <= f1[6])
                {
                    xn = f1[0] * x0 + f1[1] * y0 + f1[4];
                    yn = f1[2] * x0 + f1[3] * y0 + f1[5];
                    un = a * xn + b;
                    vn = c * yn + d;
                    g.DrawEllipse(pen, un, vn, 2.0f, 2.0f);
                    x0 = xn;
                    y0 = yn;
                    u0 = un;
                    v0 = vn;
                    f1e++;
                }
                r = (float)random.NextDouble();
                if (r <= f2[6])
                {
                    xn = f2[0] * x0 + f2[1] * y0 + f2[4];
                    yn = f2[2] * x0 + f2[3] * y0 + f2[5];
                    un = a * xn + b;
                    vn = c * yn + d;
                    g.DrawEllipse(pen, un, vn, 2.0f, 2.0f);
                    x0 = xn;
                    y0 = yn;
                    u0 = un;
                    v0 = vn;
                    f2e++;
                }
                r = (float)random.NextDouble();
                if (r <= f3[6])
                {
                    xn = f3[0] * x0 + f3[1] * y0 + f3[4];
                    yn = f3[2] * x0 + f3[3] * y0 + f3[5];
                    un = a * xn + b;
                    vn = c * yn + d;
                    g.DrawEllipse(pen, un, vn, 2.0f, 2.0f);
                    x0 = xn;
                    y0 = yn;
                    u0 = un;
                    v0 = vn;
                    f3e++;
                }
                r = (float)random.NextDouble();
                if (r <= f4[6])
                {
                    xn = f4[0] * x0 + f4[1] * y0 + f4[4];
                    yn = f4[2] * x0 + f4[3] * y0 + f4[5];
                    un = a * xn + b;
                    vn = c * yn + d;
                    g.DrawEllipse(pen, un, vn, 2.0f, 2.0f);
                    x0 = xn;
                    y0 = yn;
                    u0 = un;
                    v0 = vn;
                    f4e++;
                }
            }

            DateTime dateTime1 = DateTime.Now;
            TimeSpan total = dateTime1.Subtract(dateTime0);

            Text += " " + total.Minutes.ToString("D2") + ":" + total.Seconds.ToString("D2")
                + "." + total.Milliseconds.ToString("D3") + " " + f1e.ToString()
                + " " + f2e.ToString() + " " + f3e.ToString() + " " + f4e.ToString();
        }

        public Fern2(float Xmin, float Ymin, float Xmax, float Ymax,
            float[] F1, float[] F2, float[] F3, float[] F4)
        {
            w = 800;
            h = 800;
            xmin = Xmin;
            ymin = Ymin;
            xmax = Xmax;
            ymax = Ymax;
            f1 = F1;
            f2 = F2;
            f3 = F3;
            f4 = F4;
            InitializeComponent();
            ClientSize = new Size(w, h);
        }
    }
}
