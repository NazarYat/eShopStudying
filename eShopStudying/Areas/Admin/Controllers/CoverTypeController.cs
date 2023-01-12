using Microsoft.AspNetCore.Mvc;
using eShopStudying.DataAccess;
using eShopStudying.Models;
using eShopStudying.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using eShopStudying.Utility;

namespace eShopStudying.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> categoriesList = _unitOfWork.CoverType.GetAll();
        return View(categoriesList);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType element was successfuly created";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot create CoverType element";
        return View(obj);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (obj == null) 
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType element was successfuly Updated";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot update CoverType element";
        return View(obj);
    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (obj == null) 
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (obj == null) 
        {
            TempData["error"] = "Cannot delete CoverType element";
            return NotFound();
        }

        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType element was deleted successfuly";
        return RedirectToAction("Index");
    }
}