using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data; // namespace do seu DbContext
using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Controllers
{
    public class FarmaceuticoController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Construtor com injeção de dependência
        public FarmaceuticoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Farmaceutico")]
        public async Task<IActionResult> BuscarPrescricao(string codigo)
        {
            var prescricao = await _context.MedicamentoProntuario
                .Include(m => m.Prontuario)
                    .ThenInclude(p => p.Paciente)
                .Include(m => m.Prontuario.Medico)
                .Include(m => m.Medicamento)
                .FirstOrDefaultAsync(m => m.CodigoPrescricao == codigo);

            if (prescricao == null)
                return NotFound();

            return View(prescricao);
        }

        [Authorize(Roles = "Farmaceutico")]
        [HttpPost]
        public async Task<IActionResult> DarBaixa(int id, PrescricaoStatus novoStatus)
        {
            var prescricao = await _context.MedicamentoProntuario.FindAsync(id);
            if (prescricao == null || prescricao.Status != PrescricaoStatus.Pendente)
                return BadRequest("Prescrição inválida ou já utilizada.");

            prescricao.Status = novoStatus;
            prescricao.DataBaixa = DateTime.UtcNow;

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            prescricao.FarmaceuticoId = Guid.Parse(userId);

            _context.Update(prescricao);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Prescrição atualizada com sucesso!";
            return RedirectToAction("BuscarPrescricao", new { codigo = prescricao.CodigoPrescricao });
        }

        public static string GerarCodigoPrescricao(string nomePaciente)
        {
            var iniciais = new string(nomePaciente
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p[0])
                .ToArray())
                .ToUpper();

            var guid = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            return $"{iniciais}-{guid}";
        }
    }
}