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
               var endDate = DateTime.Parse(this.startDate.Text);
               XmlHelper xmlFileManager = new XmlHelper();
               var salesReport = xmlFileManager.ReadData(startDate, endDate);
               string XmlResultFileName = @"..\SalesByVendorReport.xml";
               xmlFileManager.ExportToXml(salesReport, XmlResultFileName);
               this.startDate.Text = "";
               this.endDate.Text = "";
               MessageBox.Show(@"MS SQL Export to XML file has been finished!");
           }
           catch (System.FormatException ex)
           {
               MessageBox.Show(
                   ex.Message + Environment.NewLine
                   + @"Date must be in format: ""YYYY.DD.MM""");
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
    }
}
