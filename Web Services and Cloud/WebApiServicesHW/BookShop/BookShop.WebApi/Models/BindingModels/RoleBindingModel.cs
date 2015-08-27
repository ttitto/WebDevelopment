namespace BookShop.WebApi.Models
{
    using BookShop.WebApi.Infrastructure.ModelsMapping;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleBindingModel : IMapFrom<IdentityRole>
    {
        public string Name { get; set; }
    }
}