using System.Net;
using System.Net.Http.Json;
using BibliotecaFrontend.Domain.DTOs;
using BibliotecaFrontend.Domain.Entities;

namespace BibliotecaFrontend.Infrastructure.Api;

public sealed class BibliotecaApiClient : IBibliotecaApiClient
{
    private readonly HttpClient _httpClient;

    public BibliotecaApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<Livro>> ListarLivrosAsync(
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync("api/livros", cancellationToken);

        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<List<Livro>>(cancellationToken)
               ?? [];
    }

    public async Task<Livro?> BuscarLivroAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync($"api/livros/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<Livro>(cancellationToken);
    }

    public async Task<Livro?> CriarLivroAsync(
        CriarLivroRequest request,
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync(
            "api/livros", request, cancellationToken);

        await EnsureSuccessAsync(response);

        return await response.Content.ReadFromJsonAsync<Livro>(cancellationToken);
    }

    public async Task<string?> EmprestarLivroAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await ExecutarAcaoAsync(
            $"api/livros/{id}/emprestar", cancellationToken);
    }

    public async Task<string?> DevolverLivroAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await ExecutarAcaoAsync(
            $"api/livros/{id}/devolver", cancellationToken);
    }

    private async Task<string?> ExecutarAcaoAsync(
        string endpoint,
        CancellationToken cancellationToken)
    {
        using var response = await _httpClient.PostAsync(
            endpoint, null, cancellationToken);

        await EnsureSuccessAsync(response);

        var result = await response.Content.ReadFromJsonAsync<ApiErrorResponse>(
            cancellationToken);

        return result?.Mensagem;
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        string? message = null;

        try
        {
            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            message = error?.Erro ?? error?.Mensagem;
        }
        catch
        {
            // Mantém uma mensagem genérica caso a API não retorne JSON.
        }

        throw new HttpRequestException(
            message ?? $"A API retornou HTTP {(int)response.StatusCode}.");
    }
}
