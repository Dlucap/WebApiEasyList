# Configuração de Login Social

Este documento explica como configurar e usar o Login Social com Google e Instagram na API EasyList.

## Pré-requisitos

### Google OAuth 2.0

1. Acesse o [Google Cloud Console](https://console.cloud.google.com/)
2. Crie um novo projeto ou selecione um existente
3. Navegue para "APIs & Services" > "Credentials"
4. Clique em "Create Credentials" > "OAuth 2.0 Client ID"
5. Configure o tipo de aplicação como "Web application"
6. Adicione as URIs de redirecionamento autorizadas:
   - `https://localhost:5001/api/v1/auth/external-login-callback`
   - `https://localhost:5001/api/v2/auth/external-login-callback`
   - Adicione também as URLs de produção quando disponível
7. Copie o Client ID e Client Secret gerados

### Instagram Basic Display API

1. Acesse o [Facebook Developers](https://developers.facebook.com/)
2. Crie um novo aplicativo ou selecione um existente
3. Adicione o produto "Instagram Basic Display"
4. Configure as URLs de redirecionamento OAuth válidas:
   - `https://localhost:5001/api/v1/auth/external-login-callback`
   - `https://localhost:5001/api/v2/auth/external-login-callback`
   - Adicione também as URLs de produção quando disponível
5. Copie o Instagram App ID (Client ID) e Instagram App Secret (Client Secret)

## Configuração no appsettings.json

Atualize o arquivo `appsettings.json` com suas credenciais:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "SEU_GOOGLE_CLIENT_ID",
      "ClientSecret": "SEU_GOOGLE_CLIENT_SECRET"
    },
    "Instagram": {
      "ClientId": "SEU_INSTAGRAM_APP_ID",
      "ClientSecret": "SEU_INSTAGRAM_APP_SECRET"
    }
  }
}
```

**IMPORTANTE:** Nunca commit suas credenciais reais no repositório. Use User Secrets para desenvolvimento ou variáveis de ambiente para produção.

### Usando User Secrets (Desenvolvimento)

```bash
cd src/EasyList.Api
dotnet user-secrets set "Authentication:Google:ClientId" "seu-client-id"
dotnet user-secrets set "Authentication:Google:ClientSecret" "seu-client-secret"
dotnet user-secrets set "Authentication:Instagram:ClientId" "seu-instagram-app-id"
dotnet user-secrets set "Authentication:Instagram:ClientSecret" "seu-instagram-app-secret"
```

**Nota:** Se você não configurar credenciais para um provedor específico, esse provedor simplesmente não será disponibilizado na lista de provedores externos. A API funcionará normalmente com os provedores configurados e com a autenticação tradicional por username/password.

## Endpoints Disponíveis

### 1. Listar Provedores Disponíveis

```
GET /api/v1/auth/external-login-providers
```

Retorna a lista de provedores de autenticação externa disponíveis.

**Resposta de sucesso:**
```json
[
  { "Name": "Google", "DisplayName": "Google" },
  { "Name": "Instagram", "DisplayName": "Instagram" }
]
```

### 2. Iniciar Login Social

```
GET /api/v1/auth/external-login?provider={provider}&returnUrl={returnUrl}
```

**Parâmetros:**
- `provider` (obrigatório): Nome do provedor ("Google" ou "Instagram")
- `returnUrl` (opcional): URL para redirecionar após login bem-sucedido

**Exemplo:**
```
GET /api/v1/auth/external-login?provider=Google
```

Este endpoint redireciona o usuário para a página de autenticação do provedor selecionado.

### 3. Callback de Login Social

```
GET /api/v1/auth/external-login-callback
```

Este endpoint é chamado automaticamente pelo provedor após a autenticação. Não deve ser chamado diretamente pelo cliente.

**Resposta de sucesso:**
```json
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

Retorna o token JWT que deve ser usado nas requisições subsequentes.

## Fluxo de Autenticação

### Para aplicações web:

1. Cliente chama `/api/v1/auth/external-login?provider=Google`
2. API redireciona para a página de login do Google
3. Usuário faz login no Google e autoriza o aplicativo
4. Google redireciona de volta para `/api/v1/auth/external-login-callback`
5. API cria/recupera o usuário e retorna o token JWT
6. Cliente usa o token JWT nas requisições subsequentes

### Para aplicações mobile/SPA:

Para aplicações que não suportam redirecionamentos, considere:
1. Implementar o fluxo OAuth no próprio aplicativo
2. Enviar o token de acesso do provedor para um endpoint customizado
3. A API valida o token com o provedor e retorna o JWT

## Segurança

- Sempre use HTTPS em produção
- Mantenha as credenciais OAuth seguras e nunca as exponha publicamente
- Configure corretamente as URLs de redirecionamento nos consoles dos provedores
- Considere implementar rate limiting para os endpoints de autenticação
- Valide sempre os tokens JWT nas requisições protegidas
- A API implementa validação de returnUrl para prevenir ataques de open redirect
- Os provedores de autenticação são registrados apenas se as credenciais estiverem configuradas

## Observações

- Ao fazer login pela primeira vez com um provedor social, um novo usuário é criado automaticamente
- O email do provedor social é usado como username
- Se um usuário já existe com o mesmo email, o login externo é associado à conta existente
- Os usuários podem ter múltiplos provedores de login associados à mesma conta
- Se as credenciais do Google ou Instagram não estiverem configuradas, o provedor correspondente não será disponibilizado
- A API valida URLs de retorno para prevenir redirecionamentos maliciosos

## Troubleshooting

### Erro: "Redirect URI mismatch"
Verifique se as URLs de redirecionamento configuradas nos consoles dos provedores correspondem exatamente às URLs da sua aplicação.

### Erro: "Email não fornecido pelo provedor"
Certifique-se de solicitar o escopo de email nas configurações do provedor.

### Erro: "Invalid client"
Verifique se o Client ID e Client Secret estão corretos no appsettings.json.
