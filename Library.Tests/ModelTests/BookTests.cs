using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.Tests
{
  [TestClass]
  public class BookTest : IDisposable
  {
    public void Dispose()
    {
      Book.ClearAll();
    }
    public BookTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889; database=library_tests;";
    }
    [TestMethod]
    public void BookConstructor_CreatesInstanceOfBook_Book()
    {
      Book newBook = new Book("test", 1);
      Assert.AreEqual(typeof(Book), newBook.GetType());
    }
    [TestMethod]
    public void GetTitle_GetsBookTitle_String()
    {
      string title = "Moby Dick";
      Book newBook = new Book(title, 1);
      string result = newBook.GetTitle();
      Assert.AreEqual(title, result);
    }

    [TestMethod]
    public void SetTitle_SetsBookTitle_String()
    {
      string title = "Moby Dick";
      Book newBook = new Book(title, 1);
      string updatedTitle = "Test";
      newBook.SetTitle(updatedTitle);
      string result = newBook.GetTitle();
      Assert.AreEqual(updatedTitle, result);
    }
    [TestMethod]
    public void GetBookId_GetsBookId_Int()
    {
      int id = 1;
      Book newBook = new Book("test", id);
      int result = newBook.GetBookId();
      Assert.AreEqual(id, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_Book()
    {
      Book testBook = new Book("test");
      testBook.Save();
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsBooks_BookList()
    {
      string book1 = "Fight Club";
      string book2 = "Harry Potter";
      Book newBook1 = new Book(book1);
      newBook1.Save();
      Book newBook2 = new Book(book2);
      newBook2.Save();
      List<Book> newList = new List<Book> {newBook1, newBook2};

      List<Book> result = Book.GetAll();

      CollectionAssert.AreEqual(newList, result);

    }

    [TestMethod]
    public void Find_ReturnsCorrectBookFromDatabase_Book()
    {
      Book testBook = new Book("test");
      testBook.Save();

      //Act
      Book foundBook= Book.Find(testBook.GetBookId());

      //Assert
      Assert.AreEqual(testBook, foundBook);
    }
  }
}
