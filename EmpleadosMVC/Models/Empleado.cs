using System;
using System.ComponentModel.DataAnnotations;

namespace EmpleadosMVC.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Los nombres son requeridos.")]
        [StringLength(100, ErrorMessage = "Los nombres no pueden exceder los 100 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos.")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder los 100 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El género es requerido.")]
        [StringLength(1, ErrorMessage = "El género debe ser 'M' o 'F'.")]
        public string Genero { get; set; } // M, F

        [Required(ErrorMessage = "El estado civil es requerido.")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public int Edad
        {
            get
            {
                if (FechaNacimiento == default(DateTime)) return 0;
                int edad = DateTime.Today.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                {
                    edad--;
                }
                return edad;
            }
        }

        [Required(ErrorMessage = "El DPI es requerido.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El DPI debe tener 13 dígitos.")]
        public string Dpi { get; set; }

        [Required(ErrorMessage = "El NIT es requerido.")]
        public string Nit { get; set; }

        public string AfiliacionIgss { get; set; }
        public string AfiliacionIrtra { get; set; }

        [Required(ErrorMessage = "La dirección es requerida.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El salario base es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El salario base debe ser mayor a cero.")]
        public decimal SalarioBase { get; set; }

        [Required(ErrorMessage = "Las bonificaciones son requeridas.")]
        [Range(0, double.MaxValue, ErrorMessage = "Las bonificaciones no pueden ser negativas.")]
        public decimal Bonificaciones { get; set; }

        public decimal SalarioTotal => SalarioBase + Bonificaciones;
    }
}
