using System.Collections.Generic;

namespace SilverlightAsyncRestWcf.Services
{
    public interface IRepository<T> where T : class
    {
        T GetById(string id);
        IList<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
