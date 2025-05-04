using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class AnketaForm : Form
    {
        public AnketaForm()
        {
            InitializeComponent();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        public void button2_Click(object sender, EventArgs e)
        {
            panelFilms.Visible = false;
            panelSerials.Visible = true;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            panelFilms.Visible = true;
            panelSerials.Visible = false;
            button2.Enabled = true;
            button1.Enabled = false;
        }
    }
}
