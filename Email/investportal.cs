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
    public partial class investportal : Form
    {
        public int proj_i;
        public int i_id=500;

        
        public investportal()
        {
           
            InitializeComponent();
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            string proj_available = "";
            int flag = 1;
            
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                //'" + (id+1) + "','" + sc.ToString() + "','" + sb.ToString() +"'); "
                string stm = "select proj_name from projects where proj_id in(select proj_id from investment where inv_id = '" + i_id + "');";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        flag = 0;
                        while (reader.Read())
                        {
                            proj_available = reader["proj_name"].ToString();
                        }
                    }
                    else
                    {
                        proj_available = "none";

                    }

                }
                textBox2.Text = proj_available;
                conn.Close();
               
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }


        }
        public investportal(int inv)
        {

            i_id = inv;
           
            InitializeComponent();
            //textBox1.Text = inv.ToString();
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            string proj_available = "";
            int flag = 1;

            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                //'" + (id+1) + "','" + sc.ToString() + "','" + sb.ToString() +"'); "
                string stm = "select proj_name from projects where proj_id in(select proj_id from investment where inv_id = '" + i_id + "');";
                Console.Write("Investor Id"+i_id);
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        flag = 0;
                        while (reader.Read())
                        {
                            proj_available = reader["proj_name"].ToString();
                        }
                    }
                    else
                    {
                        proj_available = "none";

                    }

                }
                textBox2.Text = proj_available;
                conn.Close();
               
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }


        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("get_investment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();
            conn = new MySqlConnection(cs);
            conn.Open();
            string stm = "select * from projects where proj_id in(select proj_id from investment where inv_id is null );";
            cmd = new MySqlCommand(stm, conn);
            MySqlDataAdapter adapt = new MySqlDataAdapter();
            adapt.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);

            dataGridView1.DataSource = dt;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["proj_id"]); 
                    }
                }
                else
                {
                    comboBox1.Items.Add("none");

                }

            }
            conn.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();
            
            string stm = "update investment set inv_id = '" + i_id + "'where proj_id =  '"+comboBox1.SelectedItem+"';";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Investment Successful.");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
