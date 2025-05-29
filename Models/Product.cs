using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategoryApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
