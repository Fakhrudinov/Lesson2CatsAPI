using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    /*
    Структура котика
    - кличка nickname
    - вес weigth
    - цвет color
    - наличии прививочного сертификата hasCirtificate
    - названием корма feed
    */

    public class Cat : CatRequest
    {
        [Key]
        public int Id { get; set; }
    }
}
