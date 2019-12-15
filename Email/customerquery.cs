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
    public partial class customerquery : Form
    {
        string query = "";
        int empid = 0;
        string empname = "";
        int id = 0;

        public customerquery(int emp_id)
        {
            InitializeComponent();
            id = emp_id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string query = richTextBox1.Text;
            
            string connstr = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string dbquery = "insert into customerservice values('"+empid+"','"+query+"');";
            MySqlCommand cmd = new MySqlCommand(dbquery, conn);

            MySqlDataReader myread;
            myread = cmd.ExecuteReader();
            MessageBox.Show("Your Query Has Been Registered.");
            conn.Close();
        }

        private void customerquery_Load(object sender, EventArgs e)
        {
            string connstr = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string dbquery = "select emp_id,name from employee where emp_id = '" + id + "';";
            MySqlCommand cmd = new MySqlCommand(dbquery, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    //test = (int)reader["emp_id"];
                    empid = reader.GetInt32(0);
                    empname = reader.GetString(1);
                }
            }

            textBox1.Text = empid.ToString();
            textBox2.Text = empname;

                conn.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
