using BussinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class PressService : IPressService
    {
        private readonly IPressRepository _pressRepository;

        public PressService(IPressRepository pressRepository)
        {
            _pressRepository = pressRepository;
        }
        public Task<DbSet<Press>> GetAllAsync()
        {
           return _pressRepository.GetAllAsync();
        }
    }
}
