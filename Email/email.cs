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

using System.Web;
using System.Net.Mail;

namespace Email
{
    public partial class email : Form
    {
        public email()
        {
            InitializeComponent();
            textBox6.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String from = textBox1.Text;
            String to = textBox2.Text;
            String subject =textBox3.Text;
            String body = richTextBox1.Text;
            String usrname = textBox5.Text;
            String pwd = textBox6.Text;

            if (to.Equals("") || subject.Equals("") || usrname.Equals("") || pwd.Equals("") || from.Equals(""))
            {
                MessageBox.Show("Please Enter the Required Details");
                return;
            }

            MailMessage mail = new MailMessage(from, to, subject, body);

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(usrname,pwd);
            client.EnableSsl = true;
            client.Send(mail);
            MessageBox.Show("Mail Sent!");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin ad1 = new Admin();
            ad1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            customerquery cq1 = new customerquery(2);
            cq1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            holidayrequest hr1 = new holidayrequest(1);
            hr1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            salaryrequest sr1 = new salaryrequest(3);
            sr1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hrdashboard hd1 = new hrdashboard(5);
            hd1.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
