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
    public partial class projectmanagement : Form
    {
        string connstr = @"server=localhost;userid=root;password=root;database=dbs";

        public projectmanagement()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(checkedListBox1.Text.Equals("Yes"))
                {
                    int oclientid = Int32.Parse(textBox4.Text);
                    string odest = textBox6.Text;
                    int projid = Int32.Parse(textBox1.Text);

                    MySqlConnection conn = new MySqlConnection(connstr);
                    conn.Open();

                    string query = "insert into outsourced values ('" + oclientid + "','" + odest + "','"+projid+"');";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    MessageBox.Show("Project Successfully outsourced");

                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Project Not Yet Ready To Be Outsourced");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Enter Valid Data");
            }

        
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int oclientid = Int32.Parse(textBox4.Text);
            string oname = "";
            

            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            string query = "select name from client where client_id='"+oclientid+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                while(reader.Read())
                {
                    oname = reader.GetString(0);
                }
            }

            textBox7.Text = oname;
            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();

            int projid = Int32.Parse(textBox1.Text);
            string projname = "";
            int projdur = 0;

            string query = "select proj_name,proj_duration from projects where proj_id='" + projid + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    projname = reader.GetString(0);
                    projdur = reader.GetInt32(1);
                }
            }

            textBox2.Text = projname;
            textBox3.Text = projdur.ToString();

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
