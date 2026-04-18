


using EBook.Business.ViewModel;
using EBook.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBook.Business.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderProduct order);
        Task<IEnumerable<OrderProduct>> GetAllAsync();
        Task<OrderVM> DetailsAsync(int id);
        Task<OrderVM> DeliveredAsync(OrderVM orderVM);
        Task<OrderVM> CancelOrderAsync(OrderVM orderVM);
        Task<OrderVM> UpdateOrderDetailsAsync(OrderVM orderVM);
        Task DeleteOrderAsync(int? id);
    }
}
