using RestAPI.Data.Parsers;
using RestAPI.Data.VOs;
using RestAPI.Models;
using RestAPI.Repository;
using System.Collections.Generic;

namespace RestAPI.Business
{
    public class PersonBusiness : IBusiness<PersonVO>
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonParser _parser = new PersonParser();

        public PersonBusiness(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public List<PersonVO> FindAll()
        {
            var entity = _repository.FindAll();
            return _parser.Parse(entity);
        }

        public PersonVO FindById(long id)
        {
            var entity = _repository.FindById(id);
            return _parser.Parse(entity);
        }

        public PersonVO Create(PersonVO person)
        {
            var entity = _parser.Parse(person);
            entity = _repository.Create(entity);
            return _parser.Parse(entity);
        }

        public PersonVO Update(PersonVO person)
        {
            var entity = _parser.Parse(person);
            entity = _repository.Update(entity);
            return _parser.Parse(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
