using BusinessObjects;
using BusinessObjects.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProductPaging(int? currenPage, int? pageSize)
        {
            int skipCount = ((currenPage ?? 1) - 1) * (pageSize ?? 5);
            var products = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    products = context.Products
                        .Skip(skipCount)
                        .Take(pageSize ?? 5)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listProducts = context.Products.ToList();
                }
                using (var context = new MyDBContext())
                {
                    foreach (var item in listProducts)
                    {
                        item.Category = context.Categories.FirstOrDefault(x => x.CategoryId == item.CategoryId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        public static Product FindProductById(int proId)
        {
            Product p = new Product();
            try
            {
                using (var context = new MyDBContext())
                {
                    p = context.Products.SingleOrDefault(x => x.ProductId == proId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static List<Product> FindProductByName(string name)
        {
            var listProducts = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listProducts = context.Products.Where(p => p.ProductName == name).ToList();
                }
                using (var context = new MyDBContext())
                {
                    foreach (var item in listProducts)
                    {
                        item.Category = context.Categories.FirstOrDefault(x => x.CategoryId == item.CategoryId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        public static void SaveProduct(CreateProductDto pDto)
        {
            try
            {
                Product p = new Product();
                p.ProductName = pDto.ProductName;
                p.UnitPrice = pDto.UnitPrice;
                p.UnitsInStock = pDto.UnitsInStock;
                p.CategoryId = pDto.CategoryId;
                using (var context = new MyDBContext())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateProduct(int id, UpdateProductDto pDto)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
                    if (product == null)
                    {
                        throw new Exception();
                    }
                    product.ProductName = pDto.ProductName;
                    product.UnitPrice = pDto.UnitPrice;
                    product.UnitsInStock = pDto.UnitsInStock;
                    product.CategoryId = pDto.CategoryId;
                    context.Entry<Product>(product).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var pro = context.Products.SingleOrDefault(
                        c => c.ProductId == p.ProductId);
                    context.Products.Remove(pro);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
