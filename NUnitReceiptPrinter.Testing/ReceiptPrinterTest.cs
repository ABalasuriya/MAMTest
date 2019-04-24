using NUnit.Framework;
using ReceiptPrinter.Common;
using TaxServices;

namespace NUnitReceiptPrinter.Testing
{
    [TestFixture]
    public class ReceiptPrinterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("ReceiptPrinter Testing")]
        [TestCase(1, "Book", 1, 12.49, false)]
        public void Should_have_single_item_in_order_collection(int quantity, 
            string description,
            int taxExepmt,
            decimal price,
            bool isImported)
        {
            // Arrange 

            //Create SUT
            var sut = new TheReceiptPrinter(new TaxServiceOne(10m, 5m))
            {
                Orders = {
                    new Order {
                        Purchase = {
                            new LineItem {
                                Quantity = quantity,
                                ProductDetail = new Product {
                                    Description = description,
                                    ProductType = (eProductExemptType)taxExepmt,
                                    Price = price,
                                    IsImported = isImported
                                }
                            }
                        }
                    }
                }
            };

            // Act 

            // Assert 

            Assert.That(sut.Orders, Has.Exactly(1).Items);
            Assert.That(sut.Orders[0].Purchase, Has.Exactly(1)
                .Matches<LineItem>(
                    item => item.Quantity == 1 &&
                            item.ProductDetail.Description == description &&
                            item.ProductDetail.ProductType == (eProductExemptType)taxExepmt &&
                            item.ProductDetail.Price == price &&
                            item.ProductDetail.IsImported == isImported));
        }
    }
}