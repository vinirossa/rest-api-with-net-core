using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAPI.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        public GenericRepository(MyDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        private MyDbContext _context;
        private DbSet<T> _dataset;

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(long id)
        {
            return _dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T obj)
        {
            try
            {
                _context.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return obj;
        }

        public T Update(T obj)
        {
            if (!Exists(obj.Id))
                return null;

            var result = _dataset.SingleOrDefault(p => p.Id.Equals(obj.Id));

            _context.Entry(result).CurrentValues.SetValues(obj);
            _context.SaveChanges();

            return result;
        }

        public void Delete(long id)
        {
            if (!Exists(id))
                return;

            var result = _dataset.SingleOrDefault(p => p.Id.Equals(id));

            _dataset.Remove(result);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _dataset.Any(p => p.Id.Equals(id));
        }
    }
}
