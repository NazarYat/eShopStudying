using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;

namespace eShopStudying.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly SQLDBContext _db;
        public OrderHeaderRepository(SQLDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderHeaderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentDate = DateTime.Now;
                orderHeaderFromDb.SessionId = sessionId;
                orderHeaderFromDb.PaymentIntendId = paymentIntentId;
            }
        }
    }
}