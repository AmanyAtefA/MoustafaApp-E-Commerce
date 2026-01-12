

namespace moustafapp.Server.GenericOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public UnitOfWork(AppDbContext context, IMapper mapper,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager )
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            
            UserManager = userManager;
            RoleManager = roleManager;

            Products = new ProductRepo(_context,_mapper);
            Categories = new CategoryRepo(_context);
            Departments = new DepartmentRepo(_context);
            Carts     = new CartRepo(_context);
            CartItems = new BaseRepository<CartItem>(_context);
            ProductImage = new BaseRepository<ProductImage>(_context);
            Users = new BaseRepository<ApplicationUser>(_context);
            Brands = new BaseRepository<Brand>(_context);
            ProductColors = new BaseRepository<ProductColor>(_context);
            ProductSizes = new BaseRepository<ProductSize>(_context);
            Reviews = new BaseRepository<Review>(_context);

        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ProductIRepo Products { get; private set; } 
        public CategoryIRepo Categories { get; private set; }
        public DepartmentIRepo Departments { get; private set; }
        public CartIRepo Carts { get; private set; }
        public IBaseRepository<ProductImage> ProductImage { get; private set; }  
    
        public IBaseRepository <CartItem> CartItems { get; private set; }
        public IBaseRepository <ApplicationUser> Users { get; private set; }
        public IBaseRepository <Brand> Brands { get; private set; }
        public IBaseRepository <ProductColor> ProductColors { get; private set; }
        public IBaseRepository <ProductSize> ProductSizes { get; private set; }
        public IBaseRepository <Review> Reviews { get; private set; }

        public int CommitChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
