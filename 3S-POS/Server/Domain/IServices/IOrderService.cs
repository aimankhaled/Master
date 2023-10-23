using POS.Server.Domain.Entities;
using POS.Shared.Helpers;
using POS.Shared.ViewModels;

namespace POS.Server.Domain.IServices
{
    public interface IOrderService
    {
        Task<ApiReturn<List<Products>>> GetAllProducts();
        Task<ApiReturn<List<Orders>>> GetAllNonPaidOrders();
        Task<ApiReturn<bool>> PlaceOrder(OrderViewModel order);
        Task<ApiReturn<bool>> PayOrder(int orderId);
    }
}
