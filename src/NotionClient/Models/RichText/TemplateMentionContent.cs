// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// Abstract base class for the content of a template mention in rich text.
/// Template mentions are placeholders that resolve at page-creation time.
/// Subtypes: <see cref="TemplateMentionDate"/> (template_mention_date) and
/// <see cref="TemplateMentionUser"/> (template_mention_user).
/// <see href="https://developers.notion.com/reference/rich-text#template-mention-type-object"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TemplateMentionDate), "template_mention_date")]
[JsonDerivedType(typeof(TemplateMentionUser), "template_mention_user")]
public abstract class TemplateMentionContent
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public TemplateMentionContent() { }
}
