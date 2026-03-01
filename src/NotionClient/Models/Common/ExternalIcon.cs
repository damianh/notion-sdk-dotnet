// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>An external URL used as a page or database icon.</summary>
public sealed class ExternalIcon : Icon
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "external";

    /// <summary>Gets the external file reference containing the publicly accessible icon URL.</summary>
    [JsonPropertyName("external")]
    public required ExternalFileInfo External { get; init; }
}
