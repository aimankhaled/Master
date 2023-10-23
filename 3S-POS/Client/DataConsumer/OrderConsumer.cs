using POS.Client.Helper;
using POS.Shared.Helpers;
using POS.Shared.ViewModels;

namespace POS.Client.DataConsumer
{
    public class OrderConsumer
    {
        private readonly HttpClientServices _httpClientServices;
        public OrderConsumer(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }
        public async Task<ApiReturn<List<ProductsViewModel>>> GetAllProducts()
        {
            try
            {
                var Ret = await _httpClientServices.httpClient.GetJsonAsync<List<ProductsViewModel>>($"api/Order/GetAllProducts");
                return Ret;
            }
            catch (Exception ex)
            {
                return new ApiReturn<List<ProductsViewModel>>();
            }
        }
        public async Task<ApiReturn<List<OrderViewModel>>> GetAllNonPaidOrders()
        {
            try
            {
                var Ret = await _httpClientServices.httpClient.GetJsonAsync<List<OrderViewModel>>($"api/Order/GetAllNonPaidOrders");
                return Ret;
            }
            catch (Exception ex)
            {
                return new ApiReturn<List<OrderViewModel>>();
            }
        }
        public async Task<ApiReturn<bool>> PlaceOrder(OrderViewModel model)
        {
            try
            {
                var Ret = await _httpClientServices.httpClient.PostJsonAsync<bool, OrderViewModel>($"api/Order/PlaceOrder", model);
                return Ret;
            }
            catch (Exception ex)
            {
                return new ApiReturn<bool>();
            }
        }
        public async Task<ApiReturn<bool>> PayOrder(int orderId)
        {
            try
            {
                var Ret = await _httpClientServices.httpClient.PostJsonAsync<bool, int>($"api/Order/PayOrder", orderId);
                return Ret;
            }
            catch (Exception ex)
            {
                return new ApiReturn<bool>();
            }
        }
    }
}
