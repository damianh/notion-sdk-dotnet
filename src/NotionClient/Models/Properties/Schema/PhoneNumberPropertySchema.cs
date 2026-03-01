// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>phone_number</c> property, which stores a phone number string.
/// <see href="https://developers.notion.com/reference/property-object#phone-number"/>
/// </summary>
public sealed class PhoneNumberPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "phone_number";

    /// <summary>The phone_number configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("phone_number")]
    public object? PhoneNumber { get; init; }
}
