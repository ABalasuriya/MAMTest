using System;
using System.Collections.Generic;
using ReceiptPrinter.Core;

namespace TaxService
{
    public class TaxServiceOne : ITaxService
    {
        public decimal BasicRate { get; set; }
        public decimal ImportRate { get ; set; }

        public void SetTaxForProductsInOrder(IEnumerable<LineItem> purchase)
        {
            foreach (var lineItem in purchase)
            {
                switch (lineItem)
                {
                    case LineItem lt when lt.ProductDetail.IsImported &&
                        lt.ProductDetail.ProductType != eProductExemptType.none:

                        lt.Tax = (BasicRate + ImportRate) / 100 * lt.ProductDetail.Price * lt.Quantity;
                        break;
                    case LineItem lt when lt.ProductDetail.IsImported &&
                        lt.ProductDetail.ProductType == eProductExemptType.none:
                            lt.Tax = ImportRate / 100 * lt.ProductDetail.Price * lt.Quantity;
                            break;
                    case LineItem lt when !lt.ProductDetail.IsImported &&
                        lt.ProductDetail.ProductType != eProductExemptType.none:
                            lt.Tax = BasicRate / 100 * lt.ProductDetail.Price * lt.Quantity;
                            break;
                    default:
                        break;
                }

            }

            
        }
    }
}
