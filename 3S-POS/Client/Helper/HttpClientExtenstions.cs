using POS.Shared.Helpers;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace POS.Client.Helper
{
    public static class HttpClientExtenstions
    {
        public static TokenAuthenticationStateProvider tokenStorage;
        public static async Task<ApiReturn<T>> GetJsonAsync<T>(this HttpClient http, string url, string overrideToken = "")
        {
            if (url[0] == '/')
            {
                url = url.Substring(1);
            }
            try
            {

                http.DefaultRequestHeaders.Authorization =
       new AuthenticationHeaderValue("Bearer", string.IsNullOrEmpty(overrideToken)
       ? await tokenStorage.GetTokenAsync() : overrideToken);

                var httpResponse = await http.GetAsync($"{url}");


                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var content = JsonSerializer.Deserialize<ApiReturn<T>>(responseString,
                        new JsonSerializerOptions()
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    return content;
                }
                else if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ApiReturn<T>() { Errors = new List<Error>() { new Error("Un authorized") } };
                }
                else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    return new ApiReturn<T>() { Errors = new List<Error>() { new Error("URL not found") } };
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                return new ApiReturn<T>()
                { Errors = new List<Error>() { new Error("A problem occured while calling api") } };
            }
        }



        public static async Task<ApiReturn<TReturn>> PostJsonAsync<TReturn, TValue>(this HttpClient http, string url,
            TValue value)
        {
            if (url[0] == '/')
            {
                url = url.Substring(1);
            }
            var tt = await tokenStorage.GetTokenAsync();
            http.DefaultRequestHeaders.Authorization =
new AuthenticationHeaderValue("Bearer", await tokenStorage.GetTokenAsync());

            var httpResponse = await http.PostAsJsonAsync<TValue>($"{url}", value);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var content = JsonSerializer.Deserialize<ApiReturn<TReturn>>(responseString,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return content;
            }
            else if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await tokenStorage.SetTokenAsync(null);
                return new ApiReturn<TReturn>() { Errors = new List<Error>() { new Error("Un authorized") } };

            }
            else
            {
                throw new Exception();
            }
        }
    }
}