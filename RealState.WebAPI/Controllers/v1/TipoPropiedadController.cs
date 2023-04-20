using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealState.Core.Application.Features.PropertyType.Commands.DeleteCategoryById;
using RealState.Core.Application.Features.PropertyType.Commands.UpdateCategory;
using RealState.Core.Application.Features.PropertyType.Queries.GetAllPropertyType;
using RealState.Core.Application.Features.PropertyType.Queries.GetById;
using RealState.Core.Application.ViewModels.Api;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de propiedades")]


    public class TipoPropiedadController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista de Tipos de propiedades",
            Description = "Devuelve un listado con todas las propiedades registradas."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> List()
        {

            try
            {
                return Ok(await Mediator.Send(new GetAllPropertyTypeQuerty()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [SwaggerOperation(
            Summary = "Tipo de propiedad",
            Description = "Devuelve una propiedad en especifica."
        )]
        [Consumes(MediaTypeNames.Application.Json)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]

        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                return Ok(await Mediator.Send(new GetAdTypeByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Nueva propiedad",
            Description = "Permite crear una nueva propiedad."
        )]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromBody] CreatePropertyTypeCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Actualizar",
            Description = "Permine actualizar una propiedad en especifica."
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyTypeCommand command)
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
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Eliminar",
            Description = "Permite eliminar una propiedad en especifica."
        )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeletePropertyTypeByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
