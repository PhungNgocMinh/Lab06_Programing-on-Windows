using System.ComponentModel;

namespace ProductClass.Data
{
    public class ProductRepository
    {
        public SortableBindingList<ProductClass.Model.Product> Products { get; }

        public ProductRepository()
        {
            Products = new SortableBindingList<ProductClass.Model.Product>(SampleData.GetProducts());
        }
    }
}