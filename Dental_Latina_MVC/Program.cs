using LogicaAplicacion.InterfacesCasosUso;
using LogicaDatos.EntityFramework;
using LogicaDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorios;
using LogicaAplicacion.CasosUso;
using LogicaDatos.EntityFramework;
using LogicaDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorios;
using LogicaAplicacion.InterfacesCasoUso;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILoginUser, LoginUser>();
builder.Services.AddScoped<IListarProductos, ListarProductos>();
builder.Services.AddScoped<IListarCategorias, ListarCategorias>();
builder.Services.AddScoped<IlistarSubcategorias, ListarSubcategorias>();
builder.Services.AddScoped<IRepositorioUsuario, repositorioUsuario>();
builder.Services.AddScoped<IRepositorioProducto, repositorioProducto>();
builder.Services.AddScoped<IRepositorioCategoria, repositorioCategoria>();
builder.Services.AddScoped<IRepositorioSubcategoria, repositorioSubcategoria>();
builder.Services.AddDbContext<LibreriaContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AltaProducto}/{action=Index}/{id?}");

app.Run();
