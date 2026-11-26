using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAcademico.Data;
using ProjetoAcademico.Models;

namespace ProjetoAcademico.Controllers;

public class AlunoController : Controller
{
    private readonly AcademicoContext _context;

    public AlunoController(AcademicoContext context) => _context = context;

    // READ - lista os alunos
    public async Task<IActionResult> Index()
    {
        var alunos = await _context.Alunos.OrderBy(a => a.Nome).ToListAsync();
        return View(alunos);
    }

    // CREATE - exibe o formulário
    [HttpGet]
    public IActionResult Create() => View(new Aluno());

    // CREATE - grava o novo aluno
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Email,Telefone,Endereco,Complemento,Bairro,Municipio,Uf,Cep")] Aluno aluno)
    {
        if (!ModelState.IsValid) return View(aluno);

        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
        TempData["Mensagem"] = "Aluno cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    // READ - detalhes de um aluno
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.AlunoID == id);
        return aluno is null ? NotFound() : View(aluno);
    }

    // UPDATE - carrega o aluno para edição
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var aluno = await _context.Alunos.FindAsync(id);
        return aluno is null ? NotFound() : View(aluno);
    }

    // UPDATE - salva as alterações
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AlunoID,Nome,Email,Telefone,Endereco,Complemento,Bairro,Municipio,Uf,Cep")] Aluno aluno)
    {
        if (id != aluno.AlunoID) return NotFound();
        if (!ModelState.IsValid) return View(aluno);

        try
        {
            _context.Update(aluno);
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Aluno atualizado com sucesso.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Alunos.AnyAsync(a => a.AlunoID == aluno.AlunoID)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // DELETE - confirmação
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.AlunoID == id);
        return aluno is null ? NotFound() : View(aluno);
    }

    // DELETE - efetiva a exclusão
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno is not null)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Aluno excluído com sucesso.";
        }
        return RedirectToAction(nameof(Index));
    }
}
