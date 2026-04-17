using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Data.Interfaces
{
    public interface IRepository<T> where T : class // T represents any class, it is a generic type parameter,
                                                    // we can use it to create a repository for any type of entity,
                                                    // such as Product, Category, Order, etc.
    {
        void Add(T entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,  string? includeProperties = null);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
