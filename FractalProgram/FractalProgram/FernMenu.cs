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
    public partial class FernMenu : Form
    {
        public FernMenu()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            button1.Click += new EventHandler(OnClick);
        }

        void OnClick(Object obj, EventArgs ea)
        {
            float xMin = (float)Convert.ToDouble(textBox1.Text);
            float yMin = (float)Convert.ToDouble(textBox2.Text);
            float xMax = (float)Convert.ToDouble(textBox3.Text);
            float yMax = (float)Convert.ToDouble(textBox4.Text);
            float[] f1 = new float[7];
            float[] f2 = new float[7];
            float[] f3 = new float[7];
            float[] f4 = new float[7];

            if (comboBox1.SelectedIndex == 0)
            {
                // Spleenwort fern
                //w 	a 	b 	c 	d 	e 	f 	p 	Portion generated
                //ƒ1 	0 	0 	0 	0.16 	0 	0 	0.01 	Stem
                //ƒ2 	0.85 	0.04 	−0.04 	0.85 	0 	1.60 	0.85 	Successively smaller leaflets
                //ƒ3 	0.20 	−0.26 	0.23 	0.22 	0 	1.60 	0.07 	Largest left-hand leaflet
                //ƒ4 	−0.15 	0.28 	0.26 	0.24 	0 	0.44 	0.07 	Largest right-hand leaflet
                f1[0] = f1[1] = f1[2] = f1[4] = f1[5] = 0.0f;
                f1[3] = 0.16f;
                f1[6] = 0.01f;
                f2[0] = 0.85f;
                f2[1] = 0.04f;
                f2[2] = -0.04f;
                f2[3] = 0.85f;
                f2[4] = 0.0f;
                f2[5] = 1.6f;
                f2[6] = 0.85f;
                f3[0] = 0.2f;
                f3[1] = -0.26f;
                f3[2] = 0.23f;
                f3[3] = 0.22f;
                f3[4] = 0.0f;
                f3[5] = 1.6f;
                f3[6] = 0.07f;
                f4[0] = -0.15f;
                f4[1] = 0.28f;
                f4[2] = 0.26f;
                f4[3] = 0.24f;
                f4[4] = 0.0f;
                f4[5] = 0.44f;
                f4[6] = 0.07f;

                (new Fern1()).Show();
            }
            else
            {
                // Cyclosorus fern
                //w 	a 	b 	c 	d 	e 	f 	p
                //ƒ1 	0 	0 	0 	0.25 	0 	−0.4 	0.02
                //ƒ2 	0.95 	0.005 	−0.005 	0.93 	−0.002 	0.5 	0.84
                //ƒ3 	0.035 	−0.2 	0.16 	0.04 	−0.09 	0.02 	0.07
                //ƒ4 	−0.04 	0.2 	0.16 	0.04 	0.083 	0.12 	0.07
                f1[0] = f1[1] = f1[2] = f1[4] = 0.0f;
                f1[3] = 0.25f;
                f1[5] = -0.4f;
                f1[6] = 0.02f;
                f2[0] = 0.95f;
                f2[1] = 0.005f;
                f2[2] = -0.005f;
                f2[3] = 0.93f;
                f2[4] = -0.002f;
                f2[5] = 0.5f;
                f2[6] = 0.84f;
                f3[0] = 0.035f;
                f3[1] = -0.2f;
                f3[2] = 0.16f;
                f3[3] = 0.04f;
                f3[4] = -0.09f;
                f3[5] = 0.02f;
                f3[6] = 0.07f;
                f4[0] = -0.04f;
                f4[1] = 0.2f;
                f4[2] = 0.16f;
                f4[3] = 0.04f;
                f4[4] = 0.083f;
                f4[5] = 0.12f;
                f4[6] = 0.07f;

                Fern2 form = new Fern2(xMin, yMin, xMax, yMax, f1, f2, f3, f4);

                form.Show();
            }
        }

    }
}


