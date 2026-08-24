using CRUD.Entidades;
using CRUD.Infraestrcture.Context;

namespace CRUD.Repositorio.Alumno
{
    public class AlumnoCommand
    {
        private readonly alumnosContext _context;

        public AlumnoCommand(alumnosContext context)
        {
            _context = context;
        }

        public async Task<Alumnos> CreatePersonaAsync(Alumnos alumno)
        {
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return alumno;
        }

        public async Task<Alumnos?> UpdatePersonaAsync(int id, Alumnos alumno)
        {
            var entity = await _context.Alumnos.FindAsync(id);
            if (entity == null) return null;

            entity.Nombres = alumno.Nombres;
            entity.Apellidos = alumno.Apellidos;
            entity.Edad = alumno.Edad;
            entity.Fecha = alumno.Fecha;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeletePersonaAsync(int id)
        {
            var entity = await _context.Alumnos.FindAsync(id);
            if (entity == null) return false;

            _context.Alumnos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

