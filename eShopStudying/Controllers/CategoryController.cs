using Microsoft.AspNetCore.Mvc;
using eShopStudying.DataAccess;
using eShopStudying.Models;

namespace eShopStudying.Controllers;

public class CategoryController : Controller
{
    private readonly SQLDBContext _dbContext;

    public CategoryController(SQLDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> categoriesList = _dbContext.Categories;
        return View(categoriesList);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category element was successfuly created";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot create category element";
        return View(obj);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var obj = _dbContext.Categories.Find(id);

        if (obj == null) 
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category element was successfuly Updated";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot update category element";
        return View(obj);
    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var obj = _dbContext.Categories.Find(id);

        if (obj == null) 
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var obj = _dbContext.Categories.Find(id);
        if (obj == null) 
        {
            TempData["error"] = "Cannot delete category element";
            return NotFound();
        }

        _dbContext.Categories.Remove(obj);
        _dbContext.SaveChanges();
        TempData["success"] = "Category element was deleted successfuly";
        return RedirectToAction("Index");
    }
}