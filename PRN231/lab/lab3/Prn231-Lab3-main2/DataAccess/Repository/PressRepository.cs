using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PressRepository : IPressRepository
    {
        private readonly MyDBContext db;

        public PressRepository(MyDBContext myDb)
        {
            db = myDb;
        }
        public Task<DbSet<Press>> GetAllAsync()
        {
            return Task.FromResult(db.Presss);
        }
    }
}
