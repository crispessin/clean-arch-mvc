# CleanArchMvc

Este repositório implementa uma aplicação ASP.NET Core utilizando a abordagem Clean Architecture. A aplicação é organizada em camadas bem definidas, separando responsabilidades e garantindo a independência do domínio.

## Estrutura do Projeto

A solução é composta por seis projetos principais:

1. **CleanArchMvc.Domain**: Responsável pelo modelo de domínio, regras de negócio e interfaces.
2. **CleanArchMvc.Application**: Contém as regras de domínio da aplicação, mapeamento, serviços, DTOs e a implementação de CQRS.
3. **CleanArchMvc.Infra.Data**: Implementa o acesso a dados utilizando Entity Framework Core, incluindo contexto, configurações, migrations e repositórios.
4. **CleanArchMvc.IoC**: Gerencia a injeção de dependências e o registro de serviços.
5. **CleanArchMvc.WebUI**: Camada de apresentação baseada em ASP.NET Core MVC, que inclui controllers, views, filtros e view models.
6. **CleanArchMvc.API**: Fornece uma API para interagir com a aplicação, incluindo controllers e models.

![image](https://github.com/user-attachments/assets/2eb3ae4a-c1b0-4649-af73-8c3a148be0d3)

### Relações e Dependências

- **CleanArchMvc.Domain**: Não possui dependências.
- **CleanArchMvc.Application**: Depende de **CleanArchMvc.Domain**.
- **CleanArchMvc.Infra.Data**: Depende de **CleanArchMvc.Domain**.
- **CleanArchMvc.IoC**: Depende de **CleanArchMvc.Domain**, **CleanArchMvc.Application** e **CleanArchMvc.Infra.Data**.
- **CleanArchMvc.WebUI** e **CleanArchMvc.API**: Dependem de **CleanArchMvc.IoC**.

## Fluxo de Trabalho

1. **Apresentação**: O usuário interage com a aplicação através da camada **WebUI** ou **API**.
2. **Serviços de Aplicação**: **WebUI** e **API** chamam serviços da camada **Application** para processar a lógica de negócios.
3. **Regras de Negócio**: A camada **Application** aplica as regras de negócio e utiliza a camada **Domain** para validar as entidades.
4. **Acesso a Dados**: **Application** interage com a camada **Infra.Data** para persistir ou recuperar dados do banco.
5. **Injeção de Dependência**: A camada **IoC** gerencia a criação e injeção das dependências entre as camadas.

## Implementação

### Camada Domain

O objetivo desta camada é criar um modelo de domínio rico e autônomo, independente de outras camadas. Principais características:

- Classes seladas (`sealed`) para evitar herança.
- Setters privados para proteger a integridade dos dados.
- Validação interna do modelo.
- Testes unitários para garantir a validade do domínio.

### Camada Infra.Data

Esta camada gerencia o acesso aos dados da aplicação. Principais componentes:

- **Context**: Definição do `DbContext` e mapeamento ORM.
- **EntitiesConfiguration**: Configurações das entidades usando Fluent API.
- **Repositories**: Implementação dos repositórios para `Category` e `Product`.
- **Identity**: Gerenciamento de autenticação e autorização com ASP.NET Core Identity.

**Pacotes NuGet**:

```bash
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
````

### Camada Application
Responsável pela comunicação entre a camada de apresentação e a camada de domínio. Principais elementos:

- DTOs (Data Transfer Objects): Objetos que transportam dados entre as camadas.
- Serviços: Implementação de regras de aplicação, utilizando CQRS.
- ICategoryService e IProductService: Interfaces para os serviços de Category e Product.
  
## Injeção de Dependência
Os serviços são registrados no contêiner IoC com os tempos de vida apropriados:

```C#
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext)
            .Assembly.FullName))); 
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}

```

### Migrations com EF Core
O Entity Framework Core permite atualizar incrementalmente o esquema do banco de dados, mantendo-o em sincronia com o modelo de dados da aplicação:

```bash
# Visual Studio
Add-Migration <nome>
Update-Database

# .NET CLI
dotnet ef migrations add <nome>
dotnet ef database update

```

### Padrões Implementados

- CQRS (Command Query Responsibility Segregation): Separação de comandos e consultas em modelos distintos.
- Mediador: Utilizado para otimizar o desempenho e desacoplamento no projeto.

## Configuração do Ambiente de Desenvolvimento

### Requisitos
- Docker instalado
- Visual Studio ou Visual Studio Code

### Passos para configurar

1. Clone o repositório.
2. No terminal, navegue até a pasta do projeto e rode o comando:

   ```bash
   docker-compose up -d
  
### Considerações Finais
Este projeto segue a Clean Architecture para garantir a modularidade, testabilidade e manutenibilidade do código. Cada camada tem uma responsabilidade bem definida e interage com outras camadas apenas quando necessário, seguindo as boas práticas de desenvolvimento de software.


