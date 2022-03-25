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
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTreats);
    }

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "CategoryId", "Name");
      return View();
    }

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

  //   public ActionResult Details(int id)
  //   {
  //     var thisRecipe = _db.Recipes
  //         .Include(recipe => recipe.JoinEntities)
  //         .ThenInclude(join => join.Category)
  //         .FirstOrDefault(recipe => recipe.RecipeId == id);


  //     //we want 
  //     ViewBag.recIngreds = _db.RecipeIngredient
  //                           .Include(ing => ing.Ingredient)
  //                           .ToList();
                          
  //     // ViewBag.amounts = _db.RecipeIngredient
  //     //                     .Where( recIng = recIng.RecipeId == id)
  //     //                     .ToList();

  //     return View(thisRecipe);
  //   }

  //   public ActionResult Edit(int id)
  //   {
  //     var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
  //     ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
  //     return View(thisRecipe);
  //   }

  //   [HttpPost]
  //   public ActionResult Edit(Recipe recipe, int CategoryId)
  //   {
  //     if (CategoryId != 0)
  //     {
  //       _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
  //     }
  //     _db.Entry(recipe).State = EntityState.Modified;
  //     _db.SaveChanges();
  //     return RedirectToAction("Index");
  //   }

  //   public ActionResult AddCategory(int id)
  //   {
  //     var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
  //     var thisCategoryRecipe = _db.CategoryRecipe.Where(categoryrecipe => categoryrecipe.RecipeId == id);
      
  //     List<Category> categories = _db.Categories.ToList();
  //     List<Category> categories2 = _db.Categories.ToList();

  //     foreach (CategoryRecipe categoryRecipe in thisCategoryRecipe)
  //     {
  //       foreach(Category category in categories)
  //       {
  //         if (category.CategoryId == categoryRecipe.CategoryId)
  //         {
  //           categories2.Remove(category);
  //         }
  //       }
  //     }
  //     ViewBag.CategoryId = new SelectList(categories2, "CategoryId", "Name");
  //     return View(thisRecipe);
  //   }

  //   [HttpPost]
  //   public ActionResult AddCategory(Recipe recipe, int CategoryId)
  //   {
  //     if (CategoryId != 0)
  //     {
  //     _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
  //     }
  //     _db.SaveChanges();
  //     return RedirectToAction("Index");
  //   }

  //   public ActionResult Delete(int id)
  //   {
  //     var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
  //     return View(thisRecipe);
  //   }

  //   [HttpPost, ActionName("Delete")]
  //   public ActionResult DeleteConfirmed(int id)
  //   {
  //     var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
  //     _db.Recipes.Remove(thisRecipe);
  //     _db.SaveChanges();
  //     return RedirectToAction("Index");
  //   }

  //   [HttpPost]
  //   public ActionResult DeleteCategory(int joinId)
  //   {
  //     var joinEntry = _db.CategoryRecipe.FirstOrDefault(entry => entry.CategoryRecipeId == joinId);
  //     _db.CategoryRecipe.Remove(joinEntry);
  //     _db.SaveChanges();
  //     return RedirectToAction("Index");
  //   }


  //   public ActionResult AddIngredient(int id)
  //   {
  //     var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
  //     var thisRecipeIngredient = _db.RecipeIngredient.Where(recIng => recIng.RecipeId == id);
      
  //     List<Ingredient> ingredients = _db.Ingredients.ToList();
  //     List<Ingredient> ingredients2 = _db.Ingredients.ToList();

  //     foreach (RecipeIngredient recipeIngredient in thisRecipeIngredient)
  //     {
  //       foreach(Ingredient ingredient in ingredients)
  //       {
  //         if (ingredient.IngredientId == recipeIngredient.IngredientId)
  //         {
  //           ingredients2.Remove(ingredient);
  //         }
  //       }
  //     }
  //     ViewBag.IngredientId = new SelectList(ingredients2, "IngredientId", "Name");
  //     return View(thisRecipe);
  //   }

  //   [HttpPost]
  //   public ActionResult AddIngredient(Recipe recipe, int IngredientId, string Amount)
  //   {
  //     if (IngredientId != 0)
  //     {
  //       _db.RecipeIngredient.Add(new RecipeIngredient() { IngredientId = IngredientId, RecipeId = recipe.RecipeId, Amount = Amount});
  //       _db.SaveChanges();
  //     }

  //     return RedirectToAction("Details", new {id = recipe.RecipeId});
  //   }

  //   public ActionResult EditAmount(int id)
  //   {
  //     var thisRecipeIngredient = _db.RecipeIngredient
  //                                 .Include(ing => ing.Ingredient)
  //                                 .FirstOrDefault( m => m.RecipeIngredientId == id);
  //     return View(thisRecipeIngredient);
  //   }

  //   [HttpPost]
  //   public ActionResult EditAmount(string amount, int id)
  //   {
  //     var recipeingredient =_db.RecipeIngredient.FirstOrDefault( s => s.RecipeIngredientId == id);
  //     recipeingredient.Amount = amount; 
  //     _db.Entry(recipeingredient).State = EntityState.Modified;
  //     _db.SaveChanges();
  //     return RedirectToAction("Index");
  //     // return RedirectToAction("Details", new {id = recipe.RecipeId });
  //   }
  }
}