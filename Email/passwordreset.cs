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
    public partial class passwordreset : Form
    {
        public string u_name = "";
        public passwordreset()
        {
            InitializeComponent();
        }
        public passwordreset(string u)
        {
            u_name = u;
            InitializeComponent();
            textBox1.Text = u;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string pwd = textBox2.Text;
            string r_pwd = textBox3.Text;
            if(pwd.Contains(r_pwd))
            {
                string cs = @"server=localhost;userid=root;password=root;database=dbs";
                MySqlConnection conn = null;
                conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "update login set password = '" + r_pwd + "'where username =  '" + textBox1.Text + "';";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Password Reset");
                
            }
            else
            {
                MessageBox.Show("Passwords Don't Match");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
