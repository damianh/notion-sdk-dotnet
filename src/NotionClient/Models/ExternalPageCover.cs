// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A page cover image hosted at an external URL.
/// </summary>
public sealed class ExternalPageCover : PageCover
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string CoverType => "external";

    /// <summary>Gets the external file information containing the cover image URL.</summary>
    [JsonPropertyName("external")]
    public required ExternalFileInfo External { get; init; }
}
