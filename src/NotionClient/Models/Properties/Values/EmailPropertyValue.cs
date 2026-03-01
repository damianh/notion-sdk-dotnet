// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>email</c> property, containing an email address string.
/// <see href="https://developers.notion.com/reference/property-value-object#email-property-values"/>
/// </summary>
public sealed class EmailPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "email";

    /// <summary>The email address stored in this property, or <c>null</c> if not set.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
