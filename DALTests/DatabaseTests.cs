using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Entities;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DALTests
{
    public class DatabaseTests
    {
        public static DbContextOptions<ApplicationContext> _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "ESW413")
            .Options;

        public ApplicationContext _context = new ApplicationContext(_options);
        [Fact]
        public void CheckIfDatabaseExist()
        { 
            Assert.NotNull(_context);
        }

        [Fact]
        public void TestingCreateMethod()
        {
            _context.Books.Add(new Book { Title = "Demo1", AuthorId = Guid.NewGuid().ToString(), DateOfRelease = new DateTime(1998, 09, 03) });
            _context.Books.Add(new Book { Title = "Demo2", AuthorId = Guid.NewGuid().ToString(), DateOfRelease = new DateTime(1998, 09, 03) });
            _context.Books.Add(new Book { Title = "Demo3", AuthorId = Guid.NewGuid().ToString(), DateOfRelease = new DateTime(1998, 09, 03) });
            _context.SaveChanges();
            Assert.Equal(3, _context.Books.Count());
        }

        [Fact]
        public void TestingDeleteMethod()
        {
            Book dirtyBook = new Book {Title = "Demo1", AuthorId = Guid.NewGuid().ToString(), DateOfRelease = new DateTime(1998, 09, 03)};
            _context.Books.Add(dirtyBook);
            _context.SaveChanges();
            _context.Books.Remove(dirtyBook);
            _context.SaveChanges();
            Assert.Null(_context.Books.Where(c => c.Title == "Demo1").FirstOrDefault());
        }

        [Fact]
        public void TestingUpdateMethod()
        {
            Book dirtyBook = new Book { Title = "Demo1", AuthorId = Guid.NewGuid().ToString(), DateOfRelease = new DateTime(1998, 09, 03) };
            _context.Books.Add(dirtyBook);
            _context.SaveChanges();
            dirtyBook.Title = "UpdatedDemo1";
            _context.SaveChanges();
            Assert.Equal("UpdatedDemo1", dirtyBook.Title);

        }
    }
}
