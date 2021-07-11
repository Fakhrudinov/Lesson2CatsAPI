using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiTest.DataAccessLayer.Repository;
/*
По домашке на второй урок нужно будет сделать rest сервис. 
В сервис нужно будет подключить DI/EF (Postgre)/ Swagger.
Сервис должен иметь два ендпойнта для чтения данных из базы и создания этих данных. 
В EF нужно будет сделать миграцию для создания структуры в которой будут хранится данные. 
Приложение должно быть правильно структурировано. 
Разбито на зоны ответственности - слои (Cleint Layer (API) / Business Layer / Data Access Layer) 
Сами данные не представляют какую то ценность или значение просто будем хранить и выводить котиков. 
Структура котика 
- кличка nickname
- вес weigth
- цвет color
- наличии прививочного сертификата hasCirtificate
- названием корма feed
Api создания
POST /kittens
{ json body goes here }
Api чтения
GET /kittens
Возвращает массив котиков 
аля [{}]
*/

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        public CatsController(
            ILogger<CatsController> logger,
            ICatsRepository repository) //,            ApplicationDataContext context
        {
            _logger = logger;
            _repository = repository;
            //db = context;
        }

        private readonly ILogger<CatsController> _logger;
        private ICatsRepository _repository;
        //private ApplicationDataContext db;


        /// <summary>
        /// Запросить всех имеющихся котиков
        /// </summary>
        /// <returns></returns>
        [HttpGet("kittens")]
        public async Task<ActionResult<List<Cat>>> GetAllCats()
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: GET / kittens/");

            var kitties = await _repository.GetAllCats();

            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: GET / kittens/ ОК");
            return Ok(kitties);
        }

        /// <summary>
        /// Добавить нового кота в базу данных котиков
        /// </summary>
        /// <param name="cat">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost("kitten")]
        public async Task<OkResult> RegisterAgent([FromBody] CatRequest cat)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: POST / kitten/ {cat.NickName}");
            await _repository.Create(cat);

            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: POST / kitten/ Ok");
            return Ok();
        }
    }
}
