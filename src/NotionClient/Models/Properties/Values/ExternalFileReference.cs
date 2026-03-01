// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A file reference that points to an externally hosted file (outside Notion).
/// </summary>
public sealed class ExternalFileReference : FileReference
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string FileRefType => "external";

    /// <summary>The external file URL information.</summary>
    [JsonPropertyName("external")]
    public ExternalFileInfo External { get; init; } = null!;
}
