// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierresTreats.Models;
// using System.Threading.Tasks;
// using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace PierresTreats.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly PierresTreatsContext _db;

    public FlavorsController(PierresTreatsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Flavors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(m => m.FlavorId == id);

      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      if (flavor != null)
      {
        _db.Entry(flavor).State = EntityState.Modified;
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new {id = flavor.FlavorId});
    }

     public ActionResult Details(int id)
    {
      Flavor model = _db.Flavors
                        .Include(flavT => flavT.JoinEntities)
                        .ThenInclude(join => join.Treat)
                        .FirstOrDefault(m => m.FlavorId == id);
      return View(model);
    }

    public ActionResult Delete(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(m => m.FlavorId == id);

      return View(thisFlavor);
    }


    [HttpPost]
    public ActionResult Delete(Flavor flavor)
    {
      if (flavor != null)
      {
        _db.Flavors.Remove(flavor);
        _db.SaveChanges();
      }

      return RedirectToAction("Index");
    }

  }
}
