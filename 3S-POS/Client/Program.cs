using POS.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using POS.Client.DataConsumer;
using Blazored.LocalStorage;
using POS.Client.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using POS.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<AuthConsumer>();
builder.Services.AddTransient<OrderConsumer>();
builder.Services.AddBlazoredModal();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<HttpClientServices>("APIAddress", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIAddress")));


builder.Services.AddAuthorizationCore(options => new IdentityHelper.Policies(options));
builder.Services.AddScoped<TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());
HttpClientExtenstions.tokenStorage = builder.Services.BuildServiceProvider().GetRequiredService<TokenAuthenticationStateProvider>();
await builder.Build().RunAsync();
