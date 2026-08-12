using BibliotecaFrontend.Domain.DTOs;
using BibliotecaFrontend.Domain.Entities;
using BibliotecaFrontend.Infrastructure.Api;

namespace BibliotecaFrontend.Application.Services;

public sealed class LivroAppService : ILivroAppService
{
    private readonly IBibliotecaApiClient _apiClient;

    public LivroAppService(IBibliotecaApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task<IReadOnlyList<Livro>> ListarAsync(
        CancellationToken cancellationToken = default)
        => _apiClient.ListarLivrosAsync(cancellationToken);

    public Task<Livro?> BuscarAsync(
        int id,
        CancellationToken cancellationToken = default)
        => _apiClient.BuscarLivroAsync(id, cancellationToken);

    public Task<Livro?> CriarAsync(
        CriarLivroRequest request,
        CancellationToken cancellationToken = default)
        => _apiClient.CriarLivroAsync(request, cancellationToken);

    public Task<string?> EmprestarAsync(
        int id,
        CancellationToken cancellationToken = default)
        => _apiClient.EmprestarLivroAsync(id, cancellationToken);

    public Task<string?> DevolverAsync(
        int id,
        CancellationToken cancellationToken = default)
        => _apiClient.DevolverLivroAsync(id, cancellationToken);
}
