using BibliotecaFrontend.Application.Services;
using BibliotecaFrontend.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaFrontend.Presentation.Controllers;

public sealed class LivrosController : Controller
{
    private readonly ILivroAppService _livroAppService;

    public LivrosController(ILivroAppService livroAppService)
    {
        _livroAppService = livroAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var livros = await _livroAppService.ListarAsync(cancellationToken);
            return View(livros);
        }
        catch (Exception ex)
        {
            TempData["Erro"] = ex.Message;
            return View(Array.Empty<Domain.Entities.Livro>());
        }
    }

    [HttpGet]
    public IActionResult Criar()
    {
        return View(new CriarLivroRequest());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(
        CriarLivroRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(request);

        try
        {
            await _livroAppService.CriarAsync(request, cancellationToken);
            TempData["Sucesso"] = "Livro cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(request);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Emprestar(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var mensagem = await _livroAppService.EmprestarAsync(id, cancellationToken);
            TempData["Sucesso"] = mensagem ?? "Livro emprestado com sucesso.";
        }
        catch (Exception ex)
        {
            TempData["Erro"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Devolver(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var mensagem = await _livroAppService.DevolverAsync(id, cancellationToken);
            TempData["Sucesso"] = mensagem ?? "Livro devolvido com sucesso.";
        }
        catch (Exception ex)
        {
            TempData["Erro"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Detalhes(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var livro = await _livroAppService.BuscarAsync(id, cancellationToken);

            if (livro is null)
                return NotFound();

            return View(livro);
        }
        catch (Exception ex)
        {
            TempData["Erro"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
