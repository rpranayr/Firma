using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace Email

{
  
       
    public partial class investorregistration : Form
    {
       
        public static int counter = 0;
        public investorregistration()
        {
            InitializeComponent();
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MySqlConnection msc = new MySqlConnection(@"server=localhost;userid=root;password=root;database=dbs");
            msc.Open();
            Boolean n=true; //name is not empty
            Boolean c1=true; //contact number is only numeric
            Boolean c2=true; //contact number is 10 digits
            Boolean l=true; //login_id is not empty
            Boolean e1=true; //email is in valid format
            Boolean e2 = true; //email cannot be empty
            Boolean p=true; //paswords match
            Boolean g = true;  // checks that atleast one gender is selected
            Boolean f1=true; //funding cannot be empty
            Boolean f2=true; //funding can be only numeric
            Boolean a=true; // address cannot be empty
            Boolean m=false; //to check if male is selected
            Boolean f=false; //to check if female is selected
            Boolean l1 = true; //to check login_id is int
            String s1; //for name
            String s2; //for contact number
            long Cont;
            long id1;
            String s3; //for login_id
            String s4; //for email
            String s5; //for password
            String s6; //for confirm password
            int funding=0; //for funding 
            String s7; //for address
            String s8; //for funding
            String regex1 = "^[\\w-_\\.+]*[\\w-_\\.]\\@([\\w]+\\.)+[\\w]+[\\w]$";//for email verification
            s1 = rname.Text;
            s2 = rcontact.Text;
            s3 = rlogin.Text;
            s4 = remail.Text;
            s5 = rpassword.Text;
            s6 = rcpass.Text;
            s8 = rfund.Text;
            s7 = raddress.Text;
            if (s1.Length == 0)
            {
                n = false;
            }
            if(s2.Length!=10)
            {
                c2 = false;
            }
            try
            {
                Cont = long.Parse(s2);
            }
            catch(FormatException)
            {
                c1 = false;
            }
            catch(OverflowException)
            {
                c1 = false;
            }
            if (s3.Length == 0)
            {
                l = false;
            }
            else
            {
                try
                {
                    id1 = long.Parse(s3);
                }
                catch (FormatException)
                {
                    l1 = false;

                }
            }
            if (!(s5.Equals(s6)))
            {
                p = false;
            }
            if (s8.Length == 0)
            {
                f1 = false;
            }
            else
            {
                try {
                    funding = int.Parse(rfund.Text);
                }
                catch(FormatException)
                {
                    f2 = false;
                }

            }
            if(!(radioButton1.Checked||radioButton2.Checked))
            {
                g = false;
            }
            if (radioButton1.Checked)
            {
                m = true;
            }
            else if (radioButton2.Checked)
            {
                f = true;
            }
            if(s7.Length==0)
            {
                a = false;
            }
            if(s4.Length==0)
            {
                e2 = false;
            }
            if(!(Regex.IsMatch(s4,regex1)))
            {
                e1 = false;
            }
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            if (l && l1 && c1 && c2 && n && e1 && e2 && p && g && f1 && f2 && a && (m || f))
            {
                MessageBox.Show("Registration Successfull", "Welcome to Firma", buttons, MessageBoxIcon.Information);

                String query = "insert into investors(name,funding) values('" + s1 + "','" + funding + "')";
                String query1="insert into login values('"+s3+"','"+s1+"','"+s5+"')";
                MySqlCommand cmd = new MySqlCommand(query, msc);
                MySqlCommand cmd1 = new MySqlCommand(query1, msc);
                MySqlDataReader myreader2;
                myreader2 = cmd.ExecuteReader();
                myreader2.Close();
                MySqlDataReader myreader1;
                myreader1 = cmd1.ExecuteReader();
                
                
                msc.Close();

            }
            else if (!l && l1 && c1 && c2 && n && e1 && e2 && p && g && f1 && f2 && a && (m || f))
            {
                MessageBox.Show("login id cannot be empty", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && l1&& c2 && !n && e1 && e2 && p && g && f1 && f2 && a && (m || f))
            {
                MessageBox.Show("name cannot be empty", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && !c1 && l1 && !c2 && n && e1 && e2 && p && g && f1 && f2 && a && (m || f))
            {
                MessageBox.Show("contact number can only be numeric", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && !c2 && l1 && n && e1 && e2 && p && g && f1 && f2 && a)
            {
                MessageBox.Show("contact number should be of 10 digits", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && e1 && l1 && !e2 && p && g && f1 && f2 && a)
            {
                MessageBox.Show("Email cannot be empty", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && !e1 && l1 && e2 && p && g && f1 && f2 && a)
            {
                MessageBox.Show("Email is in ivalid format", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && l1 && n && e1 && e2 && !p && g && f1 && f2 && a)
            {
                MessageBox.Show("Passwords don't match", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && l1 && e1 && e2 && p && !g && f1 && f2 && a)
            {
                MessageBox.Show("Select a gender", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && e1 && l1 && e2 && p && g && !f1 && f2 && a)
            {
                MessageBox.Show("funding cannot be empty", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && e1 && e2 && l1 && p && g && f1 && !f2 && a)
            {
                MessageBox.Show("funding can be only numeric", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && e1 && e2 && p && l1 && g && f1 && f2 && !a)
            {
                MessageBox.Show("address cannot be empty", "Error", buttons, MessageBoxIcon.Error);
            }
            else if (l && c1 && c2 && n && e1 && e2 && p && !l1 && g && f1 && f2 && a)
            {
                MessageBox.Show("Login id must be numeric", "Error", buttons, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Multiple fields in unacceptable format", "Error", buttons, MessageBoxIcon.Error);
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
