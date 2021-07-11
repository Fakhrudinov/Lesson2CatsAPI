using DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTest.DataAccessLayer.Repository
{
    public interface ICatsRepository
    {
        Task<List<Cat>> GetAllCats();
        Task Create(CatRequest cat);
    }
}