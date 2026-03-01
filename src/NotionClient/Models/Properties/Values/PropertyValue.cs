// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Abstract base class for the value of a Notion page property.
/// Each concrete subtype corresponds to one of the Notion property types:
/// title, rich_text, number, select, multi_select, status, date, people, files,
/// checkbox, url, email, phone_number, formula, relation, rollup, created_by,
/// created_time, last_edited_by, last_edited_time, unique_id, button, verification.
/// <see href="https://developers.notion.com/reference/property-value-object"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TitlePropertyValue), "title")]
[JsonDerivedType(typeof(RichTextPropertyValue), "rich_text")]
[JsonDerivedType(typeof(NumberPropertyValue), "number")]
[JsonDerivedType(typeof(SelectPropertyValue), "select")]
[JsonDerivedType(typeof(MultiSelectPropertyValue), "multi_select")]
[JsonDerivedType(typeof(StatusPropertyValue), "status")]
[JsonDerivedType(typeof(DatePropertyValue), "date")]
[JsonDerivedType(typeof(PeoplePropertyValue), "people")]
[JsonDerivedType(typeof(FilesPropertyValue), "files")]
[JsonDerivedType(typeof(CheckboxPropertyValue), "checkbox")]
[JsonDerivedType(typeof(UrlPropertyValue), "url")]
[JsonDerivedType(typeof(EmailPropertyValue), "email")]
[JsonDerivedType(typeof(PhoneNumberPropertyValue), "phone_number")]
[JsonDerivedType(typeof(FormulaPropertyValue), "formula")]
[JsonDerivedType(typeof(RelationPropertyValue), "relation")]
[JsonDerivedType(typeof(RollupPropertyValue), "rollup")]
[JsonDerivedType(typeof(CreatedByPropertyValue), "created_by")]
[JsonDerivedType(typeof(CreatedTimePropertyValue), "created_time")]
[JsonDerivedType(typeof(LastEditedByPropertyValue), "last_edited_by")]
[JsonDerivedType(typeof(LastEditedTimePropertyValue), "last_edited_time")]
[JsonDerivedType(typeof(UniqueIdPropertyValue), "unique_id")]
[JsonDerivedType(typeof(ButtonPropertyValue), "button")]
[JsonDerivedType(typeof(VerificationPropertyValue), "verification")]
public abstract class PropertyValue
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public PropertyValue() { }

    /// <summary>The unique identifier of this property within the page.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The Notion property type discriminator string (e.g., "title", "number").</summary>
    [JsonIgnore]
    public virtual string Type => string.Empty;
}
