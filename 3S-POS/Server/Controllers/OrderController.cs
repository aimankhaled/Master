using Microsoft.AspNetCore.Mvc;
using POS.Server.Domain.IServices;
using POS.Shared.ViewModels;
using POS.Shared.Helpers;

namespace POS.Server.Controllers
{
    public class OrderController : LoginController
    {
        private readonly ILogger<OrderController> _logger;
        private IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [HttpGet("GetAllProducts")]
        public async Task<ApiReturn<List<ProductsViewModel>>> GetAllProducts()
        {
            _logger.LogInformation("Get All Products");

            ApiReturn<List<ProductsViewModel>> apiRet = new ApiReturn<List<ProductsViewModel>>()
            {
                Data = new List<ProductsViewModel>()
            };
            try
            {
                var ret = await _orderService.GetAllProducts();
                foreach (var item in ret.Data)
                {
                    apiRet.Data.Add(new ProductsViewModel()
                    {
                        Id = item.ProductID,
                        Name = item.ProductName,
                        Price = item.Price,
                    });
                }
                _logger.LogInformation("Successfully Get All Products");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get All Products");
                apiRet.Data = new List<ProductsViewModel>();
                apiRet.AddNotFoundErrorEn();
            }
            return apiRet;
        }
        [HttpPost("PlaceOrder")]
        public async Task<ApiReturn<bool>> PlaceOrder([FromBody] OrderViewModel model)
        {
            _logger.LogInformation("Place Order {@model}",model);

            ApiReturn<bool> apiRet = new ApiReturn<bool>()
            {
                Data = false
            };
            try
            {
                apiRet = await _orderService.PlaceOrder(model);
                _logger.LogInformation("Successfully Place Order {@model}",model);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error Place Error {@model}",model);
                apiRet.Data = false;
                apiRet.AddNotFoundErrorEn();
            }
            return apiRet;
        }


        [HttpGet("GetAllNonPaidOrders")]
        public async Task<ApiReturn<List<OrderViewModel>>> GetAllNonPaidOrders()
        {
            _logger.LogInformation("Get All Non Paid Orders");


            ApiReturn<List<OrderViewModel>> apiRet = new ApiReturn<List<OrderViewModel>>()
            {
                Data = new List<OrderViewModel>()
            };
            try
            {
                var ret = await _orderService.GetAllNonPaidOrders();
                foreach (var item in ret.Data)
                {
                    apiRet.Data.Add(new OrderViewModel()
                    {
                        OrderId = item.OrderId,
                        OrderName = item.OrderName,
                        Amount = item.Amount,
                        Items = item.Items.Select(item => new OrderItemsViewModel()
                        {
                            OrderItemId = item.OrderItemId,
                            ItemName = item.ItemName,
                            Price = item.Price,
                            Count = item.Count
                        }).ToList()
                    });
                }
                _logger.LogInformation("Successfully Get All Non Paid Orders");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get All Non Paid Orders");
                apiRet.Data = new List<OrderViewModel>();
                apiRet.AddNotFoundErrorEn();
            }
            return apiRet;
        }

        [HttpPost("PayOrder")]
        public async Task<ApiReturn<bool>> PayOrder([FromBody] int orderId)
        {
            _logger.LogInformation("Pay Order Id {orderId}", orderId);

            ApiReturn<bool> apiRet = new ApiReturn<bool>()
            {
                Data = false
            };
            try
            {
                apiRet = await _orderService.PayOrder(orderId);
                _logger.LogInformation("Successfully Paid Order Id {orderId}", orderId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Pay Order Id {orderId}", orderId);
                apiRet.Data = false;
                apiRet.AddNotFoundErrorEn();
            }
            return apiRet;
        }
    }
}
