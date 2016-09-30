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
    public partial class Fern1 : Form
    {
       double a, b, c, d, e, f, x0, x1, y0, y1;
        Random random = new Random((int)DateTime.Now.Ticks);

        void calculateMatrixAndVector()
        {
            double r = random.NextDouble();
            double[] p = new double[4];

            p[0] = 0.01;
            p[1] = 0.86;
            p[2] = 0.93;
            p[3] = 1.00;
            if (r <= p[0])
            {
                a = b = c = 0.0;
                d = 0.16;
                e = f = 0.0;
            }
            else if (r > p[0] && r <= p[1])
            {
                a = d = 0.85;
                b = +0.04;
                c = -0.04;
                e = 0.0;
                f = 1.6;
            }
            else if (r > p[1] && r <= p[2])
            {
                a = +0.20;
                b = -0.26;
                c = +0.23;
                d = +0.22;
                e = 0.0;
                f = 1.6;
            }
            else if (r > p[2] && r <= p[3])
            {
                a = -0.15;
                b = +0.28;
                c = +0.26;
                d = +0.24;
                e = 0.0;
                f = 0.44;
            }
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;
            int chi1 = 0, eta1 = 0;
            int[,] image = new int[width, height];
            double xmax = +3.0, ymax = 10.50;
            double xmin = -3.0, ymin = 0.0;
            double chiSlope = width / (xmax - xmin);
            double etaSlope = -height / (ymax - ymin);
            double chiInter = width - chiSlope * xmax;
            double etaInter = height - etaSlope * ymin;
            Graphics g = pea.Graphics;
            Pen pen = new Pen(Color.Green);

            x0 = xmin;
            y0 = ymin;
            for (int K = 0; K < 1000000; K++)
            {
                for (int k = 0; k < 4; k++)
                {
                    calculateMatrixAndVector();
                    x1 = a * x0 + b * y0 + e;
                    y1 = c * x0 + d * y0 + f;
                    chi1 = (int)(chiSlope * x1 + chiInter);
                    eta1 = (int)(etaSlope * y1 + etaInter);
                    x0 = x1;
                    y0 = y1;
                }
                image[chi1, eta1]++;
            }
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (image[i, j] != 0)
                        g.DrawEllipse(pen, i, j, 1.0f, 1.0f);
        }
        
        public Fern1()
        {
            InitializeComponent();
            ClientSize = new Size(800, 800);
        }
    }
}
