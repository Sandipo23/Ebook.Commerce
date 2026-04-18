using Ebook.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.ViewModel
{
    public class OrderVM
    {
        public OrderProduct OrderProduct { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; } // This line declares a public property named OrderDetails of type IEnumerable<OrderDetails>.
                                                                    // This property is likely used to hold a collection of order details associated with a specific order product.
                                                                    // The OrderDetails class probably contains information about individual items in the order, such as product details, quantity, and price.
    }
}
