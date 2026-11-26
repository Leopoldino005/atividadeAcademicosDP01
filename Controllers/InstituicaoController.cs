using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAcademico.Data;
using ProjetoAcademico.Models;

namespace ProjetoAcademico.Controllers;

public class InstituicaoController : Controller
{
    private readonly AcademicoContext _context;

    public InstituicaoController(AcademicoContext context) => _context = context;

    // READ - lista as instituições
    public async Task<IActionResult> Index()
    {
        var instituicoes = await _context.Instituicoes.OrderBy(i => i.Nome).ToListAsync();
        return View(instituicoes);
    }

    // CREATE - exibe o formulário
    [HttpGet]
    public IActionResult Create() => View(new Instituicao());

    // CREATE - grava a instituição
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Endereco")] Instituicao instituicao)
    {
        if (!ModelState.IsValid) return View(instituicao);
        _context.Instituicoes.Add(instituicao);
        await _context.SaveChangesAsync();
        TempData["Mensagem"] = "Instituição cadastrada com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    // READ - detalhes
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.InstituicaoID == id);
        return instituicao is null ? NotFound() : View(instituicao);
    }

    // UPDATE - carrega para edição
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var instituicao = await _context.Instituicoes.FindAsync(id);
        return instituicao is null ? NotFound() : View(instituicao);
    }

    // UPDATE - persiste alterações
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
    {
        if (id != instituicao.InstituicaoID) return NotFound();
        if (!ModelState.IsValid) return View(instituicao);

        try
        {
            _context.Update(instituicao);
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Instituição atualizada com sucesso.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Instituicoes.AnyAsync(i => i.InstituicaoID == instituicao.InstituicaoID)) return NotFound();
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // DELETE - confirmação
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.InstituicaoID == id);
        return instituicao is null ? NotFound() : View(instituicao);
    }

    // DELETE - efetiva a exclusão
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var instituicao = await _context.Instituicoes.FindAsync(id);
        if (instituicao is not null)
        {
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Instituição excluída com sucesso.";
        }
        return RedirectToAction(nameof(Index));
    }
}
