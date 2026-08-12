# BibliotecaFrontend

Frontend MVC em ASP.NET Core 8 para consumir a API `BibliotecaDDD`.

## Arquitetura

- `Domain`: entidades e contratos do domínio do frontend.
- `Application`: casos de uso e serviços de aplicação.
- `Infrastructure`: cliente HTTP responsável pela comunicação com a API.
- `Presentation`: Controllers e Views Razor.
- `wwwroot`: CSS e JavaScript da interface.

A aplicação não acessa banco de dados. O frontend conversa exclusivamente com a API.

## Pré-requisitos

- .NET 8 SDK
- API `BibliotecaDDD` executando

## Executar

1. Inicie a API:
   `dotnet run --project ../BibliotecaDDD/src/BibliotecaDDD`
2. Confira a URL da API em `src/BibliotecaFrontend/appsettings.json`.
3. Execute o frontend:
   `dotnet run --project src/BibliotecaFrontend`

Por padrão, o projeto está configurado para:
`http://localhost:5000/`

Se sua API estiver em outra porta, altere `ApiSettings:BaseUrl`.

## Funcionalidades

- Listar livros
- Cadastrar livro
- Visualizar detalhes
- Emprestar livro
- Devolver livro
- Mensagens de erro da API exibidas na interface
