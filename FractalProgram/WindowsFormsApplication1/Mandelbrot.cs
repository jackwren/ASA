﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Drawing.Imaging;

namespace WindowsFormsApplication1
{
    public partial class Mandelbrot : Form
    {
        private const int MAX = 256;      // max iterations
        private const double SX = -2.025; // start value real
        private const double SY = -1.125; // start value imaginary
        private const double EX = 0.6;    // end value real
        private const double EY = 1.125;  // end value imaginary
        private static int x1, y1, xs, ys, xe, ye, red, green, blue;
        private static double xstart, ystart, xende, yende, xzoom, yzoom;
        private static bool action, rectangle, finished;
        private static float xy;

        private const double cRe = -0.7;
        private const double cIm = 0.27015;

        private Bitmap myBitmap;
        private Graphics g1;
        private Color col;

        private bool colourCycle = false; // Used to check how to draw the image
        private int timerSpeed = 2; // Speed of timer
        Timer ccTimer = new Timer();

        public Mandelbrot()
        {
            InitializeComponent();
            init();
            start();

            this.DoubleBuffered = true;
        }
        
        // RESTART FUNCTION
        private void restart()
        {
            init();
            start();
            mandelbrot();
        }

        //---------------------------------------------------------------------------------------------
        //making the bitmapstate to store zoomed values

        public class BitmapState
        {
            //REPLACE END WITH ZOOM
            public double xs;
            public double xe;
            public double ys;
            public double ye;
            public double xz;
            public double yz;
            Bitmap bitmap;
            // HAD TO MAKE THEM PUBLIC SO XML SERIALIZER CAN WRITE

            public void setValues(double xs, double xe, double ys, double ye, double xz, double yz)
            {
                this.xs = xs;
                this.ys = ys;
                this.xe = xe;
                this.ye = ye;
                this.yz = yz;
                this.xz = xz;
            }

            public double getXS()
            {
                return xs;
            }
            public double getYS()
            {
                return ys;
            }
            public double getXE()
            {
                return xe;
            }
            public double getYE()
            {
                return ye;
            }
            public double getXZ()
            {
                return xz;
            }
            public double getYZ()
            {
                return yz;
            }
            // Probably not
            public void setBitmap(Bitmap bm)
            {
                bitmap = bm;
            }
            public Bitmap getBitmap()
            {
                return bitmap;
            }
        }

        //----------makes the colour

        public struct HSBColor
        {
            float h;
            float s;
            float b;
            int a;
            public HSBColor(float h, float s, float b)
            {
                this.a = 0xff;
                this.h = Math.Min(Math.Max(h, 0), 255);
                this.s = Math.Min(Math.Max(s, 0), 255);
                this.b = Math.Min(Math.Max(b, 0), 255);
            }
          
            public static Color FromHSB(HSBColor hsbColor)
            {
                float r = hsbColor.b;
                float g = hsbColor.s;
                float b = hsbColor.h;
                if (hsbColor.s != 0)
                {
                    float max = hsbColor.b;
                    float dif = hsbColor.b * hsbColor.s / 255f;
                    float min = hsbColor.b - dif;
                    float h = hsbColor.h * 360f / 255f;
                    if (h < 60f)
                    {
                        r = max;
                        g = h * dif / 60f + min;
                        b = min;
                    }
                    else if (h < 120f)
                    {
                        r = -(h - 120f) * dif / 60f + min;
                        g = max;
                        b = min;
                    }
                    else if (h < 180f)
                    {
                        r = min;
                        g = max;
                        b = (h - 120f) * dif / 60f + min;
                    }
                    else if (h < 240f)
                    {
                        r = min;
                        g = -(h - 240f) * dif / 60f + min;
                        b = max;
                    }
                    else if (h < 300f)
                    {
                        r = (h - 240f) * dif / 60f + min;
                        g = min;
                        b = max;
                    }
                    else if (h <= 360f)
                    {
                        r = max;
                        g = min;
                        b = -(h - 360f) * dif / 60 + min;
                    }
                    else
                    {
                        r = 0;
                        g = 0;
                        b = 0;
                    }
                }
                return Color.FromArgb
                    (
                        hsbColor.a,
                        red = (int)Math.Round(Math.Min(Math.Max(r, 0), 255)),
                        green = (int)Math.Round(Math.Min(Math.Max(g, 0), 255)),
                        blue = (int)Math.Round(Math.Min(Math.Max(b, 0), 255))
                        );
            }
        }


