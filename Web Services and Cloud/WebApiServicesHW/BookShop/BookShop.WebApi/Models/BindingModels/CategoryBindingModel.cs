namespace BookShop.WebApi.Models
{
    using BookShop.Models;
    using Infrastructure.ModelsMapping;

    public class CategoryBindingModel: IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}