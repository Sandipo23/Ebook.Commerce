using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Data.Interfaces
{
    public interface IUnitOfWork: IDisposable // Implementing IDisposable to ensure proper resource management, especially for database connections.
    {
        IApplicationUserRepository AppUser { get; } // only getter, no setter, to ensure that the repositories are only set through the constructor
                                                    // of the implementing class, promoting immutability and better control over the lifecycle
                                                    // of the repositories.
        ICartRepository Cart { get;  }
        ICategoryRepository Category { get;  }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderProductRepository OrderProduct { get; }

        IProductRepository Product { get; }

        void Save();
    }
}
