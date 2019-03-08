using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System;


namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    [HttpGet("/authors")]
    public ActionResult Index()
    {
      List<Author> allAuthors = Author.GetAll();
      return View(allAuthors);
    }
    [HttpGet("/authors/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpPost("/authors")]
    public ActionResult Create(string name)
    {
      Author newAuthor = new Author(name);
      newAuthor.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/authors/{authorId}")]
    public ActionResult Show(int authorId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      Author newAuthor = Author.Find(authorId);
      List<Book> authorBooks = newAuthor.GetBooks();
      List<Book> allBooks = Book.GetAll();
      model.Add("author", newAuthor);
      model.Add("authorBooks", authorBooks);
      model.Add("allBooks", allBooks);
      return View(model);
    }
    [HttpGet("/authors/{authorId}/edit")]
    public ActionResult Edit(int authorId)
    {
      Author newAuthor = Author.Find(authorId);
      return View(newAuthor);
    }

    [HttpPost("/authors/{authorId}/edit")]
    //string title has to match form fied in edit.cshtml
    public ActionResult EditPost(int authorId, string name)
    {
      Author newAuthor = Author.Find(authorId);
      newAuthor.Edit(name);
      return RedirectToAction("Index");
    }
    [HttpGet("/authors/{authorId}/delete")]
    //HttpGet for delete because we are not going to new page to perform delete
    public ActionResult Delete(int authorId)
    {
      Author newAuthor = Author.Find(authorId);
      newAuthor.Delete();
      return RedirectToAction("Index");
    }
    [HttpPost("/authors/{authorId}/books/new")]
    public ActionResult AddBook(int authorId, int bookId)
    {
      Author author = Author.Find(authorId);
      Book book = Book.Find(bookId);
      author.AddBook(book);
      return RedirectToAction("Show", new {authorId = authorId});
    }

  }
}
