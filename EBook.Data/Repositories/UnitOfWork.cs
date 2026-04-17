using Ebook.Data.Data;
using EBook.Data.Interfaces;

namespace EBook.Data.Repositories
{
    public  class UnitOfWork: IUnitOfWork // this class is responsible for managing the repositories and ensuring that all database operations
                                          // are performed within a single transaction. It implements the IUnitOfWork interface,
                                          // which defines the contract for the unit of work pattern. 
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }
        public IApplicationUserRepository AppUser =>   new ApplicationUserRepository(_db); // This property provides access to the ApplicationUserRepository,
                                                                                           // which is responsible for handling database operations related to application users.
        public ICartRepository Cart  => new CartRepository(_db);
        public ICategoryRepository Category => new CategoryRepository(_db);
        public IOrderDetailsRepository OrderDetails => new OrderDetailsRepository(_db);
        public IOrderProductRepository OrderProduct => new OrderProductRepository(_db);
        public IProductRepository Product => new ProductRepository(_db);
  
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
 