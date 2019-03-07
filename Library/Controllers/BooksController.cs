using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System;


namespace Library.Controllers
{
  public class BooksController : Controller
  {

    [HttpGet("/books")]
    public ActionResult Index()
    {
      List<Book> allBooks = Book.GetAll();
      return View(allBooks);
    }
    [HttpGet("/books/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpPost("/books")]
    public ActionResult Create(string title)
    {
        Book newBook = new Book(title);
        newBook.Save();
        return RedirectToAction("Index");
    }
  }
}