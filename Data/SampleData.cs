using System;
using System.Collections.Generic;
using ProductClass.Model;

namespace ProductClass.Data
{
    public static class SampleData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product("SP001", "Cà phê Sữa Đá", "Đồ uống", 29000, 50, DateTime.Now, true, @"Images\caphe.jpg"),
                new Product("SP002", "Trà Đào Cam Sả", "Đồ uống", 39000, 30, DateTime.Now, true, @"Images\tradao.jpg"),
                new Product("SP003", "Bánh Tiramisu", "Bánh ngọt", 45000, 15, DateTime.Now, false, @"Images\tiramisu.jpg"),
                new Product("SP004", "Cà phê Đen", "Đồ uống", 20000, 40, DateTime.Now, true, @"Images\capheden.jpg"),
                new Product("SP005", "Matcha Latte (Nho)", "Đồ uống", 40000, 25, DateTime.Now, true, @"Images\matcha_nho.jpg"),
                new Product("SP006", "Matcha Latte (Lớn)", "Đồ uống", 70000, 20, DateTime.Now, true, @"Images\matcha_lon.jpg"),
                new Product("SP007", "Americano", "Đồ uống", 30000, 35, DateTime.Now, true, @"Images\americano.jpg"),
                new Product("SP008", "Bánh Bông Lan Kem Tươi (Nhỏ)", "Bánh ngọt", 38000, 10, DateTime.Now, true, @"Images\bonglan_nho.jpg"),
                new Product("SP009", "Bánh Bông Lan Kem Tươi (Lớn)", "Bánh ngọt", 180000, 5, DateTime.Now, true, @"Images\bonglan_lon.jpg"),
                new Product("SP010", "Bánh Flan", "Bánh ngọt", 15000, 50, DateTime.Now, true, @"Images\flan.jpg")
            };
        }
    }
}
