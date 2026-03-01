// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Abstract base class for a Notion database property schema definition (column type).
/// Each concrete subtype corresponds to one of the Notion property types:
/// title, rich_text, number, select, multi_select, status, date, people, files,
/// checkbox, url, email, phone_number, formula, relation, rollup, created_by,
/// created_time, last_edited_by, last_edited_time, unique_id, button, verification.
/// <see href="https://developers.notion.com/reference/property-object"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TitlePropertySchema), "title")]
[JsonDerivedType(typeof(RichTextPropertySchema), "rich_text")]
[JsonDerivedType(typeof(NumberPropertySchema), "number")]
[JsonDerivedType(typeof(SelectPropertySchema), "select")]
[JsonDerivedType(typeof(MultiSelectPropertySchema), "multi_select")]
[JsonDerivedType(typeof(StatusPropertySchema), "status")]
[JsonDerivedType(typeof(DatePropertySchema), "date")]
[JsonDerivedType(typeof(PeoplePropertySchema), "people")]
[JsonDerivedType(typeof(FilesPropertySchema), "files")]
[JsonDerivedType(typeof(CheckboxPropertySchema), "checkbox")]
[JsonDerivedType(typeof(UrlPropertySchema), "url")]
[JsonDerivedType(typeof(EmailPropertySchema), "email")]
[JsonDerivedType(typeof(PhoneNumberPropertySchema), "phone_number")]
[JsonDerivedType(typeof(FormulaPropertySchema), "formula")]
[JsonDerivedType(typeof(RelationPropertySchema), "relation")]
[JsonDerivedType(typeof(RollupPropertySchema), "rollup")]
[JsonDerivedType(typeof(CreatedByPropertySchema), "created_by")]
[JsonDerivedType(typeof(CreatedTimePropertySchema), "created_time")]
[JsonDerivedType(typeof(LastEditedByPropertySchema), "last_edited_by")]
[JsonDerivedType(typeof(LastEditedTimePropertySchema), "last_edited_time")]
[JsonDerivedType(typeof(UniqueIdPropertySchema), "unique_id")]
[JsonDerivedType(typeof(ButtonPropertySchema), "button")]
[JsonDerivedType(typeof(VerificationPropertySchema), "verification")]
public abstract class PropertySchema
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public PropertySchema() { }

    /// <summary>The unique identifier of this property within the database.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The user-visible name of this database column/property.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The Notion property type discriminator string (e.g., "title", "number").</summary>
    [JsonIgnore]
    public virtual string Type => string.Empty;
}
