using System;
using System.Collections.Generic;
using ReceiptPrinter.Common;

namespace TaxServices
{
    public class TaxServiceOne : ITaxService
    {
        public decimal BasicTaxRate { get; }
        public decimal ImportTaxRate { get ; }
        public TaxServiceOne(Decimal basicTaxRate, Decimal importTaxRate)
        {
            BasicTaxRate = basicTaxRate;
            ImportTaxRate = importTaxRate;
        }
        public void SetTaxForProductsInOrder(IEnumerable<LineItem> purchase)
        {
            foreach (var lineItem in purchase)
            {
                switch (lineItem)
                {
                    case LineItem lt when lt.ProductDetail.IsImported &&
                        lt.ProductDetail.ProductType == eProductExemptType.none:
                            lt.TaxPerLine = Math.Ceiling(((BasicTaxRate + ImportTaxRate) / 100 * lt.ProductDetail.Price * lt.Quantity) * 20 ) / 20;
                            lt.ValuePerLine = (lt.ProductDetail.Price * lt.Quantity) + lt.TaxPerLine;
                            break;
                    case LineItem lt when lt.ProductDetail.IsImported &&
                        lt.ProductDetail.ProductType != eProductExemptType.none:
                            lt.TaxPerLine = Math.Ceiling((ImportTaxRate / 100 * lt.ProductDetail.Price * lt.Quantity) * 20) / 20;
                            lt.ValuePerLine = (lt.ProductDetail.Price * lt.Quantity) + lt.TaxPerLine;
                        break;
                    case LineItem lt when !lt.ProductDetail.IsImported &&
                        lt.ProductDetail.ProductType == eProductExemptType.none:
                            lt.TaxPerLine = Math.Ceiling((BasicTaxRate / 100 * lt.ProductDetail.Price * lt.Quantity) * 20) / 20;
                            lt.ValuePerLine = (lt.ProductDetail.Price * lt.Quantity) + lt.TaxPerLine;
                        break;
                    default:
                        lineItem.ValuePerLine = lineItem.ProductDetail.Price * lineItem.Quantity;
                        break;
                }

            }

            
        }
    }
}
