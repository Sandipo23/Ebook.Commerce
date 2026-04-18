
using Ebook.Common.Models.Entities;
using Ebook.Data.Data;
using EBook.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Data.Repositories
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        private ApplicationDbContext _db;
        public OrderProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null) // this method allows updating the order status and optionally the payment status for a specific order product.
                                                                                           // The 'id' parameter identifies the order product to be updated,
                                                                                           // 'orderStatus' specifies the new status of the order, and
                                                                                           // 'paymentStatus' can be used to update the payment status if provided.
        {
            throw new NotImplementedException();
        }
    }
}
