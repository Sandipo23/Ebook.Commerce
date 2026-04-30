using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook.Common.Models.Entities
{
    public class OrderDetails // This class represents the details of an order in an e-commerce application. 
                             // It contains and a reference to the OrderProduct and Product classes, which represents the overall order.
    {
        public int Id { get; set; }
        public int OrderProductId { get; set; }
        public int ProductId { get; set; }
        public int Count {  get; set; }
        public double Price { get; set; }

        [ForeignKey("OrderProductId")]
        public OrderProduct OrderProduct { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
