using System.ComponentModel;
using ProductClass.Data;

namespace ProductClass.Data
{
    public class ProductRepository
    {
        public BindingList<ProductClass.Model.Product> Products { get; }

        public ProductRepository()
        {
            Products = new BindingList<ProductClass.Model.Product>(SampleData.GetProducts());
        }
    }
}