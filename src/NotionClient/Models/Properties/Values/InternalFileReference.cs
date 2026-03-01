// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A file reference for a file hosted internally by Notion, with a time-limited download URL.
/// </summary>
public sealed class InternalFileReference : FileReference
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string FileRefType => "file";

    /// <summary>The Notion-hosted file information, including the temporary download URL and expiry time.</summary>
    [JsonPropertyName("file")]
    public InternalFileInfo File { get; init; } = null!;
}
