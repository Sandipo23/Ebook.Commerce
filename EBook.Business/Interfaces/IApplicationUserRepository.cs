using EBook.Common.Entities;
using EBook.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Interfaces
{
    public interface IApplicationUserRepository: IRepository<ApplicationUser>
    {
    }
}
