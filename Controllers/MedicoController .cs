using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;
using System.Linq;
using System.Threading.Tasks;

namespace projetoTP3_A2.Controllers
{
    [Authorize(Policy = "MedicoPolicy")]
    public class MedicoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicoController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Medico/Pacientes?query=joao
        public IActionResult Pacientes(string query, int page = 1, int pageSize = 10)
        {
            var pacientes = _userManager.Users
                .Where(u => u.Perfil == Perfis.Paciente);

            if (!string.IsNullOrWhiteSpace(query))
            {
                pacientes = pacientes.Where(u => u.Nome.Contains(query));
            }

            var totalCount = pacientes.Count();

            var lista = pacientes
                .OrderBy(u => u.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new PacienteListItem { Id = u.Id, Nome = u.Nome, Email = u.Email })
                .ToList();

            ViewBag.TotalCount = totalCount;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(lista);
        }

        // GET: /Medico/Prontuario/{id}
        public async Task<IActionResult> Prontuario(Guid id)
        {
            var paciente = await _userManager.FindByIdAsync(id.ToString());
            if (paciente == null || paciente.Perfil != Perfis.Paciente)
                return NotFound();

            // TODO: carregar histórico de prontuários e exibir
            return View(paciente); // por enquanto, pode mostrar dados básicos
        }
    }

    public class PacienteListItem
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}