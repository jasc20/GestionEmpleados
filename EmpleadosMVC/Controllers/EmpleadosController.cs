using Microsoft.AspNetCore.Mvc;
using EmpleadosMVC.Models;
using Newtonsoft.Json; // Necesitarás instalar este paquete
using System.Text;

namespace EmpleadosMVC.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public EmpleadosController(IConfiguration configuration)
        {
            // Se inyecta IConfiguration para obtener la URL de la API desde appsettings.json
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] + "/api/empleados";
            _httpClient = new HttpClient();
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = new List<Empleado>();
            HttpResponseMessage response = await _httpClient.GetAsync(_apiBaseUrl);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                empleados = JsonConvert.DeserializeObject<List<Empleado>>(data);
            }
            return View(empleados);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empleado empleado = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                empleado = JsonConvert.DeserializeObject<Empleado>(data);
            }

            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombres,Apellidos,Genero,EstadoCivil,FechaNacimiento,Dpi,Nit,AfiliacionIgss,AfiliacionIrtra,Direccion,SalarioBase,Bonificaciones")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(empleado);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_apiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar errores de la API, por ejemplo, leer el contenido del error
                    string errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al crear empleado: {response.ReasonPhrase}. Detalles: {errorContent}");
                }
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empleado empleado = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                empleado = JsonConvert.DeserializeObject<Empleado>(data);
            }

            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,Genero,EstadoCivil,FechaNacimiento,Dpi,Nit,AfiliacionIgss,AfiliacionIrtra,Direccion,SalarioBase,Bonificaciones")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(empleado);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al actualizar empleado: {response.ReasonPhrase}. Detalles: {errorContent}");
                }
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empleado empleado = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                empleado = JsonConvert.DeserializeObject<Empleado>(data);
            }

            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error al eliminar empleado: {response.ReasonPhrase}. Detalles: {errorContent}");
                // Si falla la eliminación, podrías redirigir a la vista de detalles con un mensaje de error
                return RedirectToAction(nameof(Details), new { id = id });
            }
        }
    }
}
