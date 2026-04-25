using System;

namespace ShopApp
{
    public class Product
    {
        // Приватные поля (Инкапсуляция)
        private string article;
        private string name;
        private double price;
        private int stockCount;

        // Публичные свойства
        public string Name { get => name; set => name = value; }
        public double Price 
        { 
            get => price; 
            set { if (value >= 0) price = value; } 
        }
        public int StockCount { get => stockCount; }

        public Product(string article, string name, double price, int count)
        {
            this.article = article;
            this.name = name;
            this.Price = price;
            this.stockCount = count;
        }

        // Методы
        public void Sell(int amount)
        {
            if (amount <= stockCount)
            {
                stockCount -= amount;
                Console.WriteLine($"Продано {amount} шт. Остаток: {stockCount}");
            }
            else Console.WriteLine("Недостаточно товара на складе!");
        }

        public void Restock(int amount)
        {
            stockCount += amount;
            Console.WriteLine($"Поставка: +{amount} шт. Итого: {stockCount}");
        }

        public void PrintInfo() => Console.WriteLine($"[{article}] {name} - {price} руб. (В наличии: {stockCount})");
    }

    class Program
    {
        static void Main()
        {
            Product p = new Product("A100", "Ноутбук", 50000, 10);
            p.PrintInfo();
            p.Sell(3);
            p.Restock(5);
            Console.ReadKey();
        }
    }
}
