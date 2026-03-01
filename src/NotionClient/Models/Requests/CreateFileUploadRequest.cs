// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for initiating a file upload session in the Notion API.
/// Used to create a file upload object that can subsequently receive file data.
/// </summary>
public sealed class CreateFileUploadRequest
{
    /// <summary>Gets the original filename of the file being uploaded, or <see langword="null"/> if not specified.</summary>
    [JsonPropertyName("filename")]
    public string? Filename { get; init; }

    /// <summary>Gets the MIME type of the file being uploaded (e.g. <c>"image/png"</c>), or <see langword="null"/> if not specified.</summary>
    [JsonPropertyName("content_type")]
    public string? ContentType { get; init; }

    /// <summary>Gets the total size of the file in bytes, or <see langword="null"/> if not specified.</summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>Gets the number of parts for a multi-part upload, or <see langword="null"/> for a single-part upload.</summary>
    [JsonPropertyName("number_of_parts")]
    public int? NumberOfParts { get; init; }
}
