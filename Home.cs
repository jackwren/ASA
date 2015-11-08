using System;
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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fractal frmchild = new Fractal();
            frmchild.MdiParent = this;
            frmchild.Show();

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mandelbrot Fractal Program              \n\r\n\rLeeds Beckett University               \n\r\n\r(C) 2015 Jack Wren");
        }

        private void cacadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
