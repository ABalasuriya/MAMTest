using NUnit.Framework;
using ReceiptPrinter.Common;
using TaxServices;

namespace NUnitReceiptPrinter.Testing
{
    [TestFixture]
    public class ReceiptPrinterTest
    {
        private TheReceiptPrinter gSut;
        
        [SetUp]
        public void Setup()
        {
            gSut = new TheReceiptPrinter(new TaxServiceOne(10m, 5m))
            {
                Orders = {
                    new Order {
                        Purchase = {
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "Book",
                                    ProductType = eProductExemptType.book,
                                    Price = 12.49m,
                                    IsImported = false
                                }
                            },
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "Music CD",
                                    ProductType = eProductExemptType.none,
                                    Price = 14.99m,
                                    IsImported = false
                                }
                            },
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "Chocolate bar",
                                    ProductType = eProductExemptType.food,
                                    Price = 0.85m,
                                    IsImported = false
                                }
                            }
                        }
                    },
                    new Order {
                        Purchase = {
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "box of chocolates",
                                    ProductType = eProductExemptType.food,
                                    Price = 10.00m,
                                    IsImported = true
                                }
                            },
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "bottle of perfume",
                                    ProductType = eProductExemptType.none,
                                    Price = 47.50m,
                                    IsImported = true
                                }
                            }
                        }
                    },
                    new Order {
                        Purchase = {
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "bottle of perfume",
                                    ProductType = eProductExemptType.none,
                                    Price = 27.99m,
                                    IsImported = true
                                }
                            },
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "bottle of perfume",
                                    ProductType = eProductExemptType.none,
                                    Price = 18.99m,
                                    IsImported = false
                                }
                            },
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "Packet of paracetamol",
                                    ProductType = eProductExemptType.medicalPoduct,
                                    Price = 9.75m,
                                    IsImported = false
                                }
                            },
                            new LineItem {
                                Quantity = 1,
                                ProductDetail = new Product {
                                    Description = "box of chocolates",
                                    ProductType = eProductExemptType.food,
                                    Price = 11.25m,
                                    IsImported = true
                                }
                            }
                        }
                    }
                }
            };
        }

        [Test]
        [Category("ReceiptPrinter Testing")]
        [TestCase(1, "Book", 1, 12.49, false)]
        public void Should_have_single_item_in_order_collection(int quantity, 
            string description,
            int taxExempt,
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
                                    ProductType = (eProductExemptType)taxExempt,
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
                            item.ProductDetail.ProductType == (eProductExemptType)taxExempt &&
                            item.ProductDetail.Price == price &&
                            item.ProductDetail.IsImported == isImported));
        }

        [Test]
        [Category("ReceiptPrinter Testing")]
        [TestCase(1, "Book", 1, 12.49, false, 12.49)]   // Book not imported
        [TestCase(1, "Music CD", 0, 14.99, false, 16.49)]   // Music CD not imported 14.99
        [TestCase(1, "chocolate bar", 2, 0.85, false, 0.85)]    // Book not imported
        [TestCase(1, "imported box of chocolates", 2, 10.00, true, 10.50)]  //Imported box of chocolates at 10.00 
        [TestCase(1, "Imported bottle of perfume", 0, 47.50, true, 54.65)]  //Imported bottle of perfume at 47.50
        [TestCase(1, "Bottle of perfume", 0, 18.99, false, 20.89)]  //Bottle of perfume at 18.99
		[TestCase(1, "Packet of paracetamol", 3, 9.75, false, 9.75)]  //Packet of paracetamol at 9.75 
        [TestCase(1, "Box of imported chocolates", 2, 11.25, true, 11.85)]  //Box of imported chocolates at 11.25 
        public void Should_have_correct_lineValues(int quantity,
            string description,
            int taxExempt,
            decimal price,
            bool isImported,
            decimal lineValueToAssert)
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
                                    ProductType = (eProductExemptType)taxExempt,
                                    Price = price,
                                    IsImported = isImported
                                }
                            }
                        }
                    }
                }
            };

            // Act 
            var result = sut.DisplayReceipts();

            // Assert 

            Assert.That(sut.Orders[0].Purchase[0].ValuePerLine, Is.EqualTo(lineValueToAssert ));

        }

        [Test]
        [Category("ReceiptPrinter Testing")]
        public void Should_have_correct_values_in_order_collection()
        {
            // Arrange 

            //Create SUT in setup

            // Act 
            var result = gSut.DisplayReceipts();

            // Assert 

            Assert.That(gSut.Orders, Has.Exactly(3).Items);
            Assert.That(gSut.Orders[0].Purchase, Has.Exactly(3).Items);
            Assert.That(gSut.Orders[0].TotalTaxesPrint, Is.EqualTo("Sales Taxes: 1.50\n"));
            Assert.That(gSut.Orders[0].TotalPrint, Is.EqualTo("Total: 29.83\n\n"));
            Assert.That(gSut.Orders[1].Purchase, Has.Exactly(2).Items);
            Assert.That(gSut.Orders[1].TotalTaxesPrint, Is.EqualTo("Sales Taxes: 7.65\n"));
            Assert.That(gSut.Orders[1].TotalPrint, Is.EqualTo("Total: 65.15\n\n"));
            Assert.That(gSut.Orders[2].Purchase, Has.Exactly(4).Items);
            Assert.That(gSut.Orders[2].TotalTaxesPrint, Is.EqualTo("Sales Taxes: 6.70\n"));
            Assert.That(gSut.Orders[2].TotalPrint, Is.EqualTo("Total: 74.68\n\n"));

        }
    }
}