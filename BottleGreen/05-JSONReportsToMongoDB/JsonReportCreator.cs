using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BottleGreen.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace _05_JSONReportsToMongoDB
{
    public class JsonReportCreator
    {
        private class Report
        {
            [JsonIgnore]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [JsonProperty("product-id")]
            [BsonElement("product-id")]
            public int ProductId { get; set; }

            [JsonProperty("product-name")]
            [BsonElement("product-name")]
            public string ProductName { get; set; }

            [JsonProperty("vendor-name")]
            [BsonElement("vendor-name")]
            public string VendorName { get; set; }

            [JsonProperty("total-quantity-sold")]
            [BsonElement("total-quantity-sold")]
            public int? TotalQuantitySold { get; set; }

            [JsonProperty("total-incomes")]
            [BsonElement("total-incomes")]
            public decimal? TotalIncomes { get; set; }

            [JsonIgnore]
            [BsonElement("start-date")]
            public DateTime Start { get; set; }

            [JsonIgnore]
            [BsonElement("end-date")]
            public DateTime End { get; set; }
        }

        public void GenerateJsonReports(DateTime start, DateTime end)
        {
            if (!Directory.Exists(@"../../../Json-Reports/"))
            {
                Directory.CreateDirectory(@"../../../Json-Reports/");
            }

            using (var context = new BottleGreenEntities())
            {
                var products = GetProducts(context, start, end);
                var serializer = new JavaScriptSerializer();
                foreach (var product in products)
                {
                    if (product.TotalQuantitySold == null)
                    {
                        product.TotalQuantitySold = 0;
                    }
                    if (product.TotalIncomes == null)
                    {
                        product.TotalIncomes = 0;
                    }
                
                    string path = "../../../Json-Reports/";
                    string dirName = String.Format("{0}.{1}.{2}-{3}.{4}.{5}/",
                        start.Year, start.Month, start.Day,
                        end.Year, end.Month, end.Day);
                    path = path + dirName;
                    if (!Directory.Exists(@path))
                    {
                        Directory.CreateDirectory(@path);
                    }
                    var json = JsonConvert.SerializeObject(product, Formatting.Indented);
                    path = path + string.Format("{0}.json", product.ProductId);
                    System.IO.File.WriteAllText(path, json);
                    // opens files in notepad
                    var xmlReaderStr = "notepad.exe";
                    Process.Start(xmlReaderStr, path);
                }
            }

        }
        public void UploadMongoReports(DateTime start, DateTime end)
       {
            var client = new MongoClient();
            var db = client.GetDatabase("BottleGreen");
            var collection = db.GetCollection<Report>("SalesByProductReports");

            using (var context = new BottleGreenEntities())
            {
                var reports = GetProducts(context, start, end);
                foreach (var report in reports)
                {
                    if (report.TotalQuantitySold == null)
                    {
                        report.TotalQuantitySold = 0;
                    }
                    if (report.TotalIncomes == null)
                    {
                        report.TotalIncomes = 0;
                    }
                    collection.InsertOneAsync(report);                    
                }
            }
        }

        private static IQueryable<Report> GetProducts(BottleGreenEntities context, DateTime start, DateTime end)
        {
            var products = from p in context.Products
                           where p.SalesReports.Any(r=>r.ReportDate <= end) &&
                                p.SalesReports.Any(r=>r.ReportDate >= start)
                select new Report()
                {
                    ProductId = p.ID,
                    ProductName = p.Product_Name,
                    VendorName = p.Vendor.Vendor_Name,
                    TotalQuantitySold = p.SalesReports.Select(s => s.Quantity).Sum(),
                    TotalIncomes = p.SalesReports.Select(s => s.Quantity).Sum()*p.Price,
                    Start = start,
                    End = end
                };
            return products;
        }
    }
}
