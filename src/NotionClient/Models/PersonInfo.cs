// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// Person-specific information for a <see cref="PersonUser"/>.
/// </summary>
public sealed class PersonInfo
{
    /// <summary>Gets the email address of the person.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
