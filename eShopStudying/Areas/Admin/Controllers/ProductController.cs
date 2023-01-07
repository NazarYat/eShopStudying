using Microsoft.AspNetCore.Mvc;
using eShopStudying.DataAccess;
using eShopStudying.Models;
using eShopStudying.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using eShopStudying.Models.ViewModels;

namespace eShopStudying.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<Product> categoriesList = _unitOfWork.Product.GetAll();
        return View(categoriesList);
    }

    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new() 
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };

        if (id == null || id == 0)
        {
            //create product
            return View(productVM);
        }
        else 
        {
            // update product
        }
        return View(productVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            // _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product element was successfuly Updated";
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
        var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

        if (obj == null) 
        {
            return NotFound();
        }
        return View(obj);
    }
    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (obj == null) 
        {
            TempData["error"] = "Cannot delete category element";
            return NotFound();
        }

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Product element was deleted successfuly";
        return RedirectToAction("Index");
    }
}