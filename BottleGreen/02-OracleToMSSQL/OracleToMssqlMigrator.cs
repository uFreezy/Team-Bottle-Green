﻿using System.Linq;
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
                        if (foundMatch == false)
                        {
                            //CopyProductToMssql(oracleProduct, mssqlContext);
                            Product newProduct = new Product()
                            {
                                Product_Name = oracleProduct.PRODUCT_NAME,
                                Price = oracleProduct.PRICE,
                                Vendor = new Vendor()
                                {
                                    Vendor_Name = oracleProduct.VENDOR.VENDOR_NAME,
                                },
                                Measure = new Measure()
                                {
                                    Measure_Name = oracleProduct.MEASURE.MEASURE_NAME
                                }
                            };
                            mssqlContext.Products.Add(newProduct);
                            mssqlContext.SaveChanges();
                        }
                    }
                }
            }
        }

        public void CopyProductToMssql(PRODUCT oracleProduct, BottleGreenEntities mssqlContext)
        {
            using (mssqlContext)
            {
                Product newProduct = new Product()
                {
                    Product_Name = oracleProduct.PRODUCT_NAME,
                    Price = oracleProduct.PRICE,
                    Vendor = new Vendor()
                    {
                        Vendor_Name = oracleProduct.VENDOR.VENDOR_NAME,
                    },
                    Measure = new Measure()
                    {
                        Measure_Name = oracleProduct.MEASURE.MEASURE_NAME
                    }
                };
                mssqlContext.Products.Add(newProduct);
                mssqlContext.SaveChanges();
            }
        }
    }
}