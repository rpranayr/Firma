using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Email

{
    public partial class navwindow : Form
    {
        public int id;
        public navwindow()
        {
            InitializeComponent();
        }
        public navwindow(int i)
        {
            id = i;
            InitializeComponent();
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            emprating er1 = new emprating(id);
            er1.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            userdashboard ud1 = new userdashboard(id);
            ud1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
