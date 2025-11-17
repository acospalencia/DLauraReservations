using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Locations.Commands.CreateLocation;
using DLaura.BusinessLogic.UseCases.Locations.Commands.UpdateLocation;
using DLaura.BusinessLogic.UseCases.Locations.Queries.GetTableLocation;
using DLaura.BusinessLogic.UseCases.UserTables.Commands.CreateUserTable;
using DLaura.BusinessLogic.UseCases.UserTables.Commands.DeleteUserTable;
using DLaura.DataAcces;
using DLaura.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DLaura.WebApplication.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UserTablesController : Controller
    {
        
        private readonly IMediator _mediator;

        public UserTablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: UserTables
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst(ClaimTypes.Role)?.Value == "Administrador")
            {
                var tables = await _mediator.Send(new GetTableLocationQuerie());

                return View(tables);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> AddTable(CreateUserTableRequest mesa, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var newMesa = new CreateUserTableCommand(mesa);
            var command = await _mediator.Send(newMesa, cancellationToken);

            var location = new CreateLocationrequest
            {
                TableNumber = mesa.TableNumber,
                CoordinateX = "100px",
                CoordinateY = "100px"
            };
            var command2 = new CreateLocationCommand(location);
            await _mediator.Send(command2, cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTablePosition([FromBody] UpdateLocationRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateLocationCommand(request);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(int Id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserTableCommand(Id);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction("Index");

        }

    }
}