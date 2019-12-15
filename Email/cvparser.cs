using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//using TextSharp.text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf.parser;
using Console = System.Console;
using System.Collections;

namespace Email
    
{
    public partial class cvparser : Form
    {



        public string fName="";
        public int dep_id;
        public int id = 0;
        public static void PrintValues(IEnumerable myList)
        {
            foreach (Object obj in myList)
                Console.Write("   {0}", obj);
            Console.WriteLine();
        }
        List<string[]> aList = new List<string[]>
        {
            new string[] { "java", "c++", "flutter" ,"software","application","core"},
            new string[] { "python", "r", "data analysis","data science","deep learning","machine learning"}
};

        public string[] departments = {"Android","Data_Science","Blockchain","Marketing","IoS","Finance","Software_Development","DevOps","Testing"};
        public string[] stringArray = { "text1", "testtest", "test1test2", "test2text1" };
        public string[] Android = { "java", "c++", "flutter" ,"software","application","core"};
        public string[] Data_Science = { "python", "r", "data analysis","data science","deep learning","machine learning","sql","pandas","numpy"};
        public string[] Finance = {"finance","analysis","economics", "decisiveness", "dersuasiveness","mathematics","business","communication"};
        public string[] IoS = { "swift", "apple", "ui", "ux", "spatial reasoning", "networking" };
        public string[] Blockchain = { "blockchain","cryptography","hashing","node.js","ethereumjs" ,"web3.js" };


        public ArrayList myL = new ArrayList();
        public StringBuilder sb = new StringBuilder();
        public StringBuilder sc = new StringBuilder();








        public cvparser()
        {
            InitializeComponent();
            


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //myL.Add(Android);
           // myL.Add(Data_Science);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Pdf Files|*.pdf";
            dlg.Title = "Open";
            dlg.ShowDialog();
            fName = dlg.FileName;
            StreamReader sr = new StreamReader(fName);
            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader(fName))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }
            checkBox1.Checked = true;

            string doc = text.ToString();
            String doc_lower = doc.ToLower();
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(doc_lower) ;

            //StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in emailMatches)
            {
                sb.AppendLine(emailMatch.Value);
            }
            richTextBox1.AppendText("Email: "+sb.ToString());
            Regex phoneRegex = new Regex(@"\d{10}", RegexOptions.IgnoreCase);
            MatchCollection phoneMatches = phoneRegex.Matches(doc_lower);
            //StringBuilder sc = new StringBuilder();

            foreach (Match phoneMatch in phoneMatches)
            {
                sc.AppendLine(phoneMatch.Value);
            }
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText("Phone Number: " + sc.ToString());
            Console.WriteLine(sc.Length);
            //richTextBox1.AppendText(reader["address"].ToString() + "\t");
            //richTextBox1.AppendText(reader["salary"].ToString() + "\t");
            StringBuilder patternBuilder = new StringBuilder();
            patternBuilder.Append(@"First name: (?<fn>.*$)\n")
                .Append("Last name: (?<ln>.*$)\n")
                .Append("Address: (?<address>.*$)\n")
                .Append("City: (?<city>.*$)\n")
                .Append("State: (?<state>.*$)\n")
                .Append("Zip: (?<zip>.*$)");
            Match match1 = Regex.Match(doc_lower, patternBuilder.ToString(), RegexOptions.Multiline | RegexOptions.IgnoreCase);
            string fullname = string.Concat(match1.Groups["fn"], " ", match1.Groups["ln"]);
            string address = match1.Groups["address"].ToString();
            string city = match1.Groups["city"].ToString();
            string state = match1.Groups["state"].ToString();
            string zip = match1.Groups["zip"].ToString();
            richTextBox1.AppendText("\n");
            richTextBox1.AppendText(fullname);






            int[] count = new int[] { 0,0 };
            int count_i=0,match=0;
            foreach (var item in aList)
            {
                match = 0;
                for (int i = 0; i < item.Length; i++)
                {
                    if (doc_lower.Contains(item[i]))
                    {
                        count[count_i] = match++;
                    }
                }
                count_i++;
            }
            int maxValue = count.Max();
            int maxIndex = count.ToList().IndexOf(maxValue);
            textBox1.Text = departments[maxIndex];

