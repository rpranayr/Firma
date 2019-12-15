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
    public partial class emprating : Form
    {
        public emprating()
        {
            InitializeComponent();
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            try
            {
                int id = 6;
                conn = new MySqlConnection(cs);
                conn.Open();
                string stm = "select emp_id from manages where man_id = "+id;
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                //string version = Convert.ToString(cmd.ExecuteScalar());
                //richTextBox1.Text = version;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // access your record colums by using reader
                        comboBox1.Items.Add(reader["emp_id"]);;
                        //Console.WriteLine(reader["price"]);
                        //MessageBox.Show(reader["price"].ToString());
                    }
                }
                conn.Close();
                label4.Text = id.ToString();


            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            
        }
        public emprating(int i)
        {
            InitializeComponent();
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            try
            {

                int id = i;
                label4.Text = id.ToString();
                conn = new MySqlConnection(cs);
                conn.Open();
                string stm = "select emp_id from manages where man_id = " + id;
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                //string version = Convert.ToString(cmd.ExecuteScalar());
                //richTextBox1.Text = version;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // access your record colums by using reader
                        comboBox1.Items.Add(reader["emp_id"]); ;
                        //Console.WriteLine(reader["price"]);
                        //MessageBox.Show(reader["price"].ToString());
                    }
                }
                conn.Close();


            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
        

        private void Button3_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = "";
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Customers_GetCustomer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", MySqlDbType.Int32).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            richTextBox1.AppendText("Name"+"\t"+"Address"+"\t"+"Salary");
            richTextBox1.AppendText("\n");

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // access your record colums by using reader
                   
                    richTextBox1.AppendText(reader["name"].ToString()+"\t");
                    richTextBox1.AppendText(reader["address"].ToString()+"\t");
                    richTextBox1.AppendText(reader["salary"].ToString() + "\t");
                   // richTextBox1.AppendText(reader["Proj_id"].ToString() + "\t");

                    //Console.WriteLine(reader["price"]);
                    //MessageBox.Show(reader["price"].ToString());
                }
            }
            conn.Close();
            }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();
            string cmdText = "INSERT INTO perfomance_rating VALUES (@emp_id, @dis,@target,@sincere)";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@emp_id", MySqlDbType.Int32).Value = comboBox1.SelectedItem;
            cmd.Parameters.AddWithValue("@dis", MySqlDbType.Int32).Value = textBox1.Text;
            cmd.Parameters.AddWithValue("@target", MySqlDbType.Int32).Value = textBox2.Text;
            cmd.Parameters.AddWithValue("@sincere", MySqlDbType.Int32).Value = textBox3.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Inserted");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
