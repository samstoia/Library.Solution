using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Controllers;
using Library.Models;

namespace Library.Tests
{
  [TestClass]
  public class HomeControllerTest
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      HomeController controller = new HomeController();

      ActionResult indexView = controller.Index();

      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

    [TestMethod]
    public void Librarian_ReturnsCorrectView_True()
    {
      HomeController controller = new HomeController();

      ActionResult librarianView = controller.Librarian();

      Assert.IsInstanceOfType(librarianView, typeof(ViewResult));
    }

    

  }
}
