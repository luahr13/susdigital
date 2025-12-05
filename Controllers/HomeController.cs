using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projetoTP3_A2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //ADM
        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Área do Administrador";
            return View();
        }

        //ADM
        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult MedicamentoHome()
        {
            ViewData["Title"] = "Área do Administrador";
            return View();
        }

        //ADM
        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult AlergiaHome()
        {
            ViewData["Title"] = "Área do Administrador";
            return View(); // procura Views/Home/PacienteHome.cshtml
        }

        //Farmaceutico
        [Authorize(Policy = "FarmaceuticoPolicy")]
        public IActionResult FarmaceuticoHome()
        {
            ViewData["Title"] = "Área do Farmacêutico";
            return View(); // procura Views/Home/FarmaceuticoHome.cshtml
        }

        //Medico
        [Authorize(Policy = "MedicoPolicy")]
        public IActionResult MedicoHome()
        {
            ViewData["Title"] = "Área do Médico";
            return View(); // procura Views/Home/MedicoHome.cshtml
        }

        //Paciente
        [Authorize(Policy = "PacientePolicy")]
        public IActionResult PacienteHome()
        {
            ViewData["Title"] = "Área do Paciente";
            return View(); // procura Views/Home/PacienteHome.cshtml
        }
    }
}