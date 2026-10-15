using System;
using System.Linq;
using ProductClass.Model;

namespace ProductClass.Data
{
    public class ProductRepository
    {
        public SortableBindingList<Product> Products { get; }

        public ProductRepository()
        {
            Products = new SortableBindingList<Product>(SampleData.GetProducts());
        }

        // 'ignore' lets Edit skip the product being edited
        public bool Exists(string maSP, Product ignore = null)
        {
            return Products.Any(p => p != ignore && p.MaSP != null &&
                                     p.MaSP.Equals(maSP, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Product product) => Products.Add(product);

        public void Remove(Product product) => Products.Remove(product);

        public SortableBindingList<Product> Search(string keyword)
        {
            keyword = (keyword ?? "").Trim().ToLower();
            var result = Products
                .Where(p => p.MaSP != null && p.MaSP.ToLower().Contains(keyword))
                .ToList();
            return new SortableBindingList<Product>(result);
        }
    }
}