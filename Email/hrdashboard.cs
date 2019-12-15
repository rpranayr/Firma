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
    public partial class hrdashboard : Form
    {
        string connstr = @"server=localhost;userid=root;password=root;database=dbs";
        int hrid;
        string hrname;

        int loginid;
        

        public hrdashboard(int login_id)
        {
            InitializeComponent();
            loginid = login_id;
        }

        private void hrdashboard_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "select emp_id,name from employee where login_id = '" + loginid + "';";
            MySqlCommand cmd_1 = new MySqlCommand(query, conn);
            MySqlDataReader reader_1 = cmd_1.ExecuteReader();

            using (reader_1)
            {
                while (reader_1.Read())
                {
                    hrid = reader_1.GetInt32(0);
                    hrname = reader_1.GetString(1);
                }
            }

            textBox1.Text = hrid.ToString();
            textBox2.Text = hrname;

            comboBox1.Items.Clear();

            query = "select name from employee where emp_id in (select emp_id from recruits where hr_id= '"+hrid+"');";
            MySqlCommand cmd_2 = new MySqlCommand(query, conn);
            MySqlDataReader reader_2 = cmd_2.ExecuteReader();

            using (reader_2)
            {
                while(reader_2.Read())
                {
                    comboBox1.Items.Add(reader_2.GetString(0));
                }
            }


                conn.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '"+comboBox1.Text+"';";
            MySqlCommand cmd = new MySqlCommand(query,conn);

            MySqlDataAdapter adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            dataGridView1.DataSource = dt;
            
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            String query = "select HR.emp_id , E.name , HR.holidays_requested , HR.reason , HR.status from holidayrequest HR join employee E on HR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataAdapter adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int empid = Int32.Parse(textBox3.Text);
            string rea = textBox4.Text;

            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string approved = "ap";

            string query = "update salaryrequest set status='"+approved+"' where emp_id= '"+empid+"' and reason='"+rea+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader read = cmd.ExecuteReader();
            MessageBox.Show("Request was Approved!");
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int empid = Int32.Parse(textBox3.Text);
            string rea = textBox4.Text;

            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string approved = "ap";

            string query = "update holidayrequest set status='" + approved + "' where emp_id= '" + empid + "' and reason='" + rea + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader read = cmd.ExecuteReader();
            MessageBox.Show("Request was Approved!");
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
