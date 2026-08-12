using BibliotecaFrontend.Domain.DTOs;
using BibliotecaFrontend.Domain.Entities;

namespace BibliotecaFrontend.Infrastructure.Api;

public interface IBibliotecaApiClient
{
    Task<IReadOnlyList<Livro>> ListarLivrosAsync(CancellationToken cancellationToken = default);
    Task<Livro?> BuscarLivroAsync(int id, CancellationToken cancellationToken = default);
    Task<Livro?> CriarLivroAsync(CriarLivroRequest request, CancellationToken cancellationToken = default);
    Task<string?> EmprestarLivroAsync(int id, CancellationToken cancellationToken = default);
    Task<string?> DevolverLivroAsync(int id, CancellationToken cancellationToken = default);
}
