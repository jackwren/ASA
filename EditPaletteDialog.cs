using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class EditPaletteDialog : Form
    {
        public int red, green, blue;
        private System.Drawing.Color selectedcolor;

        public EditPaletteDialog()
        {
            InitializeComponent();
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Fractal frm = new Fractal();

            if (selectedcolor != null)
            {
                Fractal.HSBColor.FromHSB(new Fractal.HSBColor(red, green, blue));
                this.Close();
                //MessageBox.Show(Convert.ToString(red));
            }
            else
            {
                //ignore
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBox1.BackColor = colorDialog1.Color;
            selectedcolor = colorDialog1.Color;
            //colorDialog1.Color = Color.FromArgb(red);
            red = colorDialog1.Color.ToArgb();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBox2.BackColor = colorDialog1.Color;
            green = colorDialog1.Color.ToArgb(); 
            //colorDialog1.Color = Color.FromArgb(green);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBox3.BackColor = colorDialog1.Color;
            blue = colorDialog1.Color.ToArgb();
            //colorDialog1.Color = Color.FromArgb(blue);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
        }

      
    }
}
