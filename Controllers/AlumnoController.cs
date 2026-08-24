using CRUD.Entidades;
using CRUD.Repositorio.Alumno;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoQuery _query;
        private readonly AlumnoCommand _command;

        public AlumnoController(AlumnoQuery query, AlumnoCommand command)
        {
            _query = query;
            _command = command;
        }

        // 1. GET: api/Alumno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumnos>>> GetAll()
        {
            var result = await _query.GetPersonasAsync();
            return Ok(result);
        }

        // 1b. GET: api/Alumno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumnos>> GetById(int id)
        {
            var result = await _query.GetByIdAsync(id);
            if (result == null) return NotFound(new { message = $"Alumno con ID {id} no encontrado" });
            return Ok(result);
        }

        // 2. POST: api/Alumno
        [HttpPost]
        public async Task<ActionResult<Alumnos>> Create([FromBody] Alumnos dto)
        {
            if (dto == null) return BadRequest();
            var created = await _command.CreatePersonaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Idalumnos }, created);
        }

        // 3. PUT: api/Alumno/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Alumnos>> Update(int id, [FromBody] Alumnos dto)
        {
            if (dto == null) return BadRequest();
            var updated = await _command.UpdatePersonaAsync(id, dto);
            if (updated == null) return NotFound(new { message = $"Alumno con ID {id} no encontrado" });
            return Ok(updated);
        }

        // 4. DELETE: api/Alumno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _command.DeletePersonaAsync(id);
            if (!success) return NotFound(new { message = $"Alumno con ID {id} no encontrado" });
            return Ok(new { message = $"Alumno con ID {id} eliminado correctamente" });
        }
    }
}
