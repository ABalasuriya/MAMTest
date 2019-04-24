namespace ReceiptPrinter.Common
{
    public class Product
    {
        public string Description { get; set; }
        public eProductExemptType ProductType { get; set; }
        public decimal Price { get; set; }
        public bool IsImported { get; set; }
    }
}