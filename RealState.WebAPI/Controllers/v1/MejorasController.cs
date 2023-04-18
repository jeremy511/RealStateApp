using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Features.Upgrates.Commands.DeleteCategoryById;
using RealState.Core.Application.Features.Upgrates.Queries.GetAllUpgrates;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using RealState.Core.Application.Features.Upgrates.Commands.UpdateCategory;
using RealState.Core.Application.Features.Upgrates.Commands.CreateUpgrates;
using RealState.Core.Application.Features.Upgrates.Queries.GetById;

namespace RealState.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]

    public class MejorasController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Listado de Mejoras",
            Description = "Devuelve una lista de todas las mejoras registradas."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]

        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> List()
        {

            try
            {
                return Ok(await Mediator.Send(new GetAllUpgratesQuerty()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Mejora",
            Description = "Devuelve una mejora especificada por ID"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]

        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetUpgratesByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creacion de nueva mejora",
            Description = "permite crear una nueva mejora en el sistema"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromBody] CreateUpgratesCommand command)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualizar mejora",
            Description = "Permite actualizar una mejora registrada en el sistema"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpgratesUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, [FromBody] UpdateUpgratesCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Eliminar Mejora",
            Description = "Permite eliminar una mejora en especifica"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteUpgratesByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