        //-------------end of colour-------------------------------


        public void init() // all instances will be prepared
        {
            finished = false;

            x1 = this.Width;
            y1 = this.Height;
            xy = (float)x1 / (float)y1;

            myBitmap = new Bitmap(x1, y1);
            g1 = Graphics.FromImage(myBitmap);

            ccTimer.Interval = timerSpeed;
            ccTimer.Tick += ccTimer_Tick;
            
            finished = true;
        }

        
        public void start()
        {
            action = false;
            rectangle = false;
            initvalues();
            xzoom = (xende - xstart) / (double)x1;
            yzoom = (yende - ystart) / (double)y1;
            mandelbrot();
        }

        private void initvalues() // reset start values
        {
            xstart = SX;
            ystart = SY;
            xende = EX;
            yende = EY;
            if ((float)((xende - xstart) / (yende - ystart)) != xy)
                xstart = xende - (yende - ystart) * (double)xy;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g1 = e.Graphics;
            g1.DrawImage(myBitmap, 0, 0, x1, y1);  //draws to screen

            if (rectangle == true)
            {
                update(g1);
            }

        }

        public void update(Graphics g)
        {
            Pen Pen = new Pen(Color.White); // draws boxes for zooming etc....

            if (rectangle)
            {
                    
                if (xs < xe)
                {
                    if (ys < ye) g.DrawRectangle(Pen, xs, ys, (xe - xs), (ye - ys));
                    else g.DrawRectangle(Pen, xs, ye, (xe - xs), (ys - ye));
                }
                else
                {
                    if (ys < ye) g.DrawRectangle(Pen, xe, ys, (xs - xe), (ye - ys));
                    else g.DrawRectangle(Pen, xe, ye, (xs - xe), (ys - ye));
                }
                Pen.Dispose();
            }
        }

        private void mandelbrot() // calculate all points
        {
            Pen tempPen = null;
            int x, y;
            float h, b, alt = 0.0f;

            action = false;
            this.Cursor = Cursors.WaitCursor; //setCursor(c1);

            for (x = 0; x < x1; x += 1)
                for (y = 0; y < y1; y++)
                {
                    h = pointcolour(xstart + xzoom * (double)x, ystart + yzoom * (double)y); // color value
                    if (h != alt)
                    {
                        b = 1.0f - h * h; // brightness

                        col = HSBColor.FromHSB(new HSBColor(h * 255, 0.8f * 255, b * 255)); // draws the fractal

                        tempPen = new Pen(col);
                        alt = h;
                    }
                    g1.DrawLine(tempPen, x, y, x + 1, y);
                }

            this.Cursor = Cursors.Cross; //setCursor(c2);
            action = true;

        }

        private float pointcolour(double xwert, double ywert) // color value from 0.0 to 1.0 by iterations
        {
            double r = 0.0, i = 0.0, m = 0.0;
            int j = 0;

            while ((j < MAX) && (m < 4.0))
            {
                j++;
                m = r * r - i * i;
                i = 2.0 * r * i + ywert;
                r = m + xwert;
            }
            return (float)j / (float)MAX;
        }

