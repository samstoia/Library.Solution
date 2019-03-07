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
    // [HttpGet("/patrons/{id}")]
    // public ActionResult Show(int id)
    // {

    // }
  }
}