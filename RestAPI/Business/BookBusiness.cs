using RestAPI.Data.Parsers;
using RestAPI.Data.VOs;
using RestAPI.Models;
using RestAPI.Repository;
using System.Collections.Generic;

namespace RestAPI.Business
{
    public class BookBusiness : IBusiness<BookVO>
    {
        private readonly IRepository<Book> _repository;
        private readonly BookParser _parser = new BookParser();

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public List<BookVO> FindAll()
        {
            var entity = _repository.FindAll();
            return _parser.Parse(entity);
        }

        public BookVO FindById(long id)
        {
            var entity = _repository.FindById(id);
            return _parser.Parse(entity);
        }

        public BookVO Create(BookVO book)
        {
            var entity = _parser.Parse(book);
            entity = _repository.Create(entity);
            return _parser.Parse(entity);
        }

        public BookVO Update(BookVO book)
        {
            var entity = _parser.Parse(book);
            entity = _repository.Update(entity);
            return _parser.Parse(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
