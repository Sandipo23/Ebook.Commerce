
using Ebook.Common.Models.Entities;
using System.Collections.Generic;

namespace EBook.Business.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<IEnumerable<OrderProduct>> GetUserOrdersAsync();
        Task CancelOrderAsync(int id);
    }
}
