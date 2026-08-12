namespace BibliotecaFrontend.Domain.Entities;

public sealed class Livro
{
    public int Id { get; init; }
    public string Titulo { get; init; } = string.Empty;
    public string Autor { get; init; } = string.Empty;
    public bool Disponivel { get; init; }
}
