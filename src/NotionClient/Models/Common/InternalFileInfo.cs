// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>An internal Notion-hosted file reference with a pre-signed URL and expiry.</summary>
public sealed class InternalFileInfo
{
    /// <summary>Gets the pre-signed URL for accessing the Notion-hosted file. This URL expires after the time indicated by <see cref="ExpiryTime"/>.</summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    /// <summary>Gets the ISO 8601 date-time string after which the pre-signed URL is no longer valid, or <see langword="null"/> if no expiry is specified.</summary>
    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; init; }
}
