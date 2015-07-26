


namespace _04_VendorReportToXML
{
    using System;

    public class Summary
    {
        public Summary(DateTime initDate, decimal initTotalSum)
        {
            this.DateOfSale = initDate;
            this.TotalSum = initTotalSum;
        }

        public decimal TotalSum { get; set; }

        public DateTime DateOfSale { get; set; }
    }
}
