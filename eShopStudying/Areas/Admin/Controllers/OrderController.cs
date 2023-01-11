using System.Security.Claims;
using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;
using eShopStudying.Models.ViewModels;
using eShopStudying.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopStudying.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM orderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int orderId)
        {
            orderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(u => u.OrderId == orderId, includeProperties: "Product")
            };
            return View(orderVM);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string? status = null)
        {
            IEnumerable<OrderHeader> orderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else 
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unitOfWork.OrderHeader.GetAll( u => u.ApplicationUserId == claim.Value, includeProperties: "ApplicationUser");
            }

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatussDelayedPayment);
                    break;

                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusProcessing);
                    break;

                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.PaymentStatusApproved);
                    break;

                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
            }

            return Json(new {
                data = orderHeaders
            });
        }
        #endregion
    }
}