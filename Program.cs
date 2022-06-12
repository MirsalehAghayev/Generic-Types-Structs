using System;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop starbucks = new Shop();

            ChooseFromMenu(starbucks);

        }

        static void ChooseFromMenu(Shop shop)
        {
            Console.WriteLine("1. Icki elave edin");
            Console.WriteLine("2. Icki satisi edin");
            Console.WriteLine("3. kassani gor");
            Console.WriteLine("4. Mehsullari gor");
            string chosenMenuOption = Console.ReadLine();

            switch (chosenMenuOption)
            {
                case "1":
                    AddProduct(shop);
                    break;
                case "2":
                    SellProduct(shop);
                    break;
                case "3":
                    Console.WriteLine("Kassa: " + shop.TotalIncome);
                    ChooseFromMenu(shop);
                    break;
                case "4":
                    shop.ShowProducts();
                    ChooseFromMenu(shop);
                    break;
            }
        }

        static void AddProduct(Shop shop)
        {
            Product product;

        type:
            Console.Write("Tipini daxil edin: ");
            string type = Console.ReadLine();
            if (type == "t")
            {
                product = new Tea();
            }
            else if (type == "c")
            {
                product = new Coffee();
            }
            else
            {
                Console.WriteLine("Duzgun tip daxil edin");
                goto type;
            }
            Console.Write("Adini daxil edin: ");
            product.Name = Console.ReadLine();
        priceInput:
            Console.Write("Qiymetini daxil edin: ");
            bool IsParseable = double.TryParse(Console.ReadLine(), out double price);
            if (!IsParseable)
            {
                Console.WriteLine("Qiymeti duzgun daxil etmediniz, yeniden cehd edin");
                goto priceInput;
            }
            product.Price = price;
        countInput:
            Console.Write("Sayini daxil edin: ");
            bool IsCountParseable = int.TryParse(Console.ReadLine(), out int count);
            if (!IsCountParseable)
            {
                Console.WriteLine("Sayini duzgun daxil etmediniz, yeniden cehd edin");
                goto countInput;
            }
            product.Count = count;
            shop.Add(product);
            ChooseFromMenu(shop);
        }

        static void SellProduct(Shop shop)
        {
            Console.Write("Ne isteyirsiniz: ");
            string name = Console.ReadLine();

        SellCountInput:
            Console.Write("Nece eded isteyirsiniz: ");
            bool isSellCountParsable = int.TryParse(Console.ReadLine(), out int sellCount);
            if (!isSellCountParsable)
            {
                Console.WriteLine("Sayini duzgun daxil etmediniz, yeniden cehd edin");
                goto SellCountInput;
            }
            shop.Sell(name, sellCount);

            ChooseFromMenu(shop);
        }
    }

    class Shop
    {
        private Product[] _products;

        public Shop()
        {
            _products = new Product[0];
        }

        public double TotalIncome { get; set; }

        public void Add(Product product)
        {
            foreach (var item in _products)
            {
                if (item.Name == product.Name)
                {
                    item.Count += product.Count;
                    return;
                }
            }

            Array.Resize(ref _products, _products.Length + 1);
            _products[_products.Length - 1] = product;

        }

        public void Sell(string name, int count)
        {
            foreach (var item in _products)
            {
                if (item.Name == name)
                {
                    if (item.Count < count)
                    {
                        if (item.Count != 0)
                        {
                            Console.WriteLine($"Sadece {item.Count} qeder var, bu qeder verehmi? yes or no");
                            string cavab = Console.ReadLine();
                            if (cavab == "yes")
                            {
                                Sell(name, item.Count);
                            }
                        }
                        else if (item.Count == 0)
                        {
                            Console.WriteLine($"Tessuf ki {name} qalmayib");
                        }

                    }
                    else
                    {
                        item.Count -= count;
                        TotalIncome += item.Price * count;
                    }
                }
            }
        }


        public void ShowProducts()
        {
            foreach (var item in _products)
            {
                Console.WriteLine($"{item.Name} : {item.Count} eded , qiymet : {item.Price} manat");
            }
        }




    }

    // latte
    class Product
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }

    class Tea : Product
    {

    }

    class Coffee : Product
    {

    }
}

