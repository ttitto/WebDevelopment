using BookShop.Models;
using BookShop.WebApi.Infrastructure.ModelsMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;

namespace BookShop.WebApi.Models
{
    public class PostBookBindingModel : IMapFrom<Book>
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
        public string CategoriesNames { get; set; }
    }
}