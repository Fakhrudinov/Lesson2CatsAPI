using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class CatRequest
    {
        [Required]
        public string NickName { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }
        public bool HasCertificate { get; set; }
        public string FeedBrand { get; set; }
    }
}
