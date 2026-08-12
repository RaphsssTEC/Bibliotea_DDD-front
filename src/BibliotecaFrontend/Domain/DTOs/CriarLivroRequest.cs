namespace BibliotecaFrontend.Domain.DTOs;

public sealed class CriarLivroRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
}
