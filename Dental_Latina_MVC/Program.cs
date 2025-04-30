using LogicaAplicacion.InterfacesCasosUso;
using LogicaDatos.EntityFramework;
using LogicaDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorios;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.HttpOverrides;
using LogicaAplicacion.InterfacesCasoUso;
using Microsoft.EntityFrameworkCore;
using FluentAssertions.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Configuración de servicios
builder.Services.AddSession();
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ILoginUser, LoginUser>();
builder.Services.AddScoped<IRegistroCliente, RegistroCliente>();
builder.Services.AddScoped<IListarProductos, ListarProductos>();
builder.Services.AddScoped<IDetalleProducto, DetalleProducto>();
builder.Services.AddScoped<IListarCategorias, ListarCategorias>();
builder.Services.AddScoped<IlistarSubcategorias, ListarSubcategorias>();
builder.Services.AddScoped<IListarClientes, ListarClientes>();
builder.Services.AddScoped<IEliminarProducto, EliminarProducto>();
builder.Services.AddScoped<IRepositorioUsuario, repositorioUsuario>();
builder.Services.AddScoped<IRepositorioProducto, repositorioProducto>();
builder.Services.AddScoped<IRepositorioCategoria, repositorioCategoria>();
builder.Services.AddScoped<IRepositorioZona, repositorioZona>();
builder.Services.AddScoped<IDetalleZona, DetalleZona>();
builder.Services.AddScoped<IAltaSubcategoria, AltaSubcategoria>();
builder.Services.AddScoped<IRepositorioSubcategoria, repositorioSubcategoria>();
builder.Services.AddScoped<IAltaProducto, AltaProducto>();
builder.Services.AddScoped<IAltaCategoria, AltaCategoria>();
builder.Services.AddScoped<IEliminarCategoriaSub, EliminarCategoriaSub>();
builder.Services.AddScoped<IListarCategoriasEspeciales, ListarCategoriasEspeciales>();
builder.Services.AddScoped<IRepositorioCEspecial, repositorioCEspecial>();
builder.Services.AddDbContext<LibreriaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25))));
var app = builder.Build();

// Configuración del middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Configuración para manejar encabezados de proxy (necesario en Plesk)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UsePathBase("/plesk-site-preview/prueba.uy");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// Configuración de las rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Run();