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
    public partial class salaryrequest : Form
    {
        int empid;
        int amt;
        string reason = "";
        string status;

        int id = 0;
        string empname = "";

        public salaryrequest(int empid)
        {
            InitializeComponent();
            id = empid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            empid = Int32.Parse(textBox1.Text);
            amt = Int32.Parse(textBox2.Text);
            reason = richTextBox1.Text;
            status = "na";

            string connstr = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "insert into salaryrequest values ('"+empid+"','"+amt+"','"+reason+"','"+status+"');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader myread = cmd.ExecuteReader();
            MessageBox.Show("Your request has been submitted");

            conn.Close();
        }

        private void salaryrequest_Load(object sender, EventArgs e)
        {
            string connstr = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string dbquery = "select emp_id,name from employee where emp_id = '" + id + "';";
            MySqlCommand cmd = new MySqlCommand(dbquery, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //test = (int)reader["emp_id"];
                    empid = reader.GetInt32(0);
                    empname = reader.GetString(1);
                }
            }

            textBox1.Text = empid.ToString();
            textBox3.Text = empname;

            conn.Close();
        }
    }
}
