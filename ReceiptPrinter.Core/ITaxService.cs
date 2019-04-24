using System.Collections.Generic;

namespace ReceiptPrinter.Core
{
    public interface ITaxService
    {
        decimal  BasicRate { get; set; }
        decimal ImportRate { get; set; }

        void SetTaxForProductsInOrder(IEnumerable<LineItem> purchase);
    }
}