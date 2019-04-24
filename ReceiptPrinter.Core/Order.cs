using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrinter.Core
{
    public class Order
    {
        public IList<LineItem> Purchase { get; set; }
        public StringBuilder receipt { get; set; }

        private string TotalTaxesPrint { get; set; }
        private string TotalPrint { get; set; }

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
                tax += lineItem.Tax;
                total += lineItem.Quantity * lineItem.ProductDetail.Price;
#                
                if(lineItem.ProductDetail.IsImported)
                    lineItem.LineItemPrint = $"{lineItem.Quantity} imported {lineItem.ProductDetail.Description}: {lineItem.ProductDetail:n}\n";
                else
                    lineItem.LineItemPrint = $"{lineItem.Quantity} {lineItem.ProductDetail.Description}: {lineItem.ProductDetail:n}\n";

            }
            TotalTaxesPrint = $"Sales Taxes: {tax:n}";
            TotalPrint = $"Total: {total:n}\r\n";

        }
    }
}
