# Confirmação de Email - Email Confirmation

## Descrição
Esta funcionalidade implementa o fluxo de confirmação de email para novos usuários registrados na API.

## Funcionalidades Implementadas

### 1. Registro de Usuário com Confirmação de Email
- **Endpoint**: `POST /api/v1/Auth/nova-conta`
- **Comportamento**: 
  - Cria um novo usuário com `EmailConfirmed = false`
  - Gera um token de confirmação de email
  - Envia um email de confirmação (console/log para fins educativos)
  - Retorna mensagem de sucesso pedindo ao usuário para verificar o email

### 2. Confirmação de Email
- **Endpoint**: `GET /api/v1/Auth/confirmar-email?userId={userId}&token={token}`
- **Comportamento**:
  - Valida o token de confirmação
  - Marca o email como confirmado
  - Retorna mensagem de sucesso ou erro

### 3. Login com Verificação de Email
- **Endpoint**: `POST /api/v1/Auth/login`
- **Comportamento**:
  - Verifica se o email está confirmado antes de permitir o login
  - Retorna erro se o email não estiver confirmado
  - Gera JWT token se email estiver confirmado e credenciais válidas

### 4. Reenvio de Email de Confirmação
- **Endpoint**: `POST /api/v1/Auth/reenviar-confirmacao`
- **Comportamento**:
  - Gera novo token de confirmação
  - Reenvia email de confirmação
  - Útil para casos onde o usuário não recebeu o email inicial

## Serviço de Email

### EmailService
Por ser uma API educativa, o serviço de email atual (`EmailService.cs`) apenas registra as informações no console/log.

Para implementar envio real de emails em produção:
1. Substituir a implementação do `EmailService`
2. Usar serviços como:
   - SendGrid
   - AWS SES
   - SMTP
   - Mailgun
   - etc.

Exemplo de configuração no console:
```
=== EMAIL DE CONFIRMAÇÃO ===
Para: usuario@example.com
Usuário: nomeUsuario
Link de confirmação: https://localhost:5001/api/v1/Auth/confirmar-email?userId=...&token=...
===========================
```

## Fluxo de Uso

### Registro e Confirmação
1. **Usuário se registra**: `POST /api/v1/Auth/nova-conta`
   ```json
   {
     "userName": "teste123",
     "name": "Teste Usuario",
     "email": "teste@example.com",
     "password": "SenhaForte@123",
     "confirmPassword": "SenhaForte@123"
   }
   ```

2. **API retorna**:
   ```json
   {
     "message": "Usuário registrado com sucesso. Por favor, verifique seu email para confirmar sua conta.",
     "userId": "abc123..."
   }
   ```

3. **Verificar console/logs** para o link de confirmação

4. **Acessar o link** ou fazer requisição GET:
   ```
   GET /api/v1/Auth/confirmar-email?userId=abc123...&token=xyz...
   ```

5. **API retorna**:
   ```
   "Email confirmado com sucesso! Você já pode fazer login."
   ```

### Login após confirmação
6. **Fazer login**: `POST /api/v1/Auth/login`
   ```json
   {
     "userName": "teste123",
     "password": "SenhaForte@123"
   }
   ```

7. **API retorna** o token JWT se email estiver confirmado

### Caso o email não seja confirmado
- Se tentar fazer login sem confirmar o email:
  ```
  "Email não confirmado. Por favor, confirme seu email antes de fazer login."
  ```

- Para reenviar o email de confirmação:
  ```
  POST /api/v1/Auth/reenviar-confirmacao
  Content-Type: application/json
  
  "teste@example.com"
  ```
  **Nota**: A API espera uma string JSON simples no corpo da requisição.

## Configuração ASP.NET Identity

A configuração do Identity já inclui suporte a tokens:
```csharp
services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddErrorDescriber<IdentityMensagensPortugues>()
    .AddDefaultTokenProviders(); // Importante para gerar tokens de confirmação
```

## Segurança

### Considerações de Segurança
- Tokens de confirmação são gerados usando os provedores padrão do ASP.NET Identity
- Tokens são codificados em URL para transporte seguro
- Tokens expiram automaticamente (tempo configurável no Identity)
- Email já confirmado não pode ser confirmado novamente

### Nota sobre GET vs POST para Confirmação
Esta implementação usa GET para confirmação de email para permitir que usuários cliquem em links diretos nos emails.
Em ambientes de alta segurança, considere:
- Usar POST com tokens no corpo da requisição
- Implementar página intermediária que faça POST
- Tokens de curta duração
- Rate limiting para prevenir ataques de força bruta

### Considerações para Produção
Ao implementar serviços de email em produção, considere:
- **Autenticação segura**: Armazenar credenciais de forma segura (Azure Key Vault, AWS Secrets Manager)
- **Rate Limiting**: Limitar número de emails por IP/usuário para prevenir spam
- **Validação de templates**: Validar e sanitizar templates de email
- **Logging de auditoria**: Registrar todas as tentativas de envio e confirmação
- **Monitoramento**: Alertas para falhas de envio ou padrões suspeitos

## Próximos Passos (Produção)

1. Implementar serviço real de envio de emails (SendGrid, AWS SES, SMTP, etc.)
2. Adicionar templates HTML para emails
3. Configurar tempo de expiração dos tokens
4. Adicionar página web para confirmação (em vez de apenas API)
5. Implementar recuperação de senha com email
6. Adicionar logs de auditoria
7. **Implementar rate limiting**: Throttling para reenvio de emails (ex: máximo 3 tentativas por hora por usuário)
8. Implementar monitoramento de falhas de envio
9. Adicionar testes de integração para fluxo completo

## Testes

Para testar localmente:
1. Iniciar a API com banco de dados configurado
2. Registrar um novo usuário via POST
3. Verificar console/logs para link de confirmação
4. Copiar o link e fazer requisição GET
5. Tentar fazer login antes e depois da confirmação
