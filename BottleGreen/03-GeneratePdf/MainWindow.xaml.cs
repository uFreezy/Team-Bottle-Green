using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Task3
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO: Fix an issue where report creation fails due to not being able to create / open file.

        private DateTime endDate = DateTime.Now;
        private DateTime startDate = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        private void ReportStartDate_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;


            if (calendar.SelectedDate.HasValue)
            {
                StartDate = calendar.SelectedDate.GetValueOrDefault(DateTime.Now);
                var box = FindName("StartDateBox") as TextBox;
            }
        }

        private void ReportEndDate_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;


            if (calendar.SelectedDate.HasValue)
            {
                EndDate = calendar.SelectedDate.GetValueOrDefault(DateTime.Now);
                var box = FindName("EndDateBox") as TextBox;
            }
        }

        private void MakePdfReportButton_OnClick(object sender, EventArgs e)
        {
            var Time = new Stopwatch();
            Time.Start();

            var Begin = startDate.Month + "/" + startDate.Day + "/" + startDate.Year;
            var End = endDate.Month + "/" + endDate.Day + "/" + endDate.Year;

            var data = GetData("SELECT " +
                               "sl.ReportDate," +
                               "p.[Product Name] AS Product," +
                               " sl.Quantity AS Quantitity," +
                               " sl.ActualPrice AS [Unit Price]," +
                               " s.Name AS [Location]," +
                               " sl.Quantity * sl.ActualPrice AS [Sum] " +
                               "FROM SalesReports sl " +
                               "INNER JOIN Products p ON sl.ProductId = p.ID " +
                               "INNER JOIN Supermarkets s ON sl.SupermarketId = s.Id " +
                               "WHERE sl.ReportDate > '" + Begin + "' AND sl.ReportDate < '" + End + "'");

            if (data.Rows.Count > 1)
            {
                var report = new Document();
                DateTime? date = null;
                double dateSum = 0;

                PdfWriter.GetInstance(report, new FileStream("../../Report.pdf", FileMode.Create));

                report.Open();

                var times = new Font(null, 16, Font.BOLD);

                var header = new PdfPTable(1);
                header.WidthPercentage = 100;
                var headerCell = new PdfPCell(new Phrase("Aggregated Sales Report", times));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                header.AddCell(headerCell);
                report.Add(header);

                var t = new PdfPTable(5);

                Console.WriteLine(data.Rows.Count);
                foreach (DataRow dataRow in data.Rows)
                {
                    t = new PdfPTable(5);

                    float[] widths = {2f, 1f, 1f, 3f, 1f};

                    t.SetWidths(widths);
                    t.DefaultCell.FixedHeight = 100f;
                    t.WidthPercentage = 100;

                    foreach (var item in dataRow.ItemArray)
                    {
                        if (item is DateTime)
                        {
                            if (date != (DateTime) item)
                            {
                                if (date != null)
                                {
                                    var finalDate = new PdfPCell(new Phrase("Total sum for: " + item,
                                        new Font(null, 11, Font.BOLD)));
                                    var finalSum = new PdfPCell();
                                    finalDate.Colspan = 4;
                                    finalSum.Colspan = 1;
                                    finalDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    finalSum.AddElement(new Chunk(dateSum.ToString()));
                                    t.AddCell(finalDate);
                                    t.AddCell(finalSum);
                                }

                                date = (DateTime) item;
                                var newDate = new Chunk(item.ToString());
                                var dateCell = new PdfPCell();
                                dateCell.Colspan = 5;
                                dateCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                dateCell.AddElement(newDate);
                                t.AddCell(dateCell);
                            }
                        }
                        else
                        {
                            var chunk = new Chunk(item.ToString());
                            var c = new PdfPCell();
                            c.AddElement(chunk);
                            t.AddCell(c);
                        }
                    }
                    report.Add(t);

                    dateSum += double.Parse(dataRow.ItemArray[dataRow.ItemArray.Count() - 1].ToString());
                }

                var lastDate = new PdfPCell();
                var lastSum = new PdfPCell();
                lastDate.Colspan = 4;
                lastSum.Colspan = 1;
                lastDate.AddElement(new Chunk("Total sum for: " + date));
                lastSum.AddElement(new Chunk(dateSum.ToString()));
                t.AddCell(lastDate);
                t.AddCell(lastSum);

                report.Add(t);

                report.Close();

                var result = MessageBox.Show("PDF Report created successfully! It took us: "
                                             + Time.Elapsed.TotalSeconds + " seconds to complete your request.",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
            }

            else
            {
                var error = MessageBox.Show("There were no entries for the " +
                                            "given period of time!", "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (error == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private DataTable GetData(string query)
        {
            var conString = ConfigurationManager.ConnectionStrings["BottleGreenEntities"].ConnectionString;

            var cmd = new SqlCommand(query);
            using (var con = new SqlConnection(conString))
            {
                using (var sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (var dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}