using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PierresTreats.Models;


namespace PierresTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierresTreatsContext _db;

    public HomeController( PierresTreatsContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      // ViewBag.Machines = _db.Treats.ToList();
      // ViewBag.Engineers = _db.Flavors.ToList();
      return View();
    }
  }
}