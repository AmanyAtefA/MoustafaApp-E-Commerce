namespace MoustafaApp.Server.IRepository
{
    public interface DepartmentIRepo: IBaseRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllDepartmentsWithProducts();
        Task<Department> GetDepartmentByIdWithProducts(int id);
    }
}
