using Microsoft.AspNetCore.Mvc;
using eShopStudying.DataAccess;
using eShopStudying.Models;
using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Utility;
using Microsoft.AspNetCore.Authorization;

namespace eShopStudying.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> categoriesList = _unitOfWork.Category.GetAll();
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
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
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
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

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
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
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
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

        if (obj == null) 
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null) 
        {
            TempData["error"] = "Cannot delete category element";
            return NotFound();
        }

        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category element was deleted successfuly";
        return RedirectToAction("Index");
    }
}