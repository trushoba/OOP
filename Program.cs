using System;
using System.Collections.Generic;

namespace ShopApp
{
    public class Warehouse<T> where T : Product
    {
        private List<T> items = new List<T>();
        public static int Total = 0;
        public void Add(T item) { items.Add(item); Total++; }
        public void Show() { foreach (var i in items) i.Display(); }
    }

    public class Product {
        protected string name; public double price;
        public Product(string n, double p) { name = n; price = p; }
        public virtual void Display() => Console.WriteLine($"{name} - {price} р.");
    }

    public class Electronics : Product {
        public Electronics(string n, double p) : base(n, p) { }
        public override void Display() => Console.WriteLine($"[Электроника] {name}");
    }

    class Program {
        static void Main() {
            Warehouse<Product> w = new Warehouse<Product>();
            w.Add(new Electronics("Наушники", 5000));
            w.Show();
            Console.ReadKey();
        }
    }
}
