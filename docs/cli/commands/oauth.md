# notion oauth

Notion OAuth 2.0 token management.

See the [CLI overview](../README.md) for global options.

> **Important:** OAuth commands do **not** use the global `--token` option or the 3-tier token resolution (env var / config file). They authenticate using `--client-id` and `--client-secret` (HTTP Basic Auth). Each subcommand defines its own `--verbose` option independently of the root command.

## notion oauth exchange-token

Exchange a temporary authorization code for an access token.

**Options:**

| Option | Type | Required | Description |
|---|---|---|---|
| `--client-id <id>` | string | **yes** | The OAuth client ID. |
| `--client-secret <secret>` | string | **yes** | The OAuth client secret. |
| `--code <code>` | string | **yes** | The temporary authorization code. |
| `--redirect-uri <uri>` | string | no | The redirect URI used in the authorization request. |
| `--verbose` | flag | no | Show diagnostic output on stderr. |

Returns an `ExchangeTokenResponse` JSON object containing the access token and workspace information.

**Example:**

```bash
notion oauth exchange-token \
  --client-id abc123 \
  --client-secret secret_xyz \
  --code temp_code_abc \
  --redirect-uri https://example.com/callback
```

---

## notion oauth revoke

Revoke a previously issued access token.

**Options:**

| Option | Type | Required | Description |
|---|---|---|---|
| `--client-id <id>` | string | **yes** | The OAuth client ID. |
| `--client-secret <secret>` | string | **yes** | The OAuth client secret. |
| `--token <token>` | string | **yes** | The access token to revoke. |
| `--verbose` | flag | no | Show diagnostic output on stderr. |

> **Note:** `--token` here is the access token to revoke, not an API bearer token for authenticating the request.

Returns an empty JSON object `{}` on success.

**Example:**

```bash
notion oauth revoke \
  --client-id abc123 \
  --client-secret secret_xyz \
  --token ntn_access_token_to_revoke
```

---

## notion oauth introspect

Introspect an access token to check its validity and metadata.

**Options:**

| Option | Type | Required | Description |
|---|---|---|---|
| `--client-id <id>` | string | **yes** | The OAuth client ID. |
| `--client-secret <secret>` | string | **yes** | The OAuth client secret. |
| `--token <token>` | string | **yes** | The access token to introspect. |
| `--verbose` | flag | no | Show diagnostic output on stderr. |

> **Note:** `--token` here is the access token to introspect, not an API bearer token for authenticating the request.

Returns an `IntrospectTokenResponse` JSON object with token validity and metadata.

**Example:**

```bash
notion oauth introspect \
  --client-id abc123 \
  --client-secret secret_xyz \
  --token ntn_access_token_to_check
```
