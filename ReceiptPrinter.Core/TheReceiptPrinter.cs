using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ReceiptPrinter.Core
{
    public class TheReceiptPrinter
    {
        private readonly ITaxService taxService;

        public IList<Order> Orders { get; set; } 
        public TheReceiptPrinter(ITaxService taxService)
        {
            Orders = new List<Order>();
            this.taxService = taxService;
        }

        public string DisplayReceipts()
        {
            var receipt = new StringBuilder();
            Orders.ToList().ForEach(i => taxService.SetTaxForProductsInOrder(i.Purchase));
            Orders.ToList().ForEach(i => i.BuildPrintDetails());
            
            foreach (var order in this.Orders)
            {
                order.Purchase.ToList().ForEach(p => receipt.Append(p.LineItemPrint));
            }

            return receipt.ToString();
        }
    }
}