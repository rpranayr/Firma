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
    public partial class adminlogin : Form
    {
        string connstr = @"server=localhost;userid=root;password=root;database=dbs";

        public adminlogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();


            string query = "select * from login";
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

            try
            {
                int logid = Int32.Parse(textBox1.Text);
                string usrname = textBox2.Text;
                string pwd = textBox3.Text;

                if (logid == null || usrname.Equals("") || pwd.Equals(""))
                {
                    MessageBox.Show("Enter All Valid Credentials");
                    return;
                }

                MySqlConnection conn = new MySqlConnection(connstr);
                conn.Open();

                string query = "insert into login values ('" + logid + "','" + usrname + "','" + pwd + "');";
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

        private void button5_Click(object sender, EventArgs e)
        {
            string selection = comboBox1.Text;
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            if (selection.Equals("Username"))
            {
                string query = "update login set username='"+textBox2.Text+"' where login_id='"+Int32.Parse(textBox1.Text)+"'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader upread = cmd.ExecuteReader();
                MessageBox.Show("Updated Username Successfully whose login ID is: " + textBox1.Text);
                conn.Close();

            }
            if (selection.Equals("Password"))
            {
                string query = "update login set password='" + textBox3.Text + "' where login_id='" + Int32.Parse(textBox1.Text) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader upread = cmd.ExecuteReader();
                MessageBox.Show("Updated Password Successfully whose login ID is: " + textBox1.Text);
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "delete from login where login_id='" + Int32.Parse(textBox1.Text) + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader delread = cmd.ExecuteReader();
            MessageBox.Show("Deleted Login Information who had an ID: " + textBox1.Text);
            conn.Close();
        }
    }
}