            sr.Close();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            id++;
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                string stm = "select max(app_id) from applicant";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                //string version = Convert.ToString(cmd.ExecuteScalar());
                //richTextBox1.Text = version;

               
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {



                      while (reader.Read())
                            {

                                id = Int32.Parse(reader["max(app_id)"].ToString());

                            }

                        }
                    

                
                Console.WriteLine(id.ToString());
                string query = "insert into applicant VALUES('" + (id+1) + "','" + sc.ToString() + "','" + sb.ToString() +"');";
                cmd = new MySqlCommand(query, conn);

                MySqlDataReader myread;
                myread = cmd.ExecuteReader();
                conn.Close();
                conn = new MySqlConnection(cs);
                conn.Open();
                //MessageBox.Show("Inserted Data");

                query = "insert into recommendation VALUES('" + textBox1.Text + "','" + (id+1) + "','" + sb.ToString() + "','" + fName + "');";
                cmd = new MySqlCommand(query, conn);
                myread = cmd.ExecuteReader();
                //MessageBox.Show("Inserted Data");
                conn.Close();
                conn = new MySqlConnection(cs);
                conn.Open();
                query = "insert into CV VALUES('" + sb.ToString() + "','" + fName + "','" + (id+1) +"');";
                cmd = new MySqlCommand(query, conn);
                myread = cmd.ExecuteReader();
                //MessageBox.Show("Inserted Data");
                conn.Close();
                conn = new MySqlConnection(cs);
                conn.Open();
                query = "select dept_id  from department where dept_name = '" + textBox1.Text + "';";
                cmd = new MySqlCommand(query, conn);




                //string version = Convert.ToString(cmd.ExecuteScalar());
                //richTextBox1.Text = version;


               
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        dep_id = Int32.Parse(reader["dept_id"].ToString());


                    }
                }
                conn.Close();
                //MessageBox.Show("executed");
                conn = new MySqlConnection(cs);
                conn.Open();
                query = "insert into applies VALUES('" + (id+1) + "','" + dep_id + "');";
                cmd = new MySqlCommand(query, conn);
                myread = cmd.ExecuteReader();
                MessageBox.Show("Inserted Data");
                Console.WriteLine(dep_id);
                conn.Close();
               
                





                /*string cmdText = "INSERT INTO applicant VALUES (@app_id,@phone,@email)";
                 cmd = new MySqlCommand(cmdText, conn);

                 cmd.Parameters.AddWithValue("@app_id", id+1);
                 cmd.Parameters.AddWithValue("@phone", sc.ToString());
                 cmd.Parameters.AddWithValue("@email", sb.ToString());
                 cmd.ExecuteNonQuery();
                 conn.Close();
                 conn = new MySqlConnection(cs);
                 conn.Open();
                 cmdText = "INSERT INTO recommendation VALUES (@department_name,@app_id,@email,@file)";
                 cmd = new MySqlCommand(cmdText, conn);
                 cmd.Parameters.AddWithValue("@department_name",textBox1.Text);
                 cmd.Parameters.AddWithValue("@app_id", 1);
                 cmd.Parameters.AddWithValue("@email", sb.ToString());
                 cmd.Parameters.AddWithValue("@file", fName);
                 cmd.ExecuteNonQuery();
                 conn.Close();*/
                /*cs = @"server=localhost;userid=root;
             password=HelpPls2;database=cms";
                conn = new MySqlConnection(cs);
                  conn.Open();
                   string cmdText = "INSERT INTO CV VALUES (@email,@file,@app_id)";
                   cmd = new MySqlCommand(cmdText, conn);
                   cmd.Parameters.AddWithValue("@email", sb.ToString());
                   cmd.Parameters.AddWithValue("@file", fName);
                   cmd.Parameters.AddWithValue("@app_id", id + 1);
                   cmd.ExecuteNonQuery();*/







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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
