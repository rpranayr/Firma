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
using Microsoft.Extensions.Configuration;
using Accord.Controls;
using Accord.Statistics.Models.Regression.Linear;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Statistics.Kernels;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;


namespace Email
{


    public partial class stockpredict : Form
    {
        public DataTable dt = new DataTable();
        public static void PrintValues(IEnumerable myList)
        {
            foreach (Object obj in myList)
                Console.Write("   {0}", obj);
            Console.WriteLine();
        }
        public ArrayList myList = new ArrayList();
        public ArrayList delivery = new ArrayList();
        public ArrayList months = new ArrayList();
        public stockpredict()
        {

            InitializeComponent();

            //double[] inputs = { 80, 60, 10, 20, 30 };
            //double[] outputs = { 20, 40, 30, 50, 60 };

            // Use Ordinary Least Squares to learn the regression
            //OrdinaryLeastSquares ols = new OrdinaryLeastSquares();

            // Use OLS to learn the simple linear regression
            //SimpleLinearRegression regression = ols.Learn(inputs, outputs);

            // Compute the output for a given input:
            //double y = regression.Transform(85); // The answer will be 28.088
            //MessageBox.Show(y.ToString());

            // We can also extract the slope and the intercept term
            // for the line. Those will be -0.26 and 50.5, respectively.
            // double s = regression.Slope;     // -0.264706
            //double c = regression.Intercept; // 50.588235
            //MLContext mlContext = new MLContext();

            //DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<HouseData>();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {


        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Chart2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string cs = @"server=localhost;userid=root;password=root;database=dbs";
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                string stm = "select * from stocks";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                //string version = Convert.ToString(cmd.ExecuteScalar());
                //richTextBox1.Text = version;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // access your record colums by using reader
                        myList.Add(reader["price"]);
                        months.Add(reader["month"]);
                        //Console.WriteLine(reader["price"]);
                        //MessageBox.Show(reader["price"].ToString());
                    }
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
            PrintValues(months);

            Double[] input = months.ToArray(typeof(double)) as double[];
            Double[] output = myList.ToArray(typeof(double)) as double[];

            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();

            // Use OLS to learn the simple linear regression
            SimpleLinearRegression regression = ols.Learn(input, output);

            // Compute the output for a given input:
            double a = double.Parse(textBox1.Text);
            textBox2.Text = regression.Transform(a).ToString();
            var chart = chart2.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            // The answer will be 28.088
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 1;
            chart.AxisY.Minimum = 0;
            chart.AxisX.Maximum = 12;
            chart.AxisY.Maximum = 100;
            chart.AxisX.Interval = 1;
            chart.AxisY.Interval = 5;

            chart2.Series.Add("Test Sample");
            chart2.Series["Test Sample"].ChartType = SeriesChartType.Line;
            chart2.Series["Test Sample"].Color = Color.Red;
            chart2.Series[0].IsVisibleInLegend = false;

            for (int i = 0; i < input.Length; i++)
            {
                chart2.Series["Test Sample"].Points.AddXY(input[i], output[i]);
            }
            chart2.Series["Test Sample"].Points.AddXY(a, regression.Transform(a));
            

            //MessageBox.Show(y.ToString());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //login_page fe4 = new login_page();
            //fe4.Show();
            //Form5 f5 = new Form5();
            //f5.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
