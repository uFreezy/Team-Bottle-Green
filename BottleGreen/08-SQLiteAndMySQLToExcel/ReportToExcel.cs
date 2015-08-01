namespace _08_SQLiteAndMySQLToExcel
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Office.Interop.Excel;
    using MySql.Data.MySqlClient;
    using SQL = System.Data;

    public class ReportToExcel
    {
        public static void Main()
        {
            string sqlSelect = "SELECT * FROM Taxes";
            string sqliteDataSourcePath = @"..\..\..\08-SQLiteAndMySQLToExcel\SQLiteDb.db";

            var sqliteConnection = new SQLiteConnection("Data Source=" + sqliteDataSourcePath);
            var sqliteSelectCommand = new SQLiteCommand(sqlSelect, sqliteConnection);
            var sales = new List<Sales>();

            SQLiteDataReader sqLiteDataReader = null;

            var productTaxes = new SQL.DataTable("Report");

            productTaxes.Columns.Add("Vendor");
            productTaxes.Columns.Add("Incomes");
            productTaxes.Columns.Add("Expenses");
            productTaxes.Columns.Add("TotalTaxes");
            productTaxes.Columns.Add("Financial result");

            String selectData =
                "SELECT " +
                    "v.name AS Vendor," +
                    "p.name AS Product, " +
                    "IFNULL((SELECT SUM(p.price) " +
                            "FROM products p " +
                                "JOIN vendors ve " +
                                    "ON ve.id = p.vendor_id " +
                                "JOIN sales s " +
                                    "ON s.product_id = p.id " +
                            "WHERE ve.id = v.id), 0) AS Incomes, " +
                    "IFNULL((SELECT SUM(e.expense_value) " +
                            "FROM vendors ven " +
                                "JOIN vendors_expenses vex " +
                                    "ON vex.vendor_id = ven.id " +
                                "JOIN expenses e " +
                                    "ON e.id = vex.expense_id " +
                            "WHERE ven.id = v.id), 0) AS Expenses " +
                "FROM vendors v " +
                    "JOIN products p " +
                        "ON p.vendor_id = v.id " +
                "GROUP BY v.id, v.name, p.name";

            String mysqlDataSource = "server=localhost;Database=chain_of_supermarkets;uid=softuni;pwd=1234567;";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlDataSource);
            MySqlCommand mysqlSelectCommand = new MySqlCommand(selectData, mySqlConnection);
            MySqlDataReader mySqlDataReader = null;

            try
            {
                sqliteConnection.Open();
                sqLiteDataReader = sqliteSelectCommand.ExecuteReader();
                mySqlConnection.Open();
                mySqlDataReader = mysqlSelectCommand.ExecuteReader();

                while (sqLiteDataReader.Read() && mySqlDataReader.Read())
                {
                    var name = mySqlDataReader.GetString(0);
                    var income = mySqlDataReader.GetDecimal(2);
                    var expense = mySqlDataReader.GetDecimal(3);
                    var getTax = Convert.ToDecimal(sqLiteDataReader["Tax"].ToString());
                    var tax = income * (getTax / 100);
                    tax = Math.Round(tax, 2);
                    var sale = new Sales(name, income, expense, tax);
                    sales.Add(sale);
                }

                var groupSales = sales.GroupBy(s => new
                {
                    s.Name,
                    s.Expense,
                    s.Income
                }).Select(s => new
                {
                    VendorName = s.Key.Name,
                    Expense = s.Key.Expense,
                    Income = s.Key.Income,
                    FinancialResult =
                        (s.Key.Income - s.Key.Expense - s.Sum(se => se.Tax)
                        ).ToString(new CultureInfo("en-US")),
                    Tax = s.Sum(se => se.Tax)
                        .ToString(new CultureInfo("en-US"))
                }).ToList();

                groupSales.ForEach(
                    gs => productTaxes.Rows.Add(
                        gs.VendorName, 
                        gs.Income, 
                        gs.Expense, 
                        gs.Tax, 
                        gs.FinancialResult));
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured!");
            }
            finally
            {
                if (sqLiteDataReader != null)
                {
                    sqLiteDataReader.Close();
                }

                if (sqLiteDataReader != null)
                {
                    mySqlDataReader.Close();
                }

                sqliteConnection.Close();
            }

            SQL.DataSet dataSet = new SQL.DataSet();
            dataSet.Tables.Add(productTaxes);
            ExportDataToExcel(dataSet);
        }

        private static void ExportDataToExcel(SQL.DataSet dataSet)
        {
            const int VendorsIndex = 1;
            const int IncomesIndex = 2;
            const int ExpensesIndex = 3;
            const int TotalTaxesIndex = 4;
            const int FinancialResultIndex = 5;

            var excelApplication = new Application();
            //String excelReportPath = @"D:\DbApps\Team-Bottle-Green-\BottleGreen\08-SQLiteAndMySQLToExcel\Report.xlsx";
            //String excelReportPathBackup = String.Format(@"D:\DbApps\Team-Bottle-Green-\BottleGreen\08-SQLiteAndMySQLToExcel\Report-{0}-{1}-{2}-{3}-{4}-{5}.xlsx",
            String excelReportPath = @"C:\Users\p.kanev\Desktop\BottleGreen\08-SQLiteAndMySQLToExcel\Report.xlsx";
            String excelReportPathBackup = String.Format(@"..\..\..\Excel-Reports\Report-{0}-{1}-{2}-{3}-{4}-{5}.xlsx",
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second);

            if (File.Exists(excelReportPath))
            {
                File.Move(excelReportPath, excelReportPathBackup);
            }

            var excelWorkBook = excelApplication.Workbooks.Add(Missing.Value);

            foreach (SQL.DataTable table in dataSet.Tables)
            {
                Worksheet excelWorksheet = excelWorkBook.Sheets.Add();

                excelWorksheet.Name = table.TableName;

                excelWorksheet.Cells[1, VendorsIndex] = table.Columns[VendorsIndex - 1].ColumnName;
                excelWorksheet.Cells[1, IncomesIndex] = table.Columns[IncomesIndex - 1].ColumnName;
                excelWorksheet.Cells[1, ExpensesIndex] = table.Columns[ExpensesIndex - 1].ColumnName;
                excelWorksheet.Cells[1, TotalTaxesIndex] = table.Columns[TotalTaxesIndex - 1].ColumnName;
                excelWorksheet.Cells[1, FinancialResultIndex] = table.Columns[FinancialResultIndex - 1].ColumnName;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        excelWorksheet.Cells[i + 2, j + 1] = table.Rows[i].ItemArray[j].ToString();
                    }
                }
            }

            excelWorkBook.SaveAs(excelReportPath);
            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApplication.Quit();

            if (File.Exists(excelReportPath))
            {
                RunExcelReport(excelReportPath);
            }

        }

        public static void RunExcelReport(string path = @"C:\Users\p.kanev\Desktop\BottleGreen\08-SQLiteAndMySQLToExcel\Report.xlsx")
        {
            Process.Start(path);
        }
    }
}
