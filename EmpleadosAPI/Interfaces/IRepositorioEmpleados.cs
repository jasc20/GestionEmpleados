using EmpleadosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpleadosAPI.Interfaces
{
    public interface IRepositorioEmpleados
    {
        Task<IEnumerable<Empleado>> ObtenerTodosAsync();
        Task<Empleado> ObtenerPorIdAsync(int id);
        Task<Empleado> AgregarAsync(Empleado empleado);
        Task<Empleado> ActualizarAsync(Empleado empleado);
        Task<bool> EliminarAsync(int id);
    }
}

