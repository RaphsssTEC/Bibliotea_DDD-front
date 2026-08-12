using BibliotecaFrontend.Domain.DTOs;
using BibliotecaFrontend.Domain.Entities;

namespace BibliotecaFrontend.Application.Services;

public interface ILivroAppService
{
    Task<IReadOnlyList<Livro>> ListarAsync(CancellationToken cancellationToken = default);
    Task<Livro?> BuscarAsync(int id, CancellationToken cancellationToken = default);
    Task<Livro?> CriarAsync(CriarLivroRequest request, CancellationToken cancellationToken = default);
    Task<string?> EmprestarAsync(int id, CancellationToken cancellationToken = default);
    Task<string?> DevolverAsync(int id, CancellationToken cancellationToken = default);
}
