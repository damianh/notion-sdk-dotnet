// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>email</c> property, which stores an email address string.
/// <see href="https://developers.notion.com/reference/property-object#email"/>
/// </summary>
public sealed class EmailPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "email";

    /// <summary>The email configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("email")]
    public object? Email { get; init; }
}
