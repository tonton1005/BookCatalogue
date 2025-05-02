using System.ComponentModel.DataAnnotations;

namespace BookCatalogue.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(80)]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Published Date"), DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [Range(0, 10000), DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}