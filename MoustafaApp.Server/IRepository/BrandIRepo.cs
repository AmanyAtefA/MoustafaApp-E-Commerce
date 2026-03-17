using MoustafaApp.Server.Dtos.BrandDtos;

namespace MoustafaApp.Server.IRepository
{
    public interface BrandIRepo : IBaseRepository<Brand>
    {
        Task<IEnumerable<BrandDto>> GetAllBrands();
        Task<BrandDto?> GetBrandById(int id);
    }
}
