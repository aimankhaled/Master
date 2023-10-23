using POS.Client.Helper;
using POS.Shared.Helpers;
using POS.Shared.ViewModels;

namespace POS.Client.DataConsumer
{
    public class AuthConsumer
    {
        private readonly HttpClientServices _httpClientServices;
        public AuthConsumer(HttpClientServices httpClientServices)
        {
            _httpClientServices = httpClientServices;
        }

        public async Task<ApiReturn<AuthReturn>> ValidateToken(string token)
        {
            var content = await _httpClientServices.httpClient.GetJsonAsync<AuthReturn>($"api/Login/Login", overrideToken: token);
            return content;
        }
    }
}
