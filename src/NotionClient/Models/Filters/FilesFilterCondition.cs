// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>files</c> database property, which holds attached file references.
/// Only one condition should be set per filter.
/// </summary>
public sealed class FilesFilterCondition
{
    /// <summary>Gets a value indicating whether the files property must have no attachments.</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the files property must have at least one attachment.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
