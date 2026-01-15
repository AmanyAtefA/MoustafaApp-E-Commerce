public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
      
        builder.HasData(
            new Size { SizeId = 1, SizeName = "Small" },
            new Size { SizeId = 2, SizeName = "Medium" },
            new Size { SizeId = 3, SizeName = "Large" },
            new Size { SizeId = 4, SizeName = "X-Large" }
        );
    }
}
