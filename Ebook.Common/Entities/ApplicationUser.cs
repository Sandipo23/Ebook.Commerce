using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class ApplicationUser: IdentityUser // this class inherits from IdentityUser,
                                               // which is a part of ASP.NET Core Identity framework.
                                               // It provides properties for user authentication and authorization.  
    {
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string CellPhone { get; set; }
        [NotMapped] // This attribute indicates that the Role property should not be mapped to a database column.
        public string Role { get; set; }
    }
}
