using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Email

{
    
    public partial class userdashboard : Form
    {
        string dep = "",pr="";
        string cs = @"server=localhost;userid=root;password=root;database=dbs";

        int empid;

        //MySqlConnection conn = null;
        public userdashboard()
        {
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            InitializeComponent();
            MySqlConnection conn = new MySqlConnection(cs);
            conn.Open();

            //String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";
            int id = 2;
            String query = "select * from employee where emp_id = '" + id + "'; ";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    label1.Text = reader["name"].ToString();
                    label2.Text = reader["emp_id"].ToString();
                    label3.Text = reader["salary"].ToString();
                    label8.Text = reader["login_id"].ToString();
                    dep = reader["dept_id"].ToString();
                    pr = reader["proj_id"].ToString();



                }
            }
            conn.Close();
            conn.Open();

            //String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";

            query = "select dept_name from department where dept_id = '" + dep + "'; ";
            cmd = new MySqlCommand(query, conn);


            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    label7.Text = reader["dept_name"].ToString();
                }
            }
            conn.Close();
            conn.Open();

            //String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";

            query = "select name,address,b_date from employee where emp_id in (select hr_id from recruits where emp_id = '" + Int32.Parse(label2.Text) + "')";
            cmd = new MySqlCommand(query, conn);

            MySqlDataAdapter adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            dataGridView1.DataSource = dt;
            conn.Close();
            conn.Open();
            query = "select * from projects where proj_id = '" + pr + "'; ";
            cmd = new MySqlCommand(query, conn);
            adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            dt = new DataTable();
            adapt.Fill(dt);

            dataGridView2.DataSource = dt;
            conn.Close();
        }
        public userdashboard(int emp)
        {
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            InitializeComponent();
            MySqlConnection conn = new MySqlConnection(cs);
            conn.Open();
            empid = emp;
            //String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";

            String query = "select * from employee where emp_id = '" + emp + "'; ";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    label1.Text = reader["name"].ToString();
                    label2.Text = reader["emp_id"].ToString();
                    label3.Text = reader["salary"].ToString();
                    label8.Text = reader["login_id"].ToString();
                    dep = reader["dept_id"].ToString();
                    pr = reader["proj_id"].ToString();



                }
            }
            conn.Close();
            conn.Open();

            //String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";

            query = "select dept_name from department where dept_id = '" + dep + "'; ";
            cmd = new MySqlCommand(query, conn);


            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    label7.Text = reader["dept_name"].ToString();
                }
            }
            conn.Close();
            conn.Open();

            //String query = "select SR.emp_id , E.name , SR.amount , SR.reason , SR.status from salaryrequest SR join employee E on SR.emp_id = E.emp_id where E.name= '" + comboBox1.Text + "';";

            query = "select name,address,b_date from employee where emp_id in (select hr_id from recruits where emp_id = '" + Int32.Parse(label2.Text) + "')";
            cmd = new MySqlCommand(query, conn);

            MySqlDataAdapter adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            dataGridView1.DataSource = dt;
            conn.Close();
            conn.Open();
            query = "select * from projects where proj_id = '" + pr + "'; ";
            cmd = new MySqlCommand(query, conn);
            adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            dt = new DataTable();
            adapt.Fill(dt);

            dataGridView2.DataSource = dt;
            conn.Close();
        }


        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            empsalary es1 = new empsalary(empid);
            es1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            holidayrequest hr1 = new holidayrequest(empid);
            hr1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            salaryrequest sr1 = new salaryrequest(empid);
            sr1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            customerquery cq1 = new customerquery(empid);
            cq1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            email em1 = new email();
            em1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
