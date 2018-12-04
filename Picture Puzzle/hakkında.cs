using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_Puzzle
{
    public partial class hakkında : Form
    {
        public hakkında()
        {
            InitializeComponent();
        }

        private void BTN_ÇIKIŞ_Click(object sender, EventArgs e)
        {
            Form1 h = new Form1();
            this.Hide();
        }
    }
}
