using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrinter.Common
{
    public class Order
    {
        public IList<LineItem> Purchase { get; set; }
        public StringBuilder receipt { get; set; }

        public string TotalTaxesPrint { get; set; }
        public string TotalPrint { get; set; }

        public Order()
        {
            Purchase = new List<LineItem>();
        }

        public void BuildPrintDetails()
        {
            BuildReceipt();
        }

        private void BuildReceipt()
        {
            decimal tax = default, total = default;
          
            foreach (var lineItem in Purchase)
            {
                tax += lineItem.TaxPerLine;
                total += lineItem.ValuePerLine;
               
                if(lineItem.ProductDetail.IsImported)
                    lineItem.LineItemPrint = $"{lineItem.Quantity} imported {lineItem.ProductDetail.Description.ToLower()}: {lineItem.ValuePerLine:n}\n";
                else
                    lineItem.LineItemPrint = $"{lineItem.Quantity} {lineItem.ProductDetail.Description.ToLower()}: {lineItem.ValuePerLine:n}\n";

            }
            TotalTaxesPrint = $"Sales Taxes: {tax:n}\n";
            TotalPrint = $"Total: {total:n}\n\n";

        }
    }
}
