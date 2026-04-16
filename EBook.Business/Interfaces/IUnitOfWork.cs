using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Interfaces
{
    public interface IUnitOfWork: IDisposable // Implementing IDisposable to ensure proper resource management, especially for database connections.
    {
        IApplicationUserRepository AppUser { get;  }
        ICartRepository Cart { get;  }
        ICategoryRepository Category { get;  }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderProductRepository OrderProduct { get; }

        IProductRepository Product { get; }

        void Save();
    }
}
