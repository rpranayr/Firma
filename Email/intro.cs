using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Email
{
    public partial class intro : Form
    {
        public intro()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cvparser cp1 = new cvparser();
            cp1.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stockpredict sp1 = new stockpredict();
            sp1.Show();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            investorregistration ir1 = new investorregistration();
            ir1.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            login_page lp1 = new login_page();
            lp1.Show();
        }
    }
}
