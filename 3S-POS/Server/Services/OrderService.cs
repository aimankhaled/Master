using Microsoft.Extensions.Configuration;
using POS.Server.Domain;
using POS.Server.Domain.Entities;
using POS.Server.Domain.IServices;
using POS.Shared.Helpers;
using POS.Shared.ViewModels;

namespace POS.Server.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Products> _productsRepository;
        IRepository<Orders> _ordersRepository;
        public OrderService(IRepository<Products> productsRepository, IRepository<Orders> ordersRepository)
        {
            _productsRepository = productsRepository;
            _ordersRepository = ordersRepository;
        }
        public async Task<ApiReturn<List<Products>>> GetAllProducts()
        {
            ApiReturn<List<Products>> apiRet = new ApiReturn<List<Products>>()
            {
                Data = new List<Products>()
            };
            try
            {
                var productRet = await _productsRepository.All();
                if (productRet.Count() > 0)
                {
                    apiRet.Data = productRet.ToList();
                    apiRet.Count = productRet.Count();
                }
            }
            catch (Exception ex)
            {
                apiRet.Data = new List<Products>();
                apiRet.AddTechnicalErrorEn();
            }
            return apiRet;
        }
        public async Task<ApiReturn<List<Orders>>> GetAllNonPaidOrders()
        {
            ApiReturn<List<Orders>> apiRet = new ApiReturn<List<Orders>>()
            {
                Data = new List<Orders>()
            };
            try
            {
                var productRet = await _ordersRepository.FindByInclude(x => !x.IsPaid, "Items");
                if (productRet.Count > 0)
                {
                    apiRet.Data = productRet.Data.ToList();
                    apiRet.Count = productRet.Count;
                }
            }
            catch (Exception ex)
            {
                apiRet.Data = new List<Orders>();
                apiRet.AddTechnicalErrorEn();
            }
            return apiRet;
        }
        public async Task<ApiReturn<bool>> PlaceOrder(OrderViewModel order)
        {
            ApiReturn<bool> apiRet = new ApiReturn<bool>()
            {
                Data = false
            };
            try
            {
                if (order != null)
                {
                    Orders ord = new Orders()
                    {
                        Amount = order.Amount,
                        IsPaid = false,
                        OrderName = order.OrderName,
                        Items = order.Items.Select(x => new OrderItems()
                        {
                            ItemName = x.ItemName,
                            Price = x.Price,
                            Count=x.Count
                        }).ToList(),
                    };
                    await _ordersRepository.Insert(ord);
                    apiRet.Data = true;
                }
            }
            catch (Exception ex)
            {
                apiRet.Data = false;
                apiRet.AddTechnicalErrorEn();
            }
            return apiRet;
        }
        public async Task<ApiReturn<bool>> PayOrder(int orderId)
        {
            ApiReturn<bool> apiRet = new ApiReturn<bool>()
            {
                Data = false
            };
            try
            {
                var ord = await _ordersRepository.FindBy(x => x.OrderId == orderId);
                if (ord.Count() > 0)
                {
                    ord.FirstOrDefault().IsPaid = true;
                    await _ordersRepository.Update(ord.FirstOrDefault());
                    apiRet.Data = true;
                }
            }
            catch (Exception ex)
            {
                apiRet.Data = false;
                apiRet.AddTechnicalErrorEn();
            }
            return apiRet;
        }

    }
}
