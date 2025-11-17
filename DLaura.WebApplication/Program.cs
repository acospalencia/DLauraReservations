using DLaura.BusinessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddBusinessLogicServices(builder.Configuration);

// Agrega y configura la autenticación en la aplicación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie((o) =>
    {
        // Especifica la ruta a la que se redirigirá al usuario si intenta acceder a una página protegida sin estar autenticado.
        // En este caso, si el usuario no ha iniciado sesión, será enviado a "/User/login".
        o.LoginPath = new PathString("/User/Login");

        // Especifica la ruta a la que se redirigirá al usuario si está autenticado pero no tiene permisos para acceder a una página.
        // Actualmente, está configurado para enviar al usuario a la misma página de inicio de sesión.
        o.AccessDeniedPath = new PathString("/User/AccessDenied");

        // Configura el tiempo de expiración de la cookie de autenticación.
        // En este caso, la sesión del usuario durará 8 horas antes de que sea necesario volver a iniciar sesión.
        o.ExpireTimeSpan = TimeSpan.FromHours(8);

        // Renueva automáticamente la fecha de expiración de la cookie con cada solicitud del usuario.
        // Esto significa que si el usuario interactúa con la aplicación antes de que la cookie expire,
        // la duración de la sesión se extenderá otros 8 días desde su última actividad.
        o.SlidingExpiration = true;

        // Establece la cookie como "HttpOnly", lo que significa que no será accesible desde JavaScript en el cliente.
        // Esto ayuda a mejorar la seguridad y evitar ataques XSS (Cross-Site Scripting).
        o.Cookie.HttpOnly = true;
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
