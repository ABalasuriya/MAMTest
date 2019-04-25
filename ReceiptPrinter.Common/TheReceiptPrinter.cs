using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReceiptPrinter.Common;

namespace ReceiptPrinter.Common
{
    public class TheReceiptPrinter
    {
        private readonly ITaxService taxService;

        public IList<Order> Orders { get;  } 

        public TheReceiptPrinter(ITaxService taxService)
        {
            Orders = new List<Order>();
            this.taxService = taxService;
        }

        public string DisplayReceipts()
        {
            var receipt = new StringBuilder();
            Orders.ToList().ForEach(i => {
                taxService.SetTaxForProductsInOrder(i.Purchase);
                i.BuildPrintDetails();
            });
            
            foreach (var order in this.Orders)
            {
                order.Purchase.ToList().ForEach(p => receipt.Append(p.LineItemPrint));
                receipt.Append(order.TotalTaxesPrint);
                receipt.Append(order.TotalPrint);
            }

            return receipt.ToString();
        }
    }
}