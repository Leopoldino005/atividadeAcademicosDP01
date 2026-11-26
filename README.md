# Projeto Acadêmico — Alunos e Instituições

Aplicação **ASP.NET Core MVC + Entity Framework Core + SQL Server**, organizada para abrir no Visual Studio Code. Implementa manutenção (CRUD) de Alunos e Instituições.

## O que está implementado

- **Alunos:** listar, cadastrar, consultar detalhes, editar e excluir.
- **Instituições:** listar, cadastrar, consultar detalhes, editar e excluir.
- `AcademicoContext : DbContext` com os `DbSet<Aluno>` e `DbSet<Instituicao>`.
- Injeção de dependência do contexto no `Program.cs`.
- Validação dos campos, proteção antifalsificação nos formulários POST e mensagens de confirmação.
- Entity Framework Core com provider SQL Server, seguindo a abordagem dos slides.

## Requisitos

1. .NET 8 SDK.
2. Visual Studio Code com a extensão **C# Dev Kit** (recomendado).
3. SQL Server LocalDB (normalmente instalado com Visual Studio) ou uma instância SQL Server acessível.

> A string de conexão inicial usa `(localdb)\\MSSQLLocalDB`, adequada ao SQL Server LocalDB no Windows. Se sua instância tiver outro nome, ajuste `ConnectionStrings:AcademicoDbConnection` em `appsettings.json`.

## Como abrir e executar no VS Code

1. Extraia o ZIP.
2. No VS Code, clique em **File > Open Folder** e selecione a pasta `ProjetoAcademico` (a pasta que contém `ProjetoAcademico.csproj`).
3. Abra **Terminal > New Terminal**.
4. Restaure os pacotes:

   ```bash
   dotnet restore
   ```

5. Instale a ferramenta de migrations do EF Core (uma vez por máquina):

   ```bash
   dotnet tool install --global dotnet-ef --version 8.0.22
   ```

   Se já estiver instalada, atualize-a com `dotnet tool update --global dotnet-ef --version 8.0.22`.

6. Crie a migration inicial e aplique-a ao banco:

   ```bash
   dotnet ef migrations add Inicial
   dotnet ef database update
   ```

   Esses comandos criam as tabelas `Alunos` e `Instituicoes` no banco `AcademicoDb` definido na connection string. A pasta `Migrations` será criada automaticamente.

7. Execute a aplicação:

   ```bash
   dotnet run
   ```

8. Abra o endereço mostrado no terminal (com o perfil incluído, normalmente `http://localhost:5080`).

## Se você usa outra instância SQL Server

Edite `appsettings.json`. Exemplo para SQL Server Express:

```json
{
  "ConnectionStrings": {
    "AcademicoDbConnection": "Server=.\\SQLEXPRESS;Database=AcademicoDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

Confirme que a instância está iniciada e que seu usuário do Windows tem permissão para criar/alterar o banco.

## Estrutura principal

```text
ProjetoAcademico/
├── Controllers/
│   ├── AlunoController.cs
│   ├── HomeController.cs
│   └── InstituicaoController.cs
├── Data/
│   └── AcademicoContext.cs
├── Models/
│   ├── Aluno.cs
│   └── Instituicao.cs
├── Views/
│   ├── Aluno/             # Index, Create, Edit, Details, Delete
│   ├── Home/
│   ├── Instituicao/       # Index, Create, Edit, Details, Delete
│   └── Shared/
├── Program.cs
├── appsettings.json
└── ProjetoAcademico.csproj
```

## Campos dos modelos

- **Aluno:** `AlunoID`, `Nome`, `Email`, `Telefone`, `Endereco`, `Complemento`, `Bairro`, `Municipio`, `Uf`, `Cep`.
- **Instituicao:** `InstituicaoID`, `Nome`, `Endereco`.

A classe `Instituicao` segue o modelo do último slide (página 30). Os campos de Aluno seguem o formulário apresentado nos slides do CRUD (páginas 23 e 25).

## Observação sobre migrations

A migration não foi pré-gerada porque depende do ambiente e da instância SQL Server da máquina em que o projeto será executado. Os comandos acima geram a migration a partir dos modelos e do `AcademicoContext` e aplicam o esquema ao banco.
