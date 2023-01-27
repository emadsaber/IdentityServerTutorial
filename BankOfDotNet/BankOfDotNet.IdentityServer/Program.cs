using BankOfDotNet.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services
        .AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryApiResources(Config.GetApiResources())
        .AddInMemoryApiScopes(Config.GetApiScopes())
        .AddInMemoryClients(Config.GetClients());
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseIdentityServer();
app.UseAuthorization();
app.Run();
