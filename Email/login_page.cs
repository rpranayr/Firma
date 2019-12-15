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
    public partial class login_page : Form
    {
        public string password, comm;
        int flag = 0,inv_id;
        public login_page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";

            MySqlConnection conn = null;
            string selected = comboBox1.SelectedItem.ToString();
            string u_name = textBox1.Text;
            string pwd = textBox2.Text;
            if (selected == "HR")
            {
                comm = "select password from employee natural join login where emp_id in (select  hr_id from recruits)  and username ='" + u_name + "';";
            }
            else if (selected == "Manager")
            {
                comm = "select password from employee natural join login where emp_id in (select man_id from manages) and username ='" + u_name + "';";
            }
            else if (selected == "Investor")
            {
                comm = "select password from login where username  = '" + u_name + "';";
            }
            else if (selected == "Employee")
            {
                comm = "select password from login where username in (select username from employee natural join login where username = '" + u_name + "');";
            }
            else if (selected == "Outsourcing")
            {
                comm = "select password from login where username  = '" + u_name + "';";

            }
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(comm, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    
                        
                        flag = 1;

                    
                }
                else
                {
                    //MessageBox.Show("You don't exist here");
                    flag = 0;
                }

            }
            if(flag==1)

            {
                passwordreset f8 = new passwordreset(u_name);
                f8.Show();

            }
            else
            {
                MessageBox.Show("Not a valid Username");
            }
            
            
            
                
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";

            MySqlConnection conn = null;
            string selected = comboBox1.SelectedItem.ToString();
            string u_name = textBox1.Text;
            string pwd = textBox2.Text;


            if (selected == "HR")
            {
                comm = "select password from employee natural join login where emp_id in (select  hr_id from recruits)  and username ='" + u_name + "';";
            }
            else if (selected == "Manager")
            {
                comm = "select password from employee natural join login where emp_id in (select man_id from manages) and username ='" + u_name + "';";
            }
            else if (selected == "Investor")
            {
                comm = "select password from login where username  = '" + u_name + "';";
            }
            else if (selected == "Employee")
            {
                comm = "select password from login where username in (select username from employee natural join login where username = '" + u_name + "');";
            }
            else if (selected == "Outsourcing")
            {
                comm = "select password from login where username  = '" + u_name + "';";
                
            }
            else if (selected == "Admin")
            {
                if (u_name.Contains("admin") && pwd.Contains("admin"))
                {
                    Admin ad1 = new Admin();
                    ad1.Show();
                    return;
                }

            }


            try
            {

                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(comm, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            //password = reader["password"].ToString();
                            password = reader.GetString(0);
                            flag = 1;

                        }
                    }
                    else
                    {
                        MessageBox.Show("You don't exist here");
                        flag = 0;
                    }

                }
                conn.Close();
                if (password.Equals(pwd))
                {
                    
                    if (selected == "HR")
                    {
                        comm = "select emp_id from employee natural join login where emp_id in (select hr_id from recruits) and username = '"+u_name+"'";
                        conn = new MySqlConnection(cs);
                        conn.Open();

                        cmd = new MySqlCommand(comm, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    inv_id = Int32.Parse(reader["emp_id"].ToString());


                                }
                            }


                        }
                        conn.Close();
                        hrdashboard hd1 = new hrdashboard(inv_id);
                        hd1.Show();



                    }
                    else if (selected == "Manager")
                    {
                        comm = "select emp_id from employee natural join login where emp_id in (select man_id from manages) and username = '" + u_name + "';";
                        conn = new MySqlConnection(cs);
                        conn.Open();
                        cmd = new MySqlCommand(comm, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    inv_id = Int32.Parse(reader["emp_id"].ToString());


                                }
                            }


                        }
                        conn.Close();
                        navwindow f9 = new navwindow(inv_id);
                        f9.Show();

                    }
                    else if (selected == "Investor")
                    {

                        comm = "select inv_id from investors where name='" + u_name + "';";
                        conn = new MySqlConnection(cs);
                        conn.Open();
                        cmd = new MySqlCommand(comm, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    inv_id = Int32.Parse(reader["inv_id"].ToString());


                                }
                            }


                        }
                        conn.Close();
                        investportal f7 = new investportal(inv_id);
                        f7.Show();
                    }
                    else if (selected == "Employee")
                    {
                        comm = "select emp_id from employee natural join login where username = '" + u_name + "';";
                        conn = new MySqlConnection(cs);
                        conn.Open();
                        cmd = new MySqlCommand(comm, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    inv_id = Int32.Parse(reader["emp_id"].ToString());


                                }
                            }


                        }
                        conn.Close();
                        userdashboard f6 = new userdashboard(inv_id);
                        f6.Show();

                    }
                    else if (selected == "Outsourcing")
                    {
                        comm = "select client_id from client where name = '" + u_name + "';";
                        conn = new MySqlConnection(cs);
                        conn.Open();
                        cmd = new MySqlCommand(comm, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    inv_id = Int32.Parse(reader["client_id"].ToString());


                                }
                            }


                        }
                        conn.Close();


                        outsourcing os1 = new outsourcing(inv_id);
                        os1.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }

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

    }
}
