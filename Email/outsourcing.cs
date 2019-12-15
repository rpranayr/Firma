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
    public partial class outsourcing : Form
    {
        string connstr = @"server=localhost;userid=root;password=root;database=dbs";
        int id;


        public outsourcing(int clientid)
        {
            InitializeComponent();
            id = clientid;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void outsourcing_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "select client_id from client where client_id ='"+id+"';";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                while(reader.Read())
                {
                    textBox1.Text =reader.GetString(0);
                }
            }
            conn.Close();
        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "select name from client where client_id ='" + id + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    textBox2.Text = reader.GetString(0);
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "select P.proj_id, P.proj_name, P.proj_duration from outsourced O join projects P on O.proj_id = P.proj_id join client C on O.client_id = C.client_id where C.client_id = '" + id + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataAdapter adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }
    }
}
