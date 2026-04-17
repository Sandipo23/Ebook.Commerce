


using EBook.Business.ViewModel;
using EBook.Common.Entities;
using System.Collections.Generic;

namespace EBook.Business.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(OrderProduct order);
        IEnumerable<OrderProduct> GetAll();
        OrderVM Details(int id);
        OrderVM Delivered(OrderVM orderVM);
        OrderVM CancelOrder(OrderVM orderVM);
        OrderVM UpdateOrderDetails(OrderVM orderVM);
        void DeleteOrder(int? id);
    }
}
