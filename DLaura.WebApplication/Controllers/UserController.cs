using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.UserTables.Commands.DeleteUserTable;
using DLaura.BusinessLogic.UseCases.Usuarios.Commands.CreateUser;
using DLaura.BusinessLogic.UseCases.Usuarios.Commands.DeleteUser;
using DLaura.BusinessLogic.UseCases.Usuarios.Commands.Queries.GetRoles;
using DLaura.BusinessLogic.UseCases.Usuarios.Commands.UpdateUser;
using DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUser;
using DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUserAuthenticated;
using DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUsers;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DLaura.WebApplication.Controllers
{
    [Authorize(Roles = "Administrador")]
    // Restringe el acceso a usuarios autenticados
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        public static string Encrypt(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion(string? pReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(GetUserAuthenticatedQuery getUserAuthenticatedQuery)
        {
            

            var encryptedPassword = Encrypt(getUserAuthenticatedQuery.password);
            var updatedQuery = getUserAuthenticatedQuery with { password = encryptedPassword };
            try
            {
                // Enviamos la consulta para autenticar al usuario con _mediator
                var userResponse = await _mediator.Send(updatedQuery);

                // Verificamos si la respuesta no es nula y si el email coincide con el ingresado
                if (userResponse != null && userResponse.Email == getUserAuthenticatedQuery.Email)
                {

                    // Creamos una lista de Claims (datos que se almacenan en la sesión del usuario)
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email, userResponse.Email),
                        new Claim("Id", userResponse.Id.ToString()),
                        new Claim(ClaimTypes.Role, userResponse.RoleName), // Guardamos el rol del usuario 
                        new Claim("FirstName", userResponse.FirstName),//Se guarda el primer nombre
                        new Claim("LastName", userResponse.LastName)//Se guarda el apellido
                    };

                    // Creamos una identidad basada en las claims usando autenticación con cookies
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Registramos la sesión del usuario en el sistema de autenticación
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, // Esquema de autenticación por cookies
                        new ClaimsPrincipal(identity), // Asigna la identidad al usuario actual
                        new AuthenticationProperties { IsPersistent = true });  // La sesión será persistente (se mantendrá aunque se cierre el navegador)


                    // Llamar al método para redirigir según el rol del usuario
                    return RedirectToRoleBasedPage(userResponse.RoleName);
                }
                else
                {
                    // Si las credenciales son incorrectas, agregamos un mensaje de error en el ModelState
                    ModelState.AddModelError("", "Credenciales incorrectas");
                    // Volvemos a mostrar la vista de login con los datos ingresados
                    return View(getUserAuthenticatedQuery);
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error, lo registramos en los logs
                _logger.LogError(ex, "Error en el inicio de sesión");
                // Mostramos un mensaje de error genérico en la vista
                ModelState.AddModelError("", "Hubo un error al procesar la solicitud. Intente nuevamente.");
                // Devolvemos la vista de login
                return View(getUserAuthenticatedQuery);
            }

        }

        // Método para redirigir según el rol del usuario
        private IActionResult RedirectToRoleBasedPage(string roleName)
        {
            switch (roleName)
            {
                case "Recepcionista":
                    return RedirectToAction("Index", "Reservations"); // Página para recepcionista
                case "Administrador":
                    return RedirectToAction("Index", "User"); // Página para usuarios normales
                default:
                    return RedirectToAction("Index", "Home"); // Página por defecto para usuarios
            }
        }



        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return View(users);
        }

        //CREATE
        //Crear usuario (Para el administrador,incluye roles)
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Usuario") || User.IsInRole("Recepcionista"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var roles = await _mediator.Send(new GetRolesQuery());
                var model = new CreateUserRequest();

                // Verificar si el usuario autenticado es un administrador
                if (User.Identity.IsAuthenticated && User.IsInRole("Administrador"))
                {
                    ViewData["RolId"] = new SelectList(roles, "Id", "RoleName");
                }
                else
                {
                    // Si el usuario NO es administrador, ocultamos la selección de roles y asignamos "Usuario"
                    ViewData["RolId"] = new SelectList(roles.Where(r => r.Id == 3), "Id", "RoleName");
                }

                return View(model);

            }

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserRequest createUserRequest)
        {

            createUserRequest.UserPassword = Encrypt(createUserRequest.UserPassword);
            createUserRequest.Compensation = false;
            try
            {
                //Si el usuario no es administrador, asignamos el rol de usuario  normal (Id=3)
                if (!User.IsInRole("Administrador"))
                {
                    createUserRequest.RolId = 3; //Asignamos el rol por defecto para usuarios normales
                }

                // Enviar el comando al mediador
                var result = await _mediator.Send(new CreateUserCommand(createUserRequest));
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Usuario creado exitosamente";

                    // Redirigir al usuario al login después del registro
                    return RedirectToAction("Login");
                }
                throw new Exception("Ocurrió un error al intentar guardar el nuevo usuario");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                var roles = await _mediator.Send(new GetRolesQuery());
                ViewData["RolId"] = new SelectList(roles, "Id", "RoleName");
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));
            var roles = await _mediator.Send(new GetRolesQuery());
            ViewData["RolId"] = new SelectList(roles, "Id", "RoleName", user.RolId); // Si es null, asignamos false
            return View(user.Adapt<UpdateUserRequest>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserRequest updateUserRequest)
        {
            try
            {

                var result = await _mediator.Send(new UpdateUserCommand(updateUserRequest));
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Usuario actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                throw new Exception("Ocurrió un error al intentar editar el usuario");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar usuario");
                var roles = await _mediator.Send(new GetRolesQuery());
                ViewData["RolId"] = new SelectList(roles, "Id", "RoleName", updateUserRequest.RolId);
                ModelState.AddModelError("", ex.Message);

                return View(updateUserRequest);
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(int Id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(Id);
            await _mediator.Send(command, cancellationToken);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" || Request.Headers.Accept.Contains("application/json"))
            {
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }
    }
}