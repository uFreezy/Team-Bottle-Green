using System.Linq;
using System.Linq.Expressions;
using BottleGreen.Models;
using BottleGreen.Models.Oracle;

namespace _02_OracleToMSSQL
{
    public class OracleToMssqlMigrator
    {
        public void CheckProductsInOracle()
        {
            var mssqlContext = new BottleGreenEntities();
            using (mssqlContext)
            {
                var oracleContext = new BottleGreenOracleContext();
                using (oracleContext)
                {
                    var oracleProducts = oracleContext.PRODUCTS
                        .Select(p => p);
                    bool foundMatch = false;
                    foreach (var oracleProduct in oracleProducts)
                    {
                        var sqlProductsNames = mssqlContext.Products
                            .Select(p => p.Product_Name);
                        foreach (var sqlProductName in sqlProductsNames)
                        {
                            if (sqlProductName == oracleProduct.PRODUCT_NAME)
                            {
                                foundMatch = true;
                            }
                        }
                        if (!foundMatch)
                        {
                            var vendor = mssqlContext.Vendors
                                .FirstOrDefault(v => v.Vendor_Name == oracleProduct.VENDOR.VENDOR_NAME);
                            if (vendor == null)
                            {
                                Vendor v = new Vendor()
                                {
                                    Vendor_Name = oracleProduct.VENDOR.VENDOR_NAME,
                                };
                                mssqlContext.Vendors.Add(v);
                            }
                            var measure = mssqlContext.Measures
                                .FirstOrDefault(m => m.Measure_Name == oracleProduct.MEASURE.MEASURE_NAME);
                            if (measure == null)
                            {
                                Measure m = new Measure()
                                {
                                    Measure_Name = oracleProduct.MEASURE.MEASURE_NAME
                                };
                                mssqlContext.Measures.Add(m);
                            }
                            Product newProduct = new Product()
                            {
                                Product_Name = oracleProduct.PRODUCT_NAME,
                                Price = oracleProduct.PRICE,
                                VendorID = mssqlContext.Vendors
                                    .Where(v => v.Vendor_Name == oracleProduct.VENDOR.VENDOR_NAME)
                                    .Select(v=>v.ID).FirstOrDefault(),
                                MeasureID = mssqlContext.Measures
                                    .Where(m=>m.Measure_Name == oracleProduct.MEASURE.MEASURE_NAME)
                                    .Select(m=>m.ID).FirstOrDefault()
                            };
                            mssqlContext.Products.Add(newProduct);
                            mssqlContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}