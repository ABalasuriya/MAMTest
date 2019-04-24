using System.Collections.Generic;

namespace ReceiptPrinter.Common
{
    public interface ITaxService
    {
        decimal  BasicTaxRate { get; }
        decimal ImportTaxRate { get; }

        void SetTaxForProductsInOrder(IEnumerable<LineItem> purchase);
    }
}