using System.Collections.Generic;
using BLL.DataAccess;
using BLL.Entities;

namespace BLL.Services
{
    public class BookService
    {
        public readonly IRepository<Book> _repository;
        public readonly IUnitOfWork _UnitOfWork;
        public BookService(IRepository<Book> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _UnitOfWork = unitOfWork;
        }
        public void Create(Book book)
        {
            _repository.Create(book);
            _UnitOfWork.Save();
        }

        public void Delete(Book book)
        {
            _repository.Delete(book);
            _UnitOfWork.Save();
        }

        public void Update(Book book)
        { 
            _repository.Update(book);
            _UnitOfWork.Save();
        }
        public void Create(IEnumerable<Book> books)
        {
            _repository.Create(books);
            _UnitOfWork.Save();
        }

        public void Delete(IEnumerable<Book> books)
        {
            _repository.Delete(books);
            _UnitOfWork.Save();
        }

        public void Update(IEnumerable<Book> books)
        {
            _repository.Update(books);
            _UnitOfWork.Save();
        }
    }
}
