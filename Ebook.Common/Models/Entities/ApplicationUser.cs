using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook.Common.Models.Entities
{
    public class ApplicationUser: IdentityUser // this class inherits from IdentityUser,
                                               // which is a part of ASP.NET Core Identity framework.
                                               // It provides properties for user authentication and authorization.
                                               // In our databse we do not see these properties because they are inherited from the IdentityUser class,
                                               // which is part of the ASP.NET Core Identity framework. The IdentityUser class includes properties such as UserName, Email, PasswordHash, SecurityStamp, and more,
                                               // which are used for managing user authentication and authorization. By inheriting from IdentityUser, the ApplicationUser class can utilize
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
