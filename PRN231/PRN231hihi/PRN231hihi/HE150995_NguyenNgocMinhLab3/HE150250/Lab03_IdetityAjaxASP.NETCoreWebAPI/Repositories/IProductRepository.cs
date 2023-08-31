using BusinessObjects;
using BusinessObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        void SaveProduct(CreateProductDto p);
        Product GetProductById(int id);
        List<Product> GetProductByName(string name);
        //List<Product> GetProductBySort(string sort);
        void DeleteProduct(Product p);
        void UpdateProduct(int id, UpdateProductDto p);
        List<Category> GetCategories();
        List<Product> GetProducts();
        List<Product> GetProductsByPaging(int? currentPage, int? pageSize);
    }
}
