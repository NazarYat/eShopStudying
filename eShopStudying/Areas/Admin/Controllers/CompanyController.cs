using Microsoft.AspNetCore.Mvc;
using eShopStudying.DataAccess;
using eShopStudying.Models;
using eShopStudying.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using eShopStudying.Models.ViewModels;

namespace eShopStudying.Controllers;

[Area("Admin")]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<Company> companiesList = _unitOfWork.Company.GetAll();
        return View(companiesList);
    }

    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        Company company = new();

        if (id == null || id == 0)
        {
            return View(company);
        }
        else 
        {
            company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            return View(company);
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);
                TempData["success"] = "Company was successfuly added";
            }
            else
            {
                _unitOfWork.Company.Update(obj);
                TempData["success"] = "Company was successfuly updated";
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        TempData["error"] = "Cannot create new company";
        return View(obj);
    }

    #region API CALLS
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
        if (obj == null) 
        {
            return Json(new { success = false, message = "Cannot delete company"});
        }

        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Company was deleted successfuly"});
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var companyList = _unitOfWork.Company.GetAll();
        return Json(new {
            data = companyList
        });
    }
    #endregion
}