using System;

namespace ShopApp
{
    public class Product {
        protected string name;
        protected double price;
        public Product(string name, double price) { this.name = name; this.price = price; }
        public virtual void Display() => Console.WriteLine($"Товар: {name}, Цена: {price}");
    }

    public class Electronics : Product {
        public int Warranty { get; set; }
        public Electronics(string n, double p, int w) : base(n, p) => Warranty = w;
        public override void Display() => Console.WriteLine($"[Электроника] {name}, Гарантия: {Warranty} мес.");
    }

    public class Clothing : Product {
        public string Size { get; set; }
        public Clothing(string n, double p, string s) : base(n, p) => Size = s;
        public override void Display() => Console.WriteLine($"[Одежда] {name}, Размер: {Size}");
    }

    class Program {
        static void Main() {
            Product[] inventory = { new Electronics("ТВ", 50000, 12), new Clothing("Худи", 3000, "M") };
            foreach (var item in inventory) item.Display();
            Console.ReadKey();
        }
    }
}
