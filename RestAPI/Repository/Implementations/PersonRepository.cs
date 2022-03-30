using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAPI.Repository.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private MyDbContext _context;

        public PersonRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }

            return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id))
                return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            _context.Entry(result).CurrentValues.SetValues(person);
            _context.SaveChanges();

            return person;
        }

        public void Delete(long id)
        {
            if (!Exists(id))
                return;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            _context.Persons.Remove(result);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