        //------------------------------mouse actions------------------------

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (action)
            {
                xs = e.X;
                ys = e.Y;
                rectangle = true;

            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            int z, w;


            if (action)
            {
                xe = e.X;
                ye = e.Y;
                if (xs > xe)
                {
                    z = xs;
                    xs = xe;
                    xe = z;
                }
                if (ys > ye)
                {
                    z = ys;
                    ys = ye;
                    ye = z;
                }
                w = (xe - xs);
                z = (ye - ys);
                if ((w < 2) && (z < 2)) initvalues();
                else
                {
                    if (((float)w > (float)z * xy)) ye = (int)((float)ys + (float)w / xy);
                    else xe = (int)((float)xs + (float)z * xy);
                    xende = xstart + xzoom * (double)xe;
                    yende = ystart + yzoom * (double)ye;
                    xstart += xzoom * (double)xs;
                    ystart += yzoom * (double)ys;
                }
                xzoom = (xende - xstart) / (double)x1;
                yzoom = (yende - ystart) / (double)y1;
                mandelbrot();
                rectangle = false;

                Invalidate();


            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (action)
            {
                xe = e.X;
                ye = e.Y;

                if (rectangle == true)
                {
                    this.Refresh();
                }

            }


        }

        //------------------end of mouse----------------------------

        //------------------FILE ACTIONS ---------------------------

        private void exitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mandelbrot Fractal Program              \n\r\n\rTest Fractal               \n\r\n\r(C) 2016 Jack Wren");
        }

        //----------------ALL SAVE OPTIONS--------------------------

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save image here
            if (myBitmap != null)
            {
                SaveFileDialog dia = new SaveFileDialog();
                dia.Filter = "png file (*.png)|*.png";

                if (dia.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.myBitmap.Save(dia.FileName);
                        MessageBox.Show("Image Saved!");
                    }
                    catch { }
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save state here
            SaveFileDialog dia = new SaveFileDialog();
            dia.Filter = "xml file (*.xml)|*.xml";

            BitmapState bit = new BitmapState();
            bit.setValues(xstart, xende, ystart, yende, xzoom, yzoom);

            if (dia.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(BitmapState));
                    TextWriter tw = new StreamWriter(dia.FileName);
                    xs.Serialize(tw, bit);
                    tw.Close();
                    MessageBox.Show("State Saved!");

                }
                catch { MessageBox.Show("There was a problem saving file."); }

            }

        }

        private void loadStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // load fractal state here
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "xml file (*.xml)|*.xml";

            if (dia.ShowDialog() == DialogResult.OK)
            {
                BitmapState bit;
                XmlSerializer xs = new XmlSerializer(typeof(BitmapState));
                using (var sr = new StreamReader(dia.FileName))
                {
                    bit = (BitmapState)xs.Deserialize(sr);
                }

                if (bit != null)
                {
                    xstart = bit.getXS();
                    ystart = bit.getYS();
                    xende = bit.getXE();
                    yende = bit.getYE();
                    yzoom = bit.getYZ();
                    xzoom = bit.getXZ();
                    mandelbrot();
                    this.Refresh();
                }
            }
        }

        private void fernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FernMenu frmchild = new FernMenu();
            
            frmchild.Show();
        }





        //---COLOUR CYCLE STOP AND START BUTTONS ------------------------------


            

        public void ccTimer_Tick(object Sender, EventArgs e)
        {
            // Save the image as a GIF.
            try
            {
                myBitmap.Save("c:\\bmp.gif", ImageFormat.Gif);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error with saving the picture gif");
            }

            Image pb = Image.FromFile("c:\\bmp.gif");

            ColorPalette palette = pb.Palette;
            Color first = palette.Entries[0];
            for (int i = 0; i < (palette.Entries.Length - 1); i++)
            {
                palette.Entries[i] = palette.Entries[i + 1];
            }
            palette.Entries[(palette.Entries.Length - 1)] = first;
            pb.Palette = palette;

            myBitmap = new Bitmap(pb, x1, y1);

            this.DoubleBuffered = true;
            this.Refresh();

            pb.Dispose();
        }

       
        private void toolStripButton1_start_Click(object sender, EventArgs e)
        {
            ccTimer.Enabled = true;
            colourCycle = true;
            action = false;
        }

        private void toolStripButton2_stop_Click(object sender, EventArgs e)
        {
            ccTimer.Enabled = false;
            colourCycle = false;
            action = true;
            restart();
        }


    }
}
