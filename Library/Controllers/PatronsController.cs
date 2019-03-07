using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System;


namespace Library.Controllers
{
  public class PatronsController : Controller
  {

    [HttpGet("/patrons")]
    public ActionResult Index()
    {
      List<Patron> allPatrons = Patron.GetAll();
      return View(allPatrons);
    }
    [HttpGet("/patrons/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/patrons")]
    public ActionResult Create(string patron_name, string address, string phone)
    {
        Patron newPatron = new Patron(patron_name, address, phone);
        newPatron.Save();
        return RedirectToAction("Index");
    }
    [HttpGet("/patrons/{patronId}")]
    public ActionResult Show(int patronId)
    {
      Patron newPatron = Patron.Find(patronId);
      return View(newPatron);
    }

    [HttpGet("/patrons/{patronId}/edit")]
    public ActionResult Edit(int patronId)
    {
      Patron newPatron = Patron.Find(patronId);
      return View(newPatron);
    }
    [HttpPost("/patrons/{patronId}/edit")]
    public ActionResult EditPost(int patronId, string name, string address, string phone)
    {
      Patron newPatron = Patron.Find(patronId);
      newPatron.Edit(name, address, phone);
      return RedirectToAction("Index");
    }
  }
}
