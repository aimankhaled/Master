namespace POS.Client.Helper
{
    public class HttpClientServices
    {
        public HttpClient httpClient;

        public HttpClientServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetBaseUrl()
        {
            return httpClient.BaseAddress.ToString();
        }
    }
}
