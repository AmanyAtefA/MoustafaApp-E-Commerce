public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
      
        builder.HasData(
            new Size { SizeId = 1, SizeName = "S" },
            new Size { SizeId = 2, SizeName = "M" },
            new Size { SizeId = 3, SizeName = "L" },
            new Size { SizeId = 4, SizeName = "XL" }
        );
    }
}
