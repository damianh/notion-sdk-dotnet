// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>phone_number</c> property, containing a phone number string.
/// <see href="https://developers.notion.com/reference/property-value-object#phone-number-property-values"/>
/// </summary>
public sealed class PhoneNumberPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "phone_number";

    /// <summary>The phone number string stored in this property, or <c>null</c> if not set.</summary>
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; init; }
}
