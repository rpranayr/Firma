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
using System.Collections;

namespace Email
{
    public partial class empsalary : Form
    {
        public float ans = 0,salary_new;
        public ArrayList myList = new ArrayList();
        int id;

        public empsalary(int e_id)
        {
            InitializeComponent();
            id = e_id;

        }

        private void Gener̥ateIndex_Click(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("perfomance_gen", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", MySqlDbType.Int32).Value = textBox1.Text;
            cmd.ExecuteNonQuery();
            int sum = 0;
            ans = 0;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // access your record colums by using reader
                    sum  =sum + Int32.Parse(reader["dis"].ToString());
                    sum = sum + Int32.Parse(reader["target"].ToString());
                    sum = sum + Int32.Parse(reader["sincere"].ToString());
                    ans = (float)sum / 15;
                    
                    // richTextBox1.AppendText(reader["Proj_id"].ToString() + "\t");

                    //Console.WriteLine(reader["price"]);
                    //MessageBox.Show(reader["price"].ToString());
                }
            }
            conn.Close();
            textBox2.Text = ans.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                string stm = "select salary from employee where emp_id = "+ textBox1.Text;
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                //string version = Convert.ToString(cmd.ExecuteScalar());
                //richTextBox1.Text = version;
                int salary = 0;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       salary = Int32.Parse(reader["salary"].ToString());
                    }
                }

                richTextBox1.Text = salary.ToString();
                salary_new = (float)((100 + (ans * 10) * salary )/ 100)+salary;
                richTextBox2.Text = salary_new.ToString();



            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void empsalary_Load(object sender, EventArgs e)
        {
            textBox1.Text = id.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                string cs = @"server=localhost;userid=root;password=root;database=dbs";
                MySqlConnection conn = null;
                conn = new MySqlConnection(cs);
                conn.Open();
                string query = "UPDATE Employee SET salary=" + salary_new + " WHERE emp_id =" + textBox1.Text;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } }
