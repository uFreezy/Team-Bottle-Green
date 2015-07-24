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
using _02_OracleToMSSQL;
using _02_ZipRepportsToMSSQL;

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
            MessageBox.Show("Operation completed! It took us " + sw.Elapsed.TotalSeconds +" seconds to process your request!");
        }

        private void BottleGreen_Load(object sender, EventArgs e)
        {
        }

        private void btn_OracleToMssql_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            var d = new OracleToMssqlMigrator();
            d.CheckProductsInOracle();
            MessageBox.Show("Operation completed! It took us " + sw.Elapsed.TotalSeconds + " seconds to process your request!\nAll products updated.");
        }
    }
}
