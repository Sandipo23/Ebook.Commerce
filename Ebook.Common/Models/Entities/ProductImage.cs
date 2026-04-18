using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ebook.Common.Models.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever] // This attribute is used to prevent validation of the Product property when validating the ProductImage model.
                        // This is useful in scenarios where you want to avoid circular references or when the Product property is not required for certain operations.
        public Product Product { get; set; } // This property represents the navigation property to the Product entity.
                                             // It allows you to access the related Product information when working with a ProductImage instance.
    }
}
