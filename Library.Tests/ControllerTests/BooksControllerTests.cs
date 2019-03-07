using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Controllers;
using Library.Models;

namespace Library.Tests
{
  [TestClass]
  public class BooksControllerTest
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      BooksController controller = new BooksController();

      ActionResult indexView = controller.Index();

      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }
    [TestMethod]
    public void New_ReturnsCorrectView_True()
    {
      BooksController controller = new BooksController();

      ActionResult newView = controller.Index();

      Assert.IsInstanceOfType(newView, typeof(ViewResult));
    }
  }
}