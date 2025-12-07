
namespace MoustafaApp.Server.Repository
{
    public class DepartmentRepo : BaseRepository<Department>, DepartmentIRepo
    {
        public DepartmentRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsWithProducts()
        {
            var departments = await _context.Departments
                .Include(d => d.Products).ThenInclude(p => p.Images)
                .Include(d => d.Products).ThenInclude(p => p.Colors)
                .Include(d => d.Products).ThenInclude(p => p.Sizes).ThenInclude(s => s.Size)
                .ToListAsync();
            return departments;
        }

        public async Task<Department> GetDepartmentByIdWithProducts(int id)
        {
            var department = await _context.Departments
                .Where(d => d.DepartmentId == id)
                .Include(d => d.Products).ThenInclude(p => p.Images)
                .Include(d => d.Products).ThenInclude(p => p.Colors)
                .Include(d => d.Products).ThenInclude(p => p.Sizes).ThenInclude(s => s.Size)
                .FirstOrDefaultAsync();
            return department;
        }
    }
}
