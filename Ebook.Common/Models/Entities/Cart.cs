using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebook.Common.Models.Entities
{
    public class Cart // This class represents a shopping cart item in an e-commerce application.
                      // It contains properties to store information about the cart item,
                      // such as the user who added it, the product, quantity, and price.
                      // The class also includes navigation properties to establish relationships
                      // with the ApplicationUser and Product entities.
    {
        public Cart() // This constructor initializes the Count property to 1 when a new Cart object is created.
                      // This means that when a user adds a product to their cart, it will start with a
                      // quantity of 1 by default.
        {
            Count = 1;
        }

        [Key]
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        [ForeignKey("AppUserId")] // This attribute specifies that the AppUserId property is a foreign key that
                                  // references the primary key of the ApplicationUser entity.
        public ApplicationUser AppUser { get; set; }
        [ForeignKey("ProductId")] // This attribute specifies that the ProductId property is a foreign key that
                                  // references the primary key of the Product entity.
        public Product Product { get; set; }

         
    }
}
