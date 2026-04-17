
using EBook.Common.Entities;
using System.Collections.Generic;

namespace EBook.Business.Interfaces
{
    public interface ICustomerOrderService
    {
        IEnumerable<OrderProduct> GetUserOrdersAsync();
        void CancelOrder(int id);
    }
}
