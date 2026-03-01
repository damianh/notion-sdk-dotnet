// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A file upload object representing a file being uploaded via the Notion file uploads API.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/reference/file-uploads">Notion API — File uploads</see>.
/// </remarks>
public sealed class FileUpload
{
    /// <summary>Gets the Notion object type, always <c>"file_upload"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "file_upload";

    /// <summary>Gets the unique identifier (UUID) of the file upload.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets the ISO 8601 timestamp when the file upload was created.</summary>
    [JsonPropertyName("created_time")]
    public string? CreatedTime { get; init; }

    /// <summary>Gets the ISO 8601 timestamp when the file upload was last edited.</summary>
    [JsonPropertyName("last_edited_time")]
    public string? LastEditedTime { get; init; }

    /// <summary>Gets the ISO 8601 timestamp when the upload URL expires.</summary>
    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; init; }

    /// <summary>Gets the current status of the file upload (e.g., <c>"uploaded"</c>, <c>"pending"</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Gets the original filename of the uploaded file.</summary>
    [JsonPropertyName("filename")]
    public string? Filename { get; init; }

    /// <summary>Gets the MIME content type of the uploaded file.</summary>
    [JsonPropertyName("content_type")]
    public string? ContentType { get; init; }

    /// <summary>Gets the size of the uploaded file in bytes.</summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>Gets the pre-signed URL to which the file content should be uploaded via PUT.</summary>
    [JsonPropertyName("upload_url")]
    public string? UploadUrl { get; init; }

    /// <summary>Gets the Notion-hosted file information after the upload is complete.</summary>
    [JsonPropertyName("file")]
    public InternalFileInfo? File { get; init; }
}
