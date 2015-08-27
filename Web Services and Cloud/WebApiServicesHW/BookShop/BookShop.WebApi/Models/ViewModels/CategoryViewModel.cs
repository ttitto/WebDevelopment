namespace BookShop.WebApi.Models
{
    using BookShop.Models;
    using Infrastructure.ModelsMapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}