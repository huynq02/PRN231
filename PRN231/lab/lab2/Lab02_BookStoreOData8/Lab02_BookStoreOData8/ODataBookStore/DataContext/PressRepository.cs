using Entity;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.DataContext.IRepository;

namespace ODataBookStore.DataContext
{
    public class PressRepository : IPressRepo
    {
        private readonly BookStoreContext db;
        public PressRepository(BookStoreContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Press>> GetAllPress()
        {
            return await db.Presses.ToListAsync();
        }
    }
}
