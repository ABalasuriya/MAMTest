namespace ReceiptPrinter.Common
{
    public class LineItem
    {
        public int Quantity { get; set; }

        public Product  ProductDetail { get; set; }

        public decimal TaxPerLine { get; set; }

        public decimal ValuePerLine { get; set; }

        public string LineItemPrint { get; set; }

    }
}