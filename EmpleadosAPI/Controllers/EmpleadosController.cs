using EmpleadosAPI.Models;
using EmpleadosAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {

        private readonly IRepositorioEmpleados _repositorioEmpleados;

        public EmpleadosController(IRepositorioEmpleados repositorioEmpleados)
        {
            _repositorioEmpleados = repositorioEmpleados;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            var empleados = await _repositorioEmpleados.ObtenerTodosAsync();
            return Ok(empleados); // Devuelve 200 OK con la lista de empleados
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _repositorioEmpleados.ObtenerPorIdAsync(id);

            if (empleado == null)
            {
                return NotFound(); // Devuelve 404 Not Found si no se encuentra
            }

            return Ok(empleado); // Devuelve 200 OK con el empleado
        }

        // POST: api/Empleados
        // Para enviar un JSON al cuerpo de la solicitud: { "nombres": "Nuevo", ... }
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 Bad Request con errores de validación
            }

            var nuevoEmpleado = await _repositorioEmpleados.AgregarAsync(empleado);
            // Devuelve 201 Created y la URL para acceder al nuevo recurso
            return CreatedAtAction(nameof(GetEmpleado), new { id = nuevoEmpleado.Id }, nuevoEmpleado);
        }

        // PUT: api/Empleados/5
        // Para enviar un JSON al cuerpo de la solicitud: { "id": 5, "nombres": "Actualizado", ... }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, [FromBody] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del empleado en el cuerpo."); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 Bad Request con errores de validación
            }

            var updatedEmpleado = await _repositorioEmpleados.ActualizarAsync(empleado);
            if (updatedEmpleado == null)
            {
                return NotFound(); // Devuelve 404 Not Found si no se encuentra para actualizar
            }

            return NoContent(); // Devuelve 204 No Content si la actualización fue exitosa
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var result = await _repositorioEmpleados.EliminarAsync(id);

            if (!result)
            {
                return NotFound(); // Devuelve 404 Not Found si no se encuentra
            }

            return NoContent(); // Devuelve 204 No Content si la eliminación fue exitosa
        }
    }
}
