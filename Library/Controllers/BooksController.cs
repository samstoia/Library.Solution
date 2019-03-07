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

    [HttpGet("/books/{bookId}")]
    public ActionResult Show(int bookId)
    {
      Book newBook = Book.Find(bookId);
      return View(newBook);
    }
    [HttpGet("/books/{bookId}/edit")]
    public ActionResult Edit(int bookId)
    {
      Book newBook = Book.Find(bookId);
      return View(newBook);
    }
    [HttpPost("/books/{bookId}/edit")]
    //string title has to match form fied in edit.cshtml
    public ActionResult EditPost(int bookId, string title)
    {
      Book newBook = Book.Find(bookId);
      newBook.Edit(title);
      return RedirectToAction("Index");
    }

    [HttpGet("/books/{bookId}/delete")]
    //HttpGet for delete because we are not going to new page to perform delete
    public ActionResult Delete(int bookId)
    {
      Book newBook = Book.Find(bookId);
      newBook.Delete();
      return RedirectToAction("Index");
    }


  }
}
