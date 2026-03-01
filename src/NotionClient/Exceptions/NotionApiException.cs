// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net;
using System.Text.Json;

namespace DamianH.NotionClient.Exceptions;

/// <summary>
/// Thrown when the Notion API returns a non-success response.
/// </summary>
public sealed class NotionApiException : Exception
{
    /// <summary>
    /// Gets the HTTP status code returned by the Notion API (e.g., 400, 401, 404, 429, 500).
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the Notion-specific error code parsed from the response body, or <see langword="null"/>
    /// if the response did not contain a recognized <c>"code"</c> field.
    /// </summary>
    public NotionErrorCode? ErrorCode { get; }

    /// <summary>
    /// Gets the unique request identifier assigned by the Notion API, useful for support inquiries.
    /// <see langword="null"/> if the response body did not include a <c>"request_id"</c> field.
    /// </summary>
    public string? RequestId { get; }

    /// <summary>
    /// Gets the raw JSON response body returned by the Notion API.
    /// </summary>
    public string RawBody { get; }

    public NotionApiException(HttpStatusCode statusCode, string rawBody)
        : base(ParseMessage(rawBody, statusCode))
    {
        StatusCode = statusCode;
        RawBody = rawBody;
        (ErrorCode, RequestId) = ParseBody(rawBody);
    }

    private static string ParseMessage(string body, HttpStatusCode status)
    {
        try
        {
            var doc = JsonDocument.Parse(body);
            if (doc.RootElement.TryGetProperty("message", out var msg))
            {
                return msg.GetString() ?? $"Notion API error: {(int)status}";
            }
        }
        catch { }
        return $"Notion API error: {(int)status}";
    }

    private static (NotionErrorCode? code, string? requestId) ParseBody(string body)
    {
        try
        {
            var doc = JsonDocument.Parse(body);
            NotionErrorCode? code = null;
            string? requestId = null;

            if (doc.RootElement.TryGetProperty("code", out var codeEl))
            {
                var codeStr = codeEl.GetString();
                code = codeStr switch
                {
                    "unauthorized" => NotionErrorCode.Unauthorized,
                    "restricted_resource" => NotionErrorCode.RestrictedResource,
                    "object_not_found" => NotionErrorCode.ObjectNotFound,
                    "rate_limited" => NotionErrorCode.RateLimited,
                    "invalid_json" => NotionErrorCode.InvalidJson,
                    "invalid_request_url" => NotionErrorCode.InvalidRequestUrl,
                    "invalid_request" => NotionErrorCode.InvalidRequest,
                    "validation_error" => NotionErrorCode.ValidationError,
                    "conflict_error" => NotionErrorCode.ConflictError,
                    "internal_server_error" => NotionErrorCode.InternalServerError,
                    "service_unavailable" => NotionErrorCode.ServiceUnavailable,
                    _ => null,
                };
            }

            if (doc.RootElement.TryGetProperty("request_id", out var rid))
            {
                requestId = rid.GetString();
            }

            return (code, requestId);
        }
        catch
        {
            return (null, null);
        }
    }
}

/// <summary>
/// Error codes returned in the <c>"code"</c> field of Notion API error responses.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/reference/errors">Notion API — Errors</see>.
/// </remarks>
public enum NotionErrorCode
{
    /// <summary>The API token is invalid or has been revoked.</summary>
    Unauthorized,

    /// <summary>The token does not have access to the requested resource.</summary>
    RestrictedResource,

    /// <summary>The requested object does not exist or is not accessible with the current token.</summary>
    ObjectNotFound,

    /// <summary>The request exceeded the Notion API rate limit; retry after a backoff period.</summary>
    RateLimited,

    /// <summary>The request body contained malformed JSON.</summary>
    InvalidJson,

    /// <summary>The request URL was not valid for the Notion API.</summary>
    InvalidRequestUrl,

    /// <summary>The request was well-formed JSON but semantically invalid (e.g., missing required fields).</summary>
    InvalidRequest,

    /// <summary>A property value or parameter failed validation (e.g., wrong type, out of range).</summary>
    ValidationError,

    /// <summary>A conflict occurred, typically due to a concurrent edit on the same object.</summary>
    ConflictError,

    /// <summary>An unexpected error occurred on the Notion server.</summary>
    InternalServerError,

    /// <summary>The Notion API is temporarily unavailable; retry later.</summary>
    ServiceUnavailable,
}
