// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A page cover image hosted by Notion (uploaded file).
/// </summary>
public sealed class FilePageCover : PageCover
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string CoverType => "file";

    /// <summary>Gets the Notion-hosted file information containing the cover image URL and expiry.</summary>
    [JsonPropertyName("file")]
    public required InternalFileInfo File { get; init; }
}
