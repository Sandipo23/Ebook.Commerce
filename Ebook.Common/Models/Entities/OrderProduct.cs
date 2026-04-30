   using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook.Common.Models.Entities
{
    public class OrderProduct  // this class represents an order placed by a user,
                               // containing details such as the order date, price, status, and associated user information.
                               // It also includes a navigation property to the ApplicationUser entity,
                               // allowing access to the user's details who placed the order.
    {
        public int Id { get; set; } 
        public string AppUserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double  OrderPrice { get; set; }
        public string OrderStatus { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
        [ForeignKey("AppUserId")]
        public ApplicationUser AppUser { get; set; }
    }
}
