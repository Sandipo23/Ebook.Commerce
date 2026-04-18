
using Ebook.Common.Models.Entities;
using Ebook.Data.Data;
using EBook.Data.Interfaces;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Data.Repositories
{
    public  class ApplicationUserRepository : Repository<ApplicationUser> ,IApplicationUserRepository
    { 
        private  ApplicationDbContext _db; // allows us to access the database context and perform operations on the ApplicationUser entity
                                           // , such as adding, updating, deleting, and retrieving users from the database.
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
    }
}
