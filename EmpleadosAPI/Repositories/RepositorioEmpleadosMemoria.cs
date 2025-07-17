using EmpleadosAPI.Interfaces;
using EmpleadosAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpleadosAPI.Repositories
{
    public class RepositorioEmpleadosMemoria : IRepositorioEmpleados
    {
        private readonly List<Empleado> _empleados;
        private int _nextId = 1;

        public RepositorioEmpleadosMemoria()
        {
            _empleados = new List<Empleado>
            {
                // Datos de ejemplo para empezar
                new Empleado {
                    Id = _nextId++, Nombres = "Juan", Apellidos = "Pérez", Genero = "M", EstadoCivil = "Soltero", FechaNacimiento = new DateTime(1990, 5, 15),
                    Dpi = "1234567890101", Nit = "1234567-8", AfiliacionIgss = "12345", AfiliacionIrtra = "67890", Direccion = "Calle Falsa 123",
                    SalarioBase = 5000.00M, Bonificaciones = 500.00M
                },
                new Empleado {
                    Id = _nextId++, Nombres = "María", Apellidos = "González", Genero = "F", EstadoCivil = "Casada", FechaNacimiento = new DateTime(1985, 10, 20),
                    Dpi = "9876543210987", Nit = "8765432-1", AfiliacionIgss = "54321", AfiliacionIrtra = "09876", Direccion = "Avenida Siempre Viva 456",
                    SalarioBase = 6000.00M, Bonificaciones = 750.00M
                }
            };
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
        {
            return await Task.FromResult(_empleados.AsEnumerable());
        }

        public async Task<Empleado> ObtenerPorIdAsync(int id)
        {
            return await Task.FromResult(_empleados.FirstOrDefault(e => e.Id == id));
        }

        public async Task<Empleado> AgregarAsync(Empleado empleado)
        {
            if (empleado == null) return null;
            empleado.Id = _nextId++;
            _empleados.Add(empleado);
            return await Task.FromResult(empleado);
        }

        public async Task<Empleado> ActualizarAsync(Empleado empleado)
        {
            if (empleado == null) return null;

            var existingEmpleado = _empleados.FirstOrDefault(e => e.Id == empleado.Id);
            if (existingEmpleado == null) return null;

            // Actualizar todas las propiedades
            existingEmpleado.Nombres = empleado.Nombres;
            existingEmpleado.Apellidos = empleado.Apellidos;
            existingEmpleado.Genero = empleado.Genero;
            existingEmpleado.EstadoCivil = empleado.EstadoCivil;
            existingEmpleado.FechaNacimiento = empleado.FechaNacimiento;
            existingEmpleado.Dpi = empleado.Dpi;
            existingEmpleado.Nit = empleado.Nit;
            existingEmpleado.AfiliacionIgss = empleado.AfiliacionIgss;
            existingEmpleado.AfiliacionIrtra = empleado.AfiliacionIrtra;
            existingEmpleado.Direccion = empleado.Direccion;
            existingEmpleado.SalarioBase = empleado.SalarioBase;
            existingEmpleado.Bonificaciones = empleado.Bonificaciones;

            return await Task.FromResult(existingEmpleado);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var empleadoToRemove = _empleados.FirstOrDefault(e => e.Id == id);
            if (empleadoToRemove == null) return await Task.FromResult(false);

            _empleados.Remove(empleadoToRemove);
            return await Task.FromResult(true);
        }
    }
}
