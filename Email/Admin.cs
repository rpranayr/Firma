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
    public partial class Admin : Form
    {
        int empid;
        string name;
        string addr="";
        int salary=0;
        string bdate = "1990-1-1";
        int login_id = 0;
        string sex = "";
        int deptid = 0;
        int projid = 0;

        string connstr = @"server=localhost;userid=root;password=root;database=dbs";

        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                

                MySqlConnection conn = new MySqlConnection(connstr);
                conn.Open();


                string query = "select * from employee";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataAdapter adapt = new MySqlDataAdapter();
                adapt.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                dataGridView1.DataSource = dt;

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            { 
                if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
                {
                    MessageBox.Show("Enter Minimum Required Data(employee ID and employee Name) To store into the database ");
                    return;
                }

                empid = Int32.Parse(textBox1.Text);
                name = textBox2.Text;

                if(!textBox4.Text.Equals("") && !textBox3.Text.Equals("") && !dateTimePicker1.Value.ToString("yyyy-MM-dd").Equals("") && !textBox5.Text.Equals("") && !comboBox1.Text.Equals("")&& !textBox8.Text.Equals("")&& !textBox9.Text.Equals(""))
                {
                    addr = textBox4.Text;
                    salary = Int32.Parse(textBox3.Text);
                    bdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    login_id = Int32.Parse(textBox5.Text);
                    sex = comboBox1.Text;
                    deptid = Int32.Parse(textBox9.Text);
                    projid = Int32.Parse(textBox8.Text);

                }
                
                MySqlConnection conn = new MySqlConnection(connstr);
                conn.Open();

                string query = "insert into employee values('" + empid + "','" +name+"','"+addr+"','"+salary+"','"+bdate+"','"+login_id+"','"+sex+"','"+deptid+"','"+projid+"');";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader myread;
                myread = cmd.ExecuteReader();
                MessageBox.Show("Inserted Data");

                conn.Close();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selection = comboBox2.Text;
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            if (selection.Equals("Address"))
            {
                string query = "update employee set address= '"+textBox4.Text+"' where emp_id= '"+Int32.Parse(textBox1.Text)+"'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader upread = cmd.ExecuteReader();
                MessageBox.Show("Updated Address Successfully for Employee with ID: " + textBox1.Text);
                conn.Close();

            }
            
            if (selection.Equals("Login ID"))
            {
                string query = "update employee set login_id= '" + textBox5.Text + "' where emp_id= '" + Int32.Parse(textBox1.Text) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader upread = cmd.ExecuteReader();
                MessageBox.Show("Updated Login ID Successfully for Employee with ID: " + textBox1.Text);
                conn.Close();
            }
            if (selection.Equals("Department ID"))
            {
                string query = "update employee set dept_id= '" + textBox9.Text + "' where emp_id= '" + Int32.Parse(textBox1.Text) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader upread = cmd.ExecuteReader();
                MessageBox.Show("Updated Department ID Successfully for Employee with ID: " + textBox1.Text);
                conn.Close();
            }
            if (selection.Equals("Project ID"))
            {
                string query = "update employee set proj_id= '" + textBox8.Text + "' where emp_id= '" + Int32.Parse(textBox1.Text) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader upread = cmd.ExecuteReader();
                MessageBox.Show("Updated Project ID Successfully for Employee with ID: " + textBox1.Text);
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            // Add redirecttoform form1 = new redirecttoform() and then form1.show()
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "delete from employee where emp_id='"+Int32.Parse(textBox1.Text)+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader delread = cmd.ExecuteReader();
            MessageBox.Show("Deleted Employee Information who had an ID: "+textBox1.Text);
            conn.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            projectmanagement pm1 = new projectmanagement();
            pm1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            adminlogin al1 = new adminlogin();
            al1.Show();
        }
    }
}
