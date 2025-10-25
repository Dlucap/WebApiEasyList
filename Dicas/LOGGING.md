# Sistema de Logging da API EasyList

## Visão Geral

Este sistema de logging implementa um log completo da API com persistência em banco de dados, capturando todos os pontos cruciais da aplicação.

## Características

### 1. Tipos de Log Capturados

#### Requisições HTTP
- Método HTTP (GET, POST, PUT, DELETE, etc.)
- Caminho da requisição
- Código de status da resposta
- Tempo de duração (em milissegundos)
- Usuário autenticado (se disponível)
- Endereço IP do cliente

#### Autenticação
- Tentativas de login (sucesso e falha)
- Criação de novas contas
- Informações do usuário
- Endereço IP de origem

#### Operações de Banco de Dados
- Operações CRUD (INSERT, UPDATE, DELETE)
- Entidade afetada
- ID do registro
- Usuário que executou a operação

#### Erros e Exceções
- Mensagem de erro
- Stack trace completo
- Categoria do erro
- Informações adicionais de contexto

## Estrutura do Banco de Dados

### Tabela LogEntry

```sql
CREATE TABLE LogEntry (
    Id                   CHAR(36) PRIMARY KEY,
    Timestamp            DATETIME(6) NOT NULL,
    Level                VARCHAR(50),
    Category             VARCHAR(200),
    Message              LONGTEXT,
    Exception            LONGTEXT,
    HttpMethod           VARCHAR(10),
    Path                 VARCHAR(500),
    StatusCode           INT,
    Duration             INT,
    UserId               VARCHAR(100),
    UserName             VARCHAR(100),
    IpAddress            VARCHAR(50),
    AdditionalInfo       LONGTEXT,
    UsuarioCriacao       VARCHAR(100),
    DataCriacao          DATETIME(6) NOT NULL,
    UsuarioModificacao   VARCHAR(100),
    DataModificacao      DATETIME(6) NOT NULL,
    INDEX IX_LogEntry_Timestamp (Timestamp),
    INDEX IX_LogEntry_Level (Level),
    INDEX IX_LogEntry_UserName (UserName)
);
```

## Componentes

### 1. LogService
Serviço principal para criação de logs programáticos.

**Métodos disponíveis:**
- `LogInformacao(message, category, additionalInfo)` - Log de informação
- `LogAviso(message, category, additionalInfo)` - Log de aviso
- `LogErro(message, exception, category, additionalInfo)` - Log de erro
- `LogRequisicaoHttp(httpMethod, path, statusCode, duration, userId, userName, ipAddress)` - Log de requisição HTTP
- `LogAutenticacao(userName, sucesso, ipAddress, additionalInfo)` - Log de autenticação
- `LogOperacaoBancoDados(operacao, entidade, detalhes)` - Log de operação de BD

### 2. LoggingMiddleware
Middleware automático que intercepta todas as requisições HTTP.

**Funcionalidades:**
- Captura automática de todas as requisições
- Medição de tempo de resposta
- Logging de exceções não tratadas
- Filtragem de endpoints estáticos (swagger, health checks)

### 3. LogController
Controller para consultar logs através da API.

**Endpoints disponíveis:**
- `GET /api/v1/log/{pagina}/{tamanho}` - Lista logs paginados
- `GET /api/v1/log/{id}` - Busca log por ID
- `GET /api/v1/log/periodo/{dataInicio}/{dataFim}` - Busca logs por período
- `GET /api/v1/log/nivel/{level}` - Busca logs por nível (Information, Warning, Error)
- `GET /api/v1/log/usuario/{userName}` - Busca logs de um usuário específico

## Como Usar

### Injeção de Dependência

O `ILogService` está registrado no container de DI e pode ser injetado em qualquer classe:

```csharp
public class MeuServico
{
    private readonly ILogService _logService;

    public MeuServico(ILogService logService)
    {
        _logService = logService;
    }

    public async Task MinhaOperacao()
    {
        await _logService.LogInformacao(
            "Operação executada com sucesso",
            "MinhaCategoria",
            "Informações adicionais"
        );
    }
}
```

### Logging de Exceções

```csharp
try
{
    // código
}
catch (Exception ex)
{
    await _logService.LogErro(
        "Erro ao processar operação",
        ex,
        "MinhaCategoria",
        "Contexto adicional"
    );
    throw;
}
```

### Logging de Operações de Banco de Dados

```csharp
await _repository.Adicionar(entidade);
await _logService.LogOperacaoBancoDados(
    "INSERT",
    "NomeDaEntidade",
    $"ID: {entidade.Id}, Usuario: {entidade.UsuarioCriacao}"
);
```

## Configuração

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "EasyList.Api.Middleware.LoggingMiddleware": "Information",
      "EasyList.Business.Services.LogService": "Information"
    }
  }
}
```

## Instalação do Banco de Dados

Execute o script SQL localizado em `/Script-Banco/CreateLogEntryTable.sql` para criar a tabela de logs no banco de dados:

```bash
mysql -u root -p easylist < Script-Banco/CreateLogEntryTable.sql
```

Ou utilize a migration do Entity Framework (quando disponível).

## Níveis de Log

- **Information**: Eventos normais da aplicação (requisições bem-sucedidas, operações CRUD)
- **Warning**: Situações anormais mas não críticas (tentativas de login falhas, validações)
- **Error**: Erros e exceções que impactam a operação

## Boas Práticas

1. **Use categorias consistentes**: Use categorias como "HTTP", "Authentication", "Database", etc.
2. **Inclua contexto**: Sempre que possível, inclua informações adicionais relevantes
3. **Não logue informações sensíveis**: Evite logar senhas, tokens, ou dados pessoais sensíveis
4. **Use o nível apropriado**: Information para operações normais, Warning para situações suspeitas, Error para falhas

## Performance

O sistema de logging é assíncrono e não deve impactar significativamente a performance da API. Em caso de falha no sistema de logging, um log de fallback é gerado no log padrão do ASP.NET Core.

## Monitoramento

Os logs podem ser consultados:
1. Diretamente no banco de dados
2. Através da API REST usando o `LogController`
3. Através de ferramentas de BI conectadas ao banco

## Retenção de Dados

Recomenda-se implementar uma política de retenção de logs, removendo registros antigos periodicamente para evitar crescimento excessivo da tabela.

Exemplo de query para limpar logs com mais de 90 dias:

```sql
DELETE FROM LogEntry WHERE Timestamp < DATE_SUB(NOW(), INTERVAL 90 DAY);
```

## Referências

Este sistema foi inspirado no artigo:
https://marraia.medium.com/utilizando-log-em-asp-net-core-171e90732ec5
