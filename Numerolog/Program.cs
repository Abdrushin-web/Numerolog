using Colorology.Interpolation;
using Colorology.Spectra;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Numerolog;
using Numerology;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton(_ => Alphabets.German);

builder.Services.AddSingleton<IRGBCircularSpectrum, HSLCircularSpectrum>();
builder.Services.AddSingleton<IRGBCircularSpectrum, WavelengthCircularSpectrum>();
builder.Services.AddSingleton<IRGBCircularSpectrum>(InterpolatedCircularSpectrum.Colors7);
builder.Services.AddSingleton<IRGBCircularSpectrum>(InterpolatedCircularSpectrum.Colors12);
builder.Services.AddSingleton<IRGBCircularSpectrum, LChabCircularSpectrum>();
builder.Services.AddSingleton<IRGBCircularSpectrum, LChuvCircularSpectrum>();

builder.Services.AddSingleton<IRGBInterpolation, LChLinearInterpolation>();
builder.Services.AddSingleton<IRGBInterpolation, RGBLinearInterpolation>();

await builder.Build().RunAsync();
