using RestAPI.Models;
using System.Collections.Generic;

namespace RestAPI.Repository
{
    public interface IRepository<T> where T : Entity
    {
        T Create(T obj);
        T FindById(long id);
        List<T> FindAll();
        T Update(T obj);
        void Delete(long id);
        bool Exists(long id);
    }
}
