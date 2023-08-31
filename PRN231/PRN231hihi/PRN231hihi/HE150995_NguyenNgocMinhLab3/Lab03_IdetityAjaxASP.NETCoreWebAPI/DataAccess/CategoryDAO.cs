using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategoris = new List<Category>();
            try
            {
                using(var context = new MyDBContext())
                {
                    listCategoris = context.Categories.ToList();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCategoris;
        }
    }
}
