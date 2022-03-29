using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierresTreats.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace PierresTreats.Controllers
{
  
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Treats.ToList());
    }

    [Authorize]
    public ActionResult Create()
    {

      return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(je => je.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(t => t.TreatId == id);

      return View(thisTreat);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(m => m.TreatId == id);

      return View(thisTreat);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      if (treat != null)
      {
       _db.Entry(treat).State = EntityState.Modified;
       _db.SaveChanges();
      }

      return RedirectToAction("Details", new {id = treat.TreatId});
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(m => m.TreatId == id);

      return View(thisTreat);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Delete(Treat treat)
    {
      if (treat != null)
      {
        _db.Treats.Remove(treat);
        _db.SaveChanges();
      }

      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddFlavor(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(i => i.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");

      return View(thisTreat);
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        if(_db.FlavorTreats.Where( flavT => flavT.FlavorId == FlavorId && flavT.TreatId == treat.TreatId).ToList().Count() == 0 )
        {
          _db.FlavorTreats.Add(new FlavorTreat(){ FlavorId = FlavorId, TreatId = treat.TreatId});
          _db.SaveChanges();
        }
      }

      return RedirectToAction("Details", new {id = treat.TreatId});
    }

    [Authorize]
    public ActionResult RemoveFlavor(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(i => i.TreatId == id);
      List<Flavor> flavSelect = _db.FlavorTreats
                        .Where(m => m.TreatId == id)
                        .Select(n => n.Flavor)
                        .ToList();

      ViewBag.FlavorId = new SelectList(flavSelect, "FlavorId", "Name");

      return View(thisTreat);
    }

    [Authorize]
    [HttpPost]
    public ActionResult RemoveFlavor(Treat treat, int FlavorId)
    {
      FlavorTreat flavToRemove = _db.FlavorTreats.FirstOrDefault( flavT => flavT.FlavorId == FlavorId && flavT.TreatId == treat.TreatId);

      if (FlavorId != 0)
      {
        if(flavToRemove != null )
        {
          _db.FlavorTreats.Remove(flavToRemove);
          _db.SaveChanges();
        }
      }

      return RedirectToAction("Details", new {id = treat.TreatId});
    }
  }
}