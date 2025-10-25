# Social Login Configuration

This document explains how to set up and use Social Login with Google and Instagram in the EasyList API.

## Quick Start

### Required Packages (Already Installed)
- Microsoft.AspNetCore.Authentication.Google v5.0.17
- AspNet.Security.OAuth.Instagram v5.0.0

### Configuration

Add your OAuth credentials to `appsettings.json`:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "YOUR_GOOGLE_CLIENT_ID",
      "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET"
    },
    "Instagram": {
      "ClientId": "YOUR_INSTAGRAM_APP_ID",
      "ClientSecret": "YOUR_INSTAGRAM_APP_SECRET"
    }
  }
}
```

**Note:** Providers are only registered if credentials are configured. You can configure just Google, just Instagram, or both.

## API Endpoints

### List Available Providers
```
GET /api/v1/auth/external-login-providers
```

### Initiate Social Login
```
GET /api/v1/auth/external-login?provider=Google
```

### Callback (automatic)
```
GET /api/v1/auth/external-login-callback
```

## How It Works

1. Client calls `/api/v1/auth/external-login?provider=Google`
2. User is redirected to Google's login page
3. After authentication, Google redirects back to the callback URL
4. API creates/retrieves the user and returns a JWT token
5. Client uses the JWT token for subsequent requests

## Security Features

- Return URL validation to prevent open redirect attacks
- OAuth credentials validation with graceful degradation
- HTTPS enforcement in production
- JWT token-based authentication

## Getting OAuth Credentials

### Google
1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a project and enable OAuth 2.0
3. Add authorized redirect URIs
4. Copy Client ID and Client Secret

### Instagram
1. Go to [Facebook Developers](https://developers.facebook.com/)
2. Create an app and add Instagram Basic Display
3. Configure OAuth redirect URIs
4. Copy App ID and App Secret

For detailed instructions in Portuguese, see `ConfiguracaoLoginSocial.md`.
