namespace BookShop.WebApi.Infrastructure.ModelsMapping
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}
