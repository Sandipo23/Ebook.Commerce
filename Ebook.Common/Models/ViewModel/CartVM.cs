
using Ebook.Common.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Common.Models.ViewModel
{
    public class CartVM // This ViewModel is used to represent the data needed for the Cart view,
                        // including the OrderProduct and the list of Cart items.
    { 
        public OrderProduct OrderProduct { get; set; }
        public IEnumerable<Cart> ListCart { get; set; }

    }
}
