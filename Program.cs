using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChatGPTDiffApp;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri("https://api.openai.com/") }
);
builder.Services.AddScoped<ChatGPTDiffApp.Services.ChatGPTService>();

await builder.Build().RunAsync();
