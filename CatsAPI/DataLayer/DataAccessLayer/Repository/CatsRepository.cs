using DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebApiTest.DataAccessLayer.Repository
{
    public class CatsRepository : ICatsRepository
    {
        ApplicationDataContext dbc;
        public CatsRepository(ApplicationDataContext context)
        {
            dbc = context;
        }

        public async Task<List<Cat>> GetAllCats()
        {
            using (dbc)
            {
                var cats = await dbc.Cats.ToListAsync();

                return cats;
            }
        }

        public async Task Create(CatRequest cat)
        {
            //Добавление
            using (dbc)
            {
                //если просто передавать сюда класс Cat то в сваггере появляется лишний параметр Id который не нужен при создании
                Cat newCat = new Cat();
                newCat.NickName = cat.NickName;
                newCat.Weight = cat.Weight;
                newCat.HasCertificate = cat.HasCertificate;
                newCat.FeedBrand = cat.FeedBrand;
                newCat.Color = cat.Color;

                await dbc.Cats.AddAsync(newCat);
                await dbc.SaveChangesAsync();
            }
        }
    }
}
