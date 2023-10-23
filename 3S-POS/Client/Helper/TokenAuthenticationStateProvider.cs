using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using POS.Client.DataConsumer;
using System.Security.Claims;
using System.Text.Json;

namespace POS.Client.Helper
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService storage;
        private readonly AuthConsumer _consumer;
        public TokenAuthenticationStateProvider(ILocalStorageService storage, AuthConsumer consumer)
        {
            this.storage = storage;
            _consumer = consumer;
        }

        public async Task SetTokenAsync(string token, DateTime expiry = default)
        {
            if (token == null)
            {
                await storage.RemoveItemAsync("authTokenPOS");
                await storage.RemoveItemAsync("authTokenPOSExpiry");
            }
            else
            {
                await storage.SetItemAsync("authTokenPOS", token);
                await storage.SetItemAsync("authTokenPOSExpiry", DateTime.Now.AddDays(5).ToString());
            }
        }

        public async Task<string> GetTokenAsync(bool validate = false)
        {
            var expiry = await storage.GetItemAsync<string>("authTokenPOSExpiry");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now || true)
                {
                    var storage_token = await storage.GetItemAsync<string>("authTokenPOS");

                    if (validate)
                    {
                        var user = await _consumer.ValidateToken(storage_token);
                        if (user.HasErrors || user.Data == null || string.IsNullOrEmpty(user.Data.EmpNum))
                        {
                            await SetTokenAsync(null);
                            return "";
                        }
                        else
                        {
                            return storage_token;
                        }
                    }
                    else
                    {
                        return storage_token;

                    }
                }
                else
                {
                    await SetTokenAsync(null);
                }
            }
            return "";
        }
        private async Task<IEnumerable<Claim>> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = await ParseBase64WithoutPaddingAsync(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private async Task<byte[]> ParseBase64WithoutPaddingAsync(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            // New Validation
            base64 = base64.Replace('_', '/').Replace("-", "+");
            try
            {
                return Convert.FromBase64String(base64);
            }
            catch
            {
                await storage.RemoveItemAsync("authTokenPOS");
                await storage.RemoveItemAsync("authTokenPOSExpiry");
                return null;
            }
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync(true);
            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(await ParseClaimsFromJwt(token), "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
