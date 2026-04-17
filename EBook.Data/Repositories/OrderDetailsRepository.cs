
using Ebook.Data.Data;
using EBook.Common.Entities;
using EBook.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Data.Repositories
{
    public class OrderDetailsRepository: Repository<OrderDetails>, IOrderDetailsRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
