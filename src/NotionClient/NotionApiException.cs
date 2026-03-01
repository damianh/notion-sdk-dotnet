// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net;
using System.Text.Json;

namespace DamianH.NotionClient;

/// <summary>
/// Thrown when the Notion API returns a non-success response.
/// </summary>
public sealed class NotionApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public NotionErrorCode? ErrorCode { get; }
    public string? RequestId { get; }
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
