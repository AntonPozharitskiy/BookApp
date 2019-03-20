using System;
using System.Collections.Generic;
using BLL.DataAccess;
using BLL.Entities;
using BLL.Finders;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookFinder _finder;
        public BookService(IRepository<Book> repository, IUnitOfWork unitOfWork, IBookFinder finder)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _finder = finder;
        }

        public void Create(Book book)
        {
            if(book == null) return;
            _repository.Create(book);
            _unitOfWork.Save();
        }

        public void Delete(Book book)
        {
            if(book == null) return;
            _repository.Delete(book);
            _unitOfWork.Save();
        }

        public void Update(Book book)
        {
            if (book == null) return;
            _repository.Update(book);
            _unitOfWork.Save();
        }

        public Book GetBook(int id)
        {
            return _finder.GetById(id);
        }

        public IEnumerable<Book> GetAll(Guid id)
        {
            return _finder.GetAll(id);
        }
    }
}
