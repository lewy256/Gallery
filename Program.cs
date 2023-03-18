using Gallery.Configuration;
using Gallery.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<BlobService>(InitializeStorage.InitializeStorageClientInstanceAsync(builder.Configuration.GetSection("Blob")).GetAwaiter().GetResult());
builder.Services.AddSingleton<CosmosService>(InitializeCosmos.InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());


var app = builder.Build();

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");

app.Run();