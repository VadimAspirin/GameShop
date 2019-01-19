using System.Collections.Generic;

namespace GameShop
{
    public interface IRepository<TTYpe>
    {
        TTYpe Get(int id);
        IEnumerable<TTYpe> GetAll();
    }
}