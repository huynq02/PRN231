using BusinessObjects.Product
namespace IProductRepository
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
    }
}