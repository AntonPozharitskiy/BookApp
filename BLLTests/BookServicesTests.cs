using System;
using BLL.DataAccess;
using BLL.Entities;
using BLL.Finders;
using BLL.Services;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BLLTests
{
    public class BookServicesTests
    {
        public static readonly DbContextOptions<ApplicationContext> _options =
            new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("ESW413")
                .Options;

        private readonly ApplicationContext _context = new ApplicationContext(_options);

        private static readonly Mock<IRepository<Book>> _reposMock = new Mock<IRepository<Book>>();
        private static readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();
        private static readonly Mock<IBookFinder> _finderMock = new Mock<IBookFinder>();
        private readonly BookService _service = new BookService(_reposMock.Object, _uowMock.Object, _finderMock.Object);
        private Book testBook = new Book { Id = 0, DateOfRelease = DateTime.Now, Title = "Test Book", AuthorId = 1 };
        private Book nullBook = null;

        [Fact]
        public void CreateBookTest()
        { 
            _service.Create(testBook);
            _reposMock.Verify(c => c.Create(It.IsAny<Book>()), Times.Once());
            _uowMock.Verify(c => c.Save(), Times.Once());
        }

        [Fact]
        public void CreateNullBookTest()
        {
            _service.Create(null);
            _reposMock.Verify(c => c.Create(It.IsAny<Book>()), Times.Never);
            _uowMock.Verify(c => c.Save(), Times.Never);
        }

        [Fact]
        public void DeleteBookTest()
        {
            _service.Create(testBook);
            _service.Delete(testBook);
            _reposMock.Verify(c => c.Create(testBook), Times.Once());
            _uowMock.Verify(c => c.Save(), Times.AtLeastOnce());
            _reposMock.Verify(c => c.Delete(testBook), Times.Once());
        }

        [Fact]
        public void DeleteNullBookTest()
        {
            _service.Create(nullBook);
            _service.Delete(nullBook);
            _reposMock.Verify(c => c.Create(It.IsAny<Book>()), Times.Never);
            _uowMock.Verify(c => c.Save(), Times.Never);
            _reposMock.Verify(c => c.Delete(It.IsAny<Book>()), Times.Never);
        }

        [Fact]
        public void UpdateBookTest()
        {
            _service.Create(testBook);
            testBook.Title = "Updated Book";
            _service.Update(testBook);
            _reposMock.Verify(c=>c.Update(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public void UpdateNullBookTest()
        {
            _service.Update(nullBook);
            _reposMock.Verify(c => c.Update(It.IsAny<Book>()), Times.Never);
        }

        [Fact]
        public void UpdateNonExistentBookTest()
        {

        }
    }
}