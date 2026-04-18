using Ebook.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Data.Interfaces
{
    public interface IOrderProductRepository: IRepository<OrderProduct>
    {
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null); // This method allows updating the order status and optionally the payment status
                                                                                     // for a specific order product. The 'id' parameter identifies the order product to be updated,
                                                                                      // 'orderStatus' specifies the new status of the order, and 'paymentStatus' can be used to update
                                                                                     // the payment status if provided.
    }
}
