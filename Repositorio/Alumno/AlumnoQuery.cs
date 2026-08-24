using CRUD.Entidades;
using CRUD.Infraestrcture.Context;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repositorio.Alumno
{
    public class AlumnoQuery
    {
        private readonly alumnosContext _context;

        public AlumnoQuery(alumnosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alumnos>> GetPersonasAsync()
        {
            return await _context.Alumnos.ToListAsync();
        }

        public async Task<Alumnos?> GetByIdAsync(int id)
        {
            return await _context.Alumnos.FindAsync(id);
        }
    }
}

