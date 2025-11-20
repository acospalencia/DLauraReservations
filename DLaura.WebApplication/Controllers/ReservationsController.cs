using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Locations.Queries.GetTableLocation;
using DLaura.BusinessLogic.UseCases.Reservations.Commands.CreateReservation;
using DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservation;
using DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservationByDate;
using DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservationsByDateAndShift;
using DLaura.BusinessLogic.UseCases.Usuarios.Commands.UpdateCompensation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DLaura.WebApplication.Controllers
{
    
    public class ReservationsController : Controller
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Recepcionista")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var fechahoy = new DateFilterRequest
            {
                ReservationDate = DateOnly.FromDateTime(DateTime.Today)
            };
            var query = new GetReservationByDateQuery(fechahoy);
            var reservations = await _mediator.Send(query, cancellationToken);
            if (reservations.Any())
            {
                return View("Index", reservations);
            }
            var listavacia = new List<ReservationResponse>();
            return View("Index", listavacia);
        }

        [Authorize(Roles = "Recepcionista")]
        [HttpGet]
        public async Task<IActionResult> GetEspecificDay(DateTime fecha, CancellationToken cancellationToken)
        {
            var fechaOnly = DateOnly.FromDateTime(fecha);

            var fechahoy = new DateFilterRequest
            {
                ReservationDate = fechaOnly
            };
            var query = new GetReservationByDateQuery(fechahoy);
            var reservations = await _mediator.Send(query, cancellationToken);

            if (reservations.Any())
            {
                return View("Index", reservations);
            }
            var listavacia = new List<ReservationResponse>();
            return View("Index", listavacia);
        }

        
        [AllowAnonymous]

        public IActionResult SearchReservations()
        {
            if (User.IsInRole("Administrador") || User.IsInRole("Recepcionista"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SearchReservations(ReservationByFilterRequest reservation, CancellationToken cancellationToken)
        {

            if (reservation.ReservationDate < DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("ReservationDate", "No puedes seleccionar una fecha pasada.");
            }
            var query = new GetReservationsByDateAndShiftQuery(reservation);
            var reservations = await _mediator.Send(query, cancellationToken);

            if (reservations.Count != null)
            {

                TempData["Reservations"] = JsonSerializer.Serialize(reservations);
                TempData["DateShift"] = JsonSerializer.Serialize(reservation);

                return RedirectToAction("SelectTable");
            }

            return View();
        }

        [Authorize(Roles = "Usuario")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SelectTable()
        {
            var tables = await _mediator.Send(new GetTableLocationQuerie());

            if (TempData["Reservations"] != null)
            {
                ViewBag.Reservations = JsonSerializer.Deserialize<List<ReservationResponse>>(TempData["Reservations"].ToString());
            }
            else
            {
                return RedirectToAction(nameof(SearchReservations));
            }
            if (TempData["DateShift"] != null)
            {
                ViewBag.DateShift = JsonSerializer.Deserialize<ReservationByFilterRequest>(TempData["DateShift"].ToString());
            }
            else
            {
                return RedirectToAction(nameof(SearchReservations));
            }

            return View(tables);
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost]
        public async Task<IActionResult> AddReservation(CreateReservationRequest request, CancellationToken cancellationToken)
        {
            // Log para verificar los valores recibidos
            Console.WriteLine($"TableNumber: {request.TableNumber}, ReservationDate: {request.ReservationDate}, ReservateShift: {request.ReservateShift}");

            request.CreatedDate = DateTime.Now;
            request.UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);

            var send = new CreateReservationCommand(request);
            var command = await _mediator.Send(send, cancellationToken);

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(MisReservaciones));
            }

            return Json(new { success = false });
        }


        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> MisReservaciones(CancellationToken cancellation)
        {
            var ID = new ReservationsByIdResquest
            {
                Id = Convert.ToInt32(User.FindFirst("Id")?.Value)
            };

            var query = new GetReservationQuerie(ID);
            var reservations = await _mediator.Send(query, cancellation);
            
            if (reservations.Any())
            {
                return View("MisReservaciones", reservations);
            }
            var listavacia = new List<ReservationResponse>();
            return View("MisReservaciones", listavacia);
        }


        /// <summary>
        /// Actualiza la compensación de un usuario cuando alcanza 10 reservaciones.
        /// </summary>
        /// <param name="id">ID del usuario a compensar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Resultado JSON indicando si la operación fue exitosa.</returns>
        [Authorize(Roles = "Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AplicarCompensacion([FromBody] int id, CancellationToken cancellationToken)
        {
            try
            {
                // Log para debugging
                Console.WriteLine($"Aplicando compensación para usuario ID: {id}");
                
                var command = new UpdateCompensationCommand(id, true);
                var result = await _mediator.Send(command, cancellationToken);

                if (result)
                {
                    Console.WriteLine($"Compensación aplicada exitosamente para usuario ID: {id}");
                    return Json(new { success = true, message = "Compensación aplicada exitosamente" });
                }

                Console.WriteLine($"No se pudo aplicar la compensación para usuario ID: {id}");
                return Json(new { success = false, message = "No se pudo aplicar la compensación" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al aplicar compensación: {ex.Message}");
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Devuelve las mesas actualizadas con el estado de reservación para AJAX
        /// </summary>
        [Authorize(Roles = "Usuario")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTablesPartial(string reservationDate, string reservateShift, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"[GetTablesPartial] Fecha recibida: {reservationDate}, Turno: {reservateShift}");
                
                // Crear el request con los parámetros recibidos
                var request = new ReservationByFilterRequest
                {
                    ReservationDate = DateOnly.Parse(reservationDate),
                    ReservateShift = reservateShift
                };
                
                // Obtener todas las ubicaciones de mesas
                var tables = await _mediator.Send(new GetTableLocationQuerie());
                
                // Obtener las reservaciones para la fecha y turno específicos
                var query = new GetReservationsByDateAndShiftQuery(request);
                var reservations = await _mediator.Send(query, cancellationToken);
                
                Console.WriteLine($"[GetTablesPartial] Mesas encontradas: {tables.Count}, Reservaciones: {reservations.Count}");
                
                // Preparar los datos para la vista parcial
                ViewBag.Reservations = reservations;
                ViewBag.DateShift = request;
                
                return PartialView("_TablesPartial", tables);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetTablesPartial] Error: {ex.Message}");
                Console.WriteLine($"[GetTablesPartial] StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { error = "Error al obtener las mesas", details = ex.Message });
            }
        }
    }
}
