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
using _02_ZipRepportsToMSSQL;
using _06_XMLToMSSQL;

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
            MessageBox.Show("Operation completed! It took us " + sw.Elapsed.TotalSeconds +
                            " seconds to process your request!");
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

                //System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);

                ////OR

                //System.IO.StreamReader reader = new System.IO.StreamReader(fileToOpen);
                ////etc
            }

            MessageBox.Show(fileName);

            XMLToMSSQL.PolulateSqlTables(fileName);
        }
    }
}
