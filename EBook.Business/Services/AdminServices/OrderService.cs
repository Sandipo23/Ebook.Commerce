using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Business.ViewModel;
using EBook.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Services.AdminServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderVM OrderVM { get; set; } // This line declares a public property named OrderVM of type OrderVM.
                                             // This property is likely used to hold the details of an order,
                                             // including the order product and its associated order details.
                                             // The OrderVM class is probably a view model that encapsulates the
                                             // data needed for displaying order information in the user interface.
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task CreateOrderAsync(OrderProduct order)
        {
            await _unitOfWork.OrderProduct.AddAsync(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<OrderProduct>> GetAllAsync()
        {
            var orderList = await _unitOfWork.OrderProduct.GetAllAsync(o => o.OrderStatus != "Delivered");

            return orderList;
        }

        public async Task<OrderVM> DetailsAsync(int id)
        {
            OrderVM = new OrderVM()
            {
                OrderProduct = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(o => o.Id == id, "AppUser"), // This line retrieves the first OrderProduct entity from the database that matches the specified id.
                                                                                                                  // The "AppUser" parameter indicates that the related AppUser entity should also be included in the query result.
                OrderDetails = await _unitOfWork.OrderDetails.GetAllAsync(od => od.OrderProductId == id, includeProperties: "Product") // This line retrieves all OrderDetails entities from the database
                                                                                                                                       // that are associated with the specified OrderProduct id.
            };
            return OrderVM;
        }


        public async Task<OrderVM> DeliveredAsync(OrderVM orderVM)
        {
            var orderProduct = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(op => op.Id == orderVM.OrderProduct.Id);

            orderProduct.OrderStatus = "Delivered";
            await _unitOfWork.OrderProduct.UpdateAsync(orderProduct);
            await _unitOfWork.SaveAsync();
            return orderVM;
        }



        public async Task<OrderVM> CancelOrderAsync(OrderVM orderVM)
        {
            var orderProduct = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(o => o.Id == orderVM.OrderProduct.Id); // This line retrieves the first OrderProduct entity from the database that matches the specified id from the OrderVM object.
                                                                                                                            // The id is accessed through orderVM.OrderProduct.Id,
                                                                                                                            // which means it is looking for the OrderProduct associated with the provided OrderVM.,
                                                                                                                            // we work on actual order product not the view model, we need to update the order product in the database,
                                                                                                                            // so we need to retrieve it first using the id from the view model.

            orderProduct.OrderStatus = "Cancel";
            await _unitOfWork.OrderProduct.UpdateAsync(orderProduct);
            await _unitOfWork.SaveAsync();
            return OrderVM;
        }


        public async Task<OrderVM> UpdateOrderDetailsAsync(OrderVM orderVM)
        {
            var orderDetailsFromDb = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(o => o.Id == orderVM.OrderProduct.Id);
            orderDetailsFromDb.Name = orderVM.OrderProduct.Name;
            orderDetailsFromDb.Address = orderVM.OrderProduct.Address;
            orderDetailsFromDb.CellPhone = orderVM.OrderProduct.CellPhone;
            orderDetailsFromDb.PostalCode = orderVM.OrderProduct.PostalCode;
         
            await _unitOfWork.OrderProduct.UpdateAsync(orderDetailsFromDb);
            await _unitOfWork.SaveAsync();
            return orderVM;
        }
        public async Task DeleteOrderAsync(int? id)
        {
            var order = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return;
            }
            await _unitOfWork.OrderProduct.RemoveAsync(order);
            await _unitOfWork.SaveAsync();
        } 

    }
}
