namespace BookShop.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BookShop.Models;

    using Infrastructure.ModelsMapping;

    public class PutBookBindingModel : IMapFrom<Book>
    {
        [MinLength(1), MaxLength(50)]
        [Required]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public EditionType EditionType { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int AuthorId { get; set; }
    }
}