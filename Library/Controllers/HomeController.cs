using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/librarian")]
    public ActionResult Librarian()
    {
      return View();
    }
  }
}
