using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Common.Models.ViewModel
{
    public class RegisterViewModel  //This class is used to represent the data that is needed to register a new user.
                                    // The properties are decorated with data annotations to specify validation rules for the input data.
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string CellPhone { get; set; }
    }
}
