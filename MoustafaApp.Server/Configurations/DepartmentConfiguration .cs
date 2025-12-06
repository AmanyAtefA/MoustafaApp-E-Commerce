
namespace MoustafaApp.Server.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
           
            builder.HasData(
                new Department { DepartmentId = 1, DepartmentName = "Men" },
                new Department { DepartmentId = 2, DepartmentName = "Women" },
                new Department { DepartmentId = 3, DepartmentName = "Kids" }
            );
        }
    }
}
