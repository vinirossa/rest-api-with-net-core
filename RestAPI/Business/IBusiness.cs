using System.Collections.Generic;

namespace RestAPI.Business
{
    public interface IBusiness<T>
    {
        T Create(T obj);
        T FindById(long id);
        List<T> FindAll();
        T Update(T obj);
        void Delete(long id);
    }
}
