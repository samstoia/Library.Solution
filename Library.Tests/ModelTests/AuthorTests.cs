using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using System.Collections.Generic;
using System;

namespace Library.Tests
{
  [TestClass]
  public class AuthorTest : IDisposable
  {
    public void Dispose()
    {
      Author.ClearAll();
    }

    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889; database=library_tests;";
    }

    [TestMethod]
    public void AuthorConstructor_CreatesInstanceOfAuthor_True()
    {
      Author newAuthor = new Author("Herman Melville");
      Assert.AreEqual(typeof(Author), newAuthor.GetType());
    }
    [TestMethod]
    public void GetAuthorName_GetsAuthorName_String()
    {
      string name = "test";
      Author newAuthor = new Author(name);
      string result = newAuthor.GetAuthorName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetAuthorName_SetsAuthorName_String()
    {
      string name = "Herman Melville";
      Author newAuthor = new Author(name, 1);
      string updatedName = "JK Rowling";
      newAuthor.SetAuthorName(updatedName);
      string result = newAuthor.GetAuthorName();
      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAuthorId_GetsAuthorId_Int()
    {
      int id = 1;
      Author newAuthor = new Author("test", id);
      int result = newAuthor.GetAuthorId();
      Assert.AreEqual(id, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_Author()
    {
      Author testAuthor = new Author("test");
      testAuthor.Save();
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void GetAll_ReturnsAuthors_AuthorList()
    {
      string author1 = "Herman Melville";
      string author2 = "JK Rowling";
      Author newAuthor1 = new Author(author1);
      newAuthor1.Save();
      Author newAuthor2 = new Author(author2);
      newAuthor2.Save();
      List<Author> newList = new List<Author> {newAuthor1, newAuthor2};

      List<Author> result = Author.GetAll();

      CollectionAssert.AreEqual(newList, result);

    }
  }
}
