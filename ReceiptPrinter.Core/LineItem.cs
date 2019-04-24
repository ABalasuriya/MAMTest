namespace ReceiptPrinter.Core
{
    public class LineItem
    {
        public int Quantity { get; set; }

        public Product  ProductDetail { get; set; }

        public decimal Tax { get; set; }

        public string LineItemPrint { get; set; }

    }
}