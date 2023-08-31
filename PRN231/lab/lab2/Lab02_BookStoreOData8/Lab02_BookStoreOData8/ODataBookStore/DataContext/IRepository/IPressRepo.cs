using Entity;

namespace ODataBookStore.DataContext.IRepository
{
    public interface IPressRepo 
    {
        Task<IEnumerable<Press>> GetAllPress();
    }
}
