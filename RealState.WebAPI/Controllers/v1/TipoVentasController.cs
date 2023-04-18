using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Features.Sales_.Commands.DeleteSalesById;
using RealState.Core.Application.Features.Sales_.Queries.GetAllSales;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using RealState.Core.Application.Features.Sales_.Commands.UpdateSales;
using RealState.Core.Application.Features.Sales_.Commands.CreateSales;
using RealState.Core.Application.Features.Sales_.Commands.UpdateCategory;
using RealState.Core.Application.Features.Sales_.Queries.GetById;

namespace RealState.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de ventas")]

    public class TipoVentasController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Listado de tipo de ventas",
            Description = "Devuelve un listado con los tipos de ventas registrado."
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> List()
        {

            try
            {
                return Ok(await Mediator.Send(new GetAllSalesQuerty()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Tipo de venta",
            Description = "Devuelve un tipo de venta especifico."
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetSalesByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Nuevo tipo de venta",
            Description = "Permite crear un nuevo tipo de venta."
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create( [FromBody] CreateSalesCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar",
            Description = "Permite actualizar un tipo de venta en especifico."
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, [FromBody] UpdateSalesCommand command)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Eliminar",
            Description = "Permite eliminar un tipo de venta en especifico."
        )]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteSalesByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
