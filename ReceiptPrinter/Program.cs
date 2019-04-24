using System;
using ReceiptPrinter.Common;
using TaxServices;

namespace ReceiptPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            var receiptPrinter = new TheReceiptPrinter(new TaxServiceOne(10m, 5m))
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

            Console.WriteLine(receiptPrinter.DisplayReceipts());
            Console.ReadLine();
        }

    }
}
