using BusinessObjects;
using BusinessObjects.Dtos;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p)
        {
            ProductDAO.DeleteProduct(p);
        }

        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }

        public Product GetProductById(int id)
        {
            return ProductDAO.FindProductById(id);
        }

        public List<Product> GetProductByName(string name)
        {
            return ProductDAO.FindProductByName(name);
        }

        //public List<Product> GetProductBySort(string sort)
        //{
        //    return ProductDAO.FindProductBySort(sort);
        //}

        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts();
        }

        public List<Product> GetProductsByPaging(int? currenPage, int? pageSize)
        {
            return ProductDAO.GetProductPaging(currenPage, pageSize);
        }

        public void SaveProduct(CreateProductDto p)
        {
            ProductDAO.SaveProduct(p);
        }

        public void UpdateProduct(int id,UpdateProductDto p)
        {
            ProductDAO.UpdateProduct(id,p);
        }
    }
}
