using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace projetoTP3_A2.Controllers
{
    public class ProntuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProntuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prontuario/Create
        public IActionResult Create()
        {
            var pacientes = _context.Set<Paciente>()
                .Select(p => new { p.Id, Display = p.Nome + " — " + p.Email })
                .ToList();

            var medicamentos = _context.Medicamento
                .Select(m => new { m.Id, Display = m.Nome })
                .ToList();

            ViewData["PacienteId"] = new SelectList(pacientes, "Id", "Display");
            ViewData["MedicamentoId"] = new SelectList(medicamentos, "Id", "Display");

            return View();
        }

        // POST: Prontuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PacienteId,Observacoes")] Prontuario prontuario,
            int[] medicamentosSelecionados,
            string[] dosagens,
            string[] frequencias,
            string[] observacoesMed,
            string[] nomesExame,
            string[] observacoesExame,
            IFormFile[] arquivos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            prontuario.MedicoId = Guid.Parse(userId);

            ModelState.Remove(nameof(Prontuario.Medico));
            ModelState.Remove(nameof(Prontuario.Paciente));

            if (ModelState.IsValid)
            {
                prontuario.Id = Guid.NewGuid();
                prontuario.CriadoEm = DateTime.Now;

                _context.Add(prontuario);

                // Vincular medicamentos
                if (medicamentosSelecionados != null)
                {
                    for (int i = 0; i < medicamentosSelecionados.Length; i++)
                    {
                        var mp = new MedicamentoProntuario
                        {
                            ProntuarioId = prontuario.Id,
                            MedicamentoId = medicamentosSelecionados[i],
                            Dosagem = dosagens.Length > i ? dosagens[i] : "",
                            Frequencia = frequencias.Length > i ? frequencias[i] : "",
                            Observacoes = observacoesMed.Length > i ? observacoesMed[i] : "",
                            Status = PrescricaoStatus.Pendente,
                            CodigoPrescricao = FarmaceuticoController.GerarCodigoPrescricao(
                                prontuario.Paciente?.Nome ?? "PACIENTE"
                            )
                        };

                        _context.MedicamentoProntuario.Add(mp);
                    }
                }

                // Salvar arquivos
                if (arquivos != null && arquivos.Length > 0)
                {
                    foreach (var file in arquivos)
                    {
                        var caminhoRelativo = Path.Combine("uploads", Guid.NewGuid() + Path.GetExtension(file.FileName));
                        var caminhoAbsoluto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativo);

                        using (var stream = new FileStream(caminhoAbsoluto, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var arq = new ArquivoProntuario
                        {
                            Id = Guid.NewGuid(),
                            ProntuarioId = prontuario.Id,
                            NomeArquivo = file.FileName,
                            Caminho = caminhoRelativo,
                            UploadEm = DateTime.Now
                        };
                        _context.ArquivoProntuario.Add(arq);
                    }
                }

                // Salvar exames
                if (nomesExame != null && nomesExame.Length > 0)
                {
                    for (int i = 0; i < nomesExame.Length; i++)
                    {
                        var exame = new Exame
                        {
                            Id = Guid.NewGuid(),
                            ProntuarioId = prontuario.Id,
                            Nome = nomesExame[i],
                            Observacoes = observacoesExame.Length > i ? observacoesExame[i] : "",
                            Status = ExameStatus.Pendente,
                            SolicitadoEm = DateTime.Now
                        };
                        _context.Exames.Add(exame);
                    }
                }

                // Persistir tudo
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexMedico));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Erro ao salvar: " + ex.Message);
                    Console.WriteLine("Inner: " + ex.InnerException?.Message);
                    ModelState.AddModelError("", "Erro ao salvar: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }

            // Se deu erro, recarrega lista de pacientes e volta para view
            var pacientes = _context.Set<Paciente>()
                .Select(p => new { p.Id, Display = p.Nome + " — " + p.Email })
                .ToList();

            ViewData["PacienteId"] = new SelectList(pacientes, "Id", "Display", prontuario.PacienteId);
            return View(prontuario);
        }

        // GET: Prontuario/IndexMedico
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> IndexMedico(ProntuarioStatus? status)
        {
            var medicoGuid = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var query = _context.Prontuario
                .Where(p => p.MedicoId == medicoGuid)
                .Include(p => p.Paciente)
                .AsQueryable();

            // aplica filtro apenas se informado
            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            var prontuarios = await query
                .OrderByDescending(p => p.CriadoEm)
                .ToListAsync();

            return View("IndexMedico", prontuarios);
        }

        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> IndexPaciente(ProntuarioStatus? status)
        {
            var pacienteId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var query = _context.Prontuario
                .Where(p => p.PacienteId == pacienteId)
                .Include(p => p.Medico)
                .AsQueryable();

            // aplica filtro apenas se informado
            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            var prontuarios = await query
                .OrderByDescending(p => p.CriadoEm)
                .ToListAsync();

            return View("IndexPaciente", prontuarios);
        }

        // GET: Prontuario/Details/5
        [Authorize(Roles = "Medico,Paciente")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var prontuario = await _context.Prontuario
                .Include(p => p.Medico)
                .Include(p => p.Paciente)
                .Include(p => p.Medicamentos).ThenInclude(mp => mp.Medicamento)
                .Include(p => p.Arquivos)
                .Include(p => p.Exames).ThenInclude(e => e.Arquivos) // 👈 ESSENCIAL
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prontuario == null)
                return NotFound();

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (User.IsInRole("Medico") && prontuario.MedicoId != userId)
                return Forbid();

            if (User.IsInRole("Paciente") && prontuario.PacienteId != userId)
                return Forbid();

            return View(prontuario);
        }

        // GET: Prontuario/Edit/5
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var medicoId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var prontuario = await _context.Prontuario
                .Include(p => p.Medicamentos).ThenInclude(mp => mp.Medicamento)
                .Include(p => p.Arquivos)
                .Where(p => p.MedicoId == medicoId)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prontuario == null) return Forbid();

            var pacientes = _context.Set<Paciente>()
                .Select(p => new { p.Id, Display = p.Nome + " — " + p.Email })
                .ToList();

            var medicamentos = _context.Medicamento
                .Select(m => new { m.Id, Display = m.Nome })
                .ToList();

            ViewData["PacienteId"] = new SelectList(pacientes, "Id", "Display", prontuario.PacienteId);
            ViewData["MedicamentoId"] = new SelectList(medicamentos, "Id", "Display");

            return View(prontuario);
        }

        // POST: Prontuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Edit(Guid id, string observacoes,
                                      int[] medicamentosSelecionados,
                                      string[] dosagens,
                                      string[] frequencias,
                                      string[] observacoesMed,
                                      Guid[] arquivosExistentes,      // <-- IDs dos arquivos mantidos
                                      IFormFile[] arquivos)           // <-- novos arquivos
        {
            var medicoId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var prontuarioDb = await _context.Prontuario
                .Include(p => p.Medicamentos)
                .Include(p => p.Arquivos)
                .Where(p => p.MedicoId == medicoId)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prontuarioDb == null) return Forbid();

            // Observações
            prontuarioDb.Observacoes = observacoes;

            // Medicamentos: remove todos e recria com os enviados
            _context.MedicamentoProntuario.RemoveRange(prontuarioDb.Medicamentos);

            if (medicamentosSelecionados != null)
            {
                for (int i = 0; i < medicamentosSelecionados.Length; i++)
                {
                    var novo = new MedicamentoProntuario
                    {
                        ProntuarioId = prontuarioDb.Id,
                        MedicamentoId = medicamentosSelecionados[i],
                        Dosagem = dosagens.Length > i ? dosagens[i] : "",
                        Frequencia = frequencias.Length > i ? frequencias[i] : "",
                        Observacoes = observacoesMed.Length > i ? observacoesMed[i] : ""
                    };
                    _context.MedicamentoProntuario.Add(novo);
                }
            }

            // Arquivos: remover os que não vieram na tabela
            var idsMantidos = (arquivosExistentes ?? Array.Empty<Guid>()).ToHashSet();
            var arquivosParaRemover = prontuarioDb.Arquivos
                .Where(a => !idsMantidos.Contains(a.Id))
                .ToList();

            foreach (var arq in arquivosParaRemover)
            {
                // Remover arquivo físico (opcional, mas recomendado)
                var caminhoAbsoluto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", arq.Caminho ?? "");
                if (System.IO.File.Exists(caminhoAbsoluto))
                {
                    try { System.IO.File.Delete(caminhoAbsoluto); } catch { /* log opcional */ }
                }

                _context.ArquivoProntuario.Remove(arq);
            }

            // Adicionar novos arquivos
            if (arquivos != null && arquivos.Length > 0)
            {
                foreach (var file in arquivos)
                {
                    var caminhoRelativo = Path.Combine("uploads", Guid.NewGuid() + Path.GetExtension(file.FileName));
                    var caminhoAbsoluto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativo);

                    using (var stream = new FileStream(caminhoAbsoluto, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var arq = new ArquivoProntuario
                    {
                        Id = Guid.NewGuid(),
                        ProntuarioId = prontuarioDb.Id,
                        NomeArquivo = file.FileName,
                        Caminho = caminhoRelativo,
                        UploadEm = DateTime.Now
                    };
                    _context.ArquivoProntuario.Add(arq);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexMedico));
        }

        // GET: Prontuario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var medicoId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var prontuario = await _context.Prontuario
                .Include(p => p.Paciente)
                .Include(p => p.Medico)
                .Include(p => p.Exames)
                .Include(p => p.Medicamentos)
                    .ThenInclude(m => m.Medicamento)
                .FirstOrDefaultAsync(p => p.Id == id && p.MedicoId == medicoId);

            if (prontuario == null) return Forbid();

            return View(prontuario);
        }

        // POST: Prontuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medicoId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var prontuario = await _context.Prontuario
                .FirstOrDefaultAsync(p => p.Id == id && p.MedicoId == medicoId);

            if (prontuario == null) return Forbid();

            _context.Prontuario.Remove(prontuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexMedico));
        }

        // POST: Prontuario/UploadExame/5
        [HttpPost]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> UploadExame(Guid id, IFormFile[] arquivosExame)
        {
            var exame = await _context.Exames
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exame == null)
                return NotFound();

            if (arquivosExame != null && arquivosExame.Length > 0)
            {
                var pastaUploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "exames");
                if (!Directory.Exists(pastaUploads))
                    Directory.CreateDirectory(pastaUploads);

                foreach (var file in arquivosExame)
                {
                    var nomeArquivo = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var caminhoRelativo = Path.Combine("uploads", "exames", nomeArquivo);
                    var caminhoAbsoluto = Path.Combine(pastaUploads, nomeArquivo);

                    using (var stream = new FileStream(caminhoAbsoluto, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var arq = new ArquivoExame
                    {
                        Id = Guid.NewGuid(),
                        ExameId = exame.Id,
                        NomeArquivo = file.FileName,
                        Caminho = caminhoRelativo,
                        UploadEm = DateTime.Now,
                        Tipo = Path.GetExtension(file.FileName).Trim('.').ToUpper()
                    };

                    _context.ArquivosExame.Add(arq);

                    Console.WriteLine($"[UploadExame] ExameId usado: {arq.ExameId}, NomeArquivo={arq.NomeArquivo}, Caminho={arq.Caminho}");
                }

                // Atualiza status do exame
                exame.Status = ExameStatus.Realizado;
                exame.ConcluidoEm = DateTime.Now;

                // Atualiza status do prontuário
                var prontuario = await _context.Prontuario
                    .FirstOrDefaultAsync(p => p.Id == exame.ProntuarioId);

                if (prontuario != null)
                {
                    prontuario.Status = ProntuarioStatus.ExameRecebido;
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Erro ao salvar: " + ex.Message);
                    Console.WriteLine("Inner: " + ex.InnerException?.Message);
                    throw;
                }
            }

            return RedirectToAction("Details", new { id = exame.ProntuarioId });
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AprovarExame(Guid id)
        {
            var exame = await _context.Exames
                .Include(e => e.Prontuario)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exame == null)
                return NotFound();

            // Aprova exame
            exame.Status = ExameStatus.Disponivel;
            exame.ValidadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = exame.ProntuarioId });
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> FecharProntuario(Guid id)
        {
            var prontuario = await _context.Prontuario
                .Include(p => p.Exames)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prontuario == null)
                return NotFound();

            // Bloqueia fechamento se houver exames pendentes ou realizados sem validação
            if (prontuario.Exames.Any(e => e.Status == ExameStatus.Pendente || e.Status == ExameStatus.Realizado))
            {
                TempData["Erro"] = "Não é possível fechar o prontuário enquanto houver exames pendentes ou aguardando aprovação.";
                return RedirectToAction("Details", new { id });
            }

            prontuario.Status = ProntuarioStatus.Fechado;
            await _context.SaveChangesAsync();

            return RedirectToAction("IndexMedico");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ReabrirIndex()
        {
            var prontuariosFechados = await _context.Prontuario
                .Include(p => p.Medico)
                .Include(p => p.Paciente)
                .Where(p => p.Status == ProntuarioStatus.Fechado)
                .OrderByDescending(p => p.CriadoEm)
                .ToListAsync();

            return View(prontuariosFechados);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Reabrir(Guid id)
        {
            var prontuario = await _context.Prontuario.FindAsync(id);
            if (prontuario == null)
            {
                return NotFound();
            }

            prontuario.Status = ProntuarioStatus.Reaberto;
            _context.Update(prontuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ReabrirIndex));
        }

        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> EditarExame(Guid id)
        {
            var exame = await _context.Exames.FindAsync(id);
            if (exame == null) return NotFound();
            return View(exame); // criar uma view EditarExame.cshtml
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> EditarExame(Guid id, Exame exameAtualizado)
        {
            if (id != exameAtualizado.Id) return BadRequest();
            _context.Update(exameAtualizado);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = exameAtualizado.ProntuarioId });
        }

        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> RemoverExame(Guid id)
        {
            var exame = await _context.Exames.FindAsync(id);
            if (exame == null) return NotFound();
            _context.Exames.Remove(exame);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = exame.ProntuarioId });
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> AdicionarExame(Guid prontuarioId, string Nome, string Observacoes)
        {
            var prontuario = await _context.Prontuario
                .FirstOrDefaultAsync(p => p.Id == prontuarioId);

            if (prontuario == null)
                return NotFound();

            var exame = new Exame
            {
                Id = Guid.NewGuid(),
                ProntuarioId = prontuarioId,
                Nome = Nome,
                Observacoes = Observacoes,
                Status = ExameStatus.Pendente,
                SolicitadoEm = DateTime.UtcNow
            };

            _context.Exames.Add(exame);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = prontuarioId });
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> NegarExame(Guid id, string observacaoNegacao)
        {
            if (string.IsNullOrWhiteSpace(observacaoNegacao))
            {
                TempData["Erro"] = "É necessário informar o motivo da negação do exame.";
                return RedirectToAction("Details", new { id });
            }

            var exame = await _context.Exames
                .Include(e => e.Prontuario)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exame == null)
                return NotFound();

            // Atualiza status e registra motivo
            exame.Status = ExameStatus.Negado;
            exame.ObservacaoNegacao = observacaoNegacao.Trim();
            exame.ValidadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Exame negado com sucesso.";
            return RedirectToAction("Details", new { id = exame.ProntuarioId });
        }
    }
}