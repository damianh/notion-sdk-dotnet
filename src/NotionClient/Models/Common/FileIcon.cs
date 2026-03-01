// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>An internal Notion-hosted file used as a page or database icon.</summary>
public sealed class FileIcon : Icon
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "file";

    /// <summary>Gets the internal Notion-hosted file reference, including its pre-signed URL and expiry time.</summary>
    [JsonPropertyName("file")]
    public required InternalFileInfo File { get; init; }
}
