using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook.Common.Models.Entities
{
    public class Category// This class represents a category of products in an e-commerce application.
                         // It contains properties to store information about the category,
                         // such as its name and a unique identifier. 
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
