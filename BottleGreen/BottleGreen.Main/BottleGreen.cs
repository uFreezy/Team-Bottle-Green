using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using _04_VendorReportToXML;
using _02_ZipRepportsToMSSQL;
using _06_XMLToMSSQL;
using System.Threading;
using System.Globalization;
using _02_OracleToMSSQL;
using _05_JSONReportsToMongoDB;
using _08_SQLiteAndMySQLToExcel;


namespace BottleGreen.Main
{
    public partial class BottleGreen : Form
    {
        public BottleGreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            var d = new DataMigrator();
            d.MigrateDataFromExcelFiles();
            MessageBox.Show(
                string.Format("Operation completed! It took us {0: 0.##} seconds to process your request!",
                sw.Elapsed.TotalSeconds));
        }

        private void BottleGreen_Load(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void XMLtoMSSQL_Click(object sender, EventArgs e)
        {

            var FD = new System.Windows.Forms.OpenFileDialog();
            string fileName = String.Empty;
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                 fileName = FD.FileName;
            }
            var sw = new Stopwatch();
            sw.Start();
            XmltoMssql.PolulateSqlTables(fileName);
            MessageBox.Show(string.Format("Data was sucessufuly transfered from file: {0}" +
                                          "\n It took us {1: 0.##} seconds", fileName, sw.Elapsed.TotalSeconds));
        }

        private void MsqlToXML_Click(object sender, EventArgs e)
        {

           Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

           try
           {
               var startDate = DateTime.Parse(this.startDate.Text);
               var endDate = DateTime.Parse(this.endDate.Text);
               var dateCheck = startDate > endDate;
               if (dateCheck)
               {
                   throw new ArgumentException();
               }
               XmlHelper xmlFileManager = new XmlHelper();
               var salesReport = xmlFileManager.ReadData(startDate, endDate);
               string XmlResultFileName = @"..\..\SalesByVendorReport.xml";
               xmlFileManager.ExportToXml(salesReport, XmlResultFileName);
               this.startDate.Text = "";
               this.endDate.Text = "";
               MessageBox.Show(@"MS SQL Export to XML file has been finished!");
               var xmlReaderStr = "notepad.exe";
               Process.Start(xmlReaderStr, XmlResultFileName);
           }
           catch (System.FormatException fEx)
           {
               MessageBox.Show(
                   fEx.Message + Environment.NewLine
                   + @"Date must be in format: ""YYYY.DD.MM""");
           }
           catch (System.ArgumentException argEx)
           {
               MessageBox.Show(
                   argEx.Message + Environment.NewLine
                   + @"Starting date must be earlier than end date!");
           }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("Task3.exe");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_OracleToMssql_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            var d = new OracleToMssqlMigrator();
            d.CheckProductsInOracle();
            MessageBox.Show("Operation completed! It took us " + sw.Elapsed.TotalSeconds + " seconds to process your request!\nAll products updated.");
        }

        private void btn_JsonReports_Click(object sender, EventArgs e)
        {
            string start = tx_start.Text;
            string end = tx_end.Text;
            try
            {
                DateTime d1 = DateTime.Parse(start);
                DateTime d2 = DateTime.Parse(end);
                if (d1 > d2)
                {
                    throw new ArgumentException("The end date has to be after the start date");
                }
                var r = new JsonReportCreator();
                r.GenerateJsonReports(d1, d2);
                MessageBox.Show("Reports created!");
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(string.Format("You have to enter dates for start and end\n{0}", ane.Message));
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message);
            }
            catch (FormatException fe)
            {
                MessageBox.Show(string.Format("Enter the dates in the following format: MM/DD/YYYY\n{0}", fe.Message));
            }
        }

        private void btn_ToMongo_Click(object sender, EventArgs e)
        {
            string start = tx_start.Text;
            string end = tx_end.Text;
            try
            {
                DateTime d1 = DateTime.Parse(start);
                DateTime d2 = DateTime.Parse(end);
                if (d1 > d2)
                {
                    throw new ArgumentException("The end date has to be after the start date");
                }

                var r = new JsonReportCreator();
                r.UploadMongoReports(d1, d2);
                MessageBox.Show("Reports uploaded!");
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(string.Format("You have to enter dates for start and end\n{0}", ane.Message));
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message);
            }
            catch (FormatException fe)
            {
                MessageBox.Show(string.Format("Enter the dates in the following format: DD/MM/YYYY\n{0}", fe.Message));
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button_SQLiteAndMSSQLToExcel_Click(object sender, EventArgs e)
        {
            ReportToExcel.RunExcelReport(@"D:\DbApps\Team-Bottle-Green-\BottleGreen\08-SQLiteAndMySQLToExcel\Report.xlsx");
        }
    }
}
