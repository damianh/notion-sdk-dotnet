// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// Abstract base for all Notion block types.
/// The "type" discriminator selects the concrete subclass.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AudioBlock), "audio")]
[JsonDerivedType(typeof(BookmarkBlock), "bookmark")]
[JsonDerivedType(typeof(BreadcrumbBlock), "breadcrumb")]
[JsonDerivedType(typeof(BulletedListItemBlock), "bulleted_list_item")]
[JsonDerivedType(typeof(CalloutBlock), "callout")]
[JsonDerivedType(typeof(ChildDatabaseBlock), "child_database")]
[JsonDerivedType(typeof(ChildPageBlock), "child_page")]
[JsonDerivedType(typeof(CodeBlock), "code")]
[JsonDerivedType(typeof(ColumnBlock), "column")]
[JsonDerivedType(typeof(ColumnListBlock), "column_list")]
[JsonDerivedType(typeof(DividerBlock), "divider")]
[JsonDerivedType(typeof(EmbedBlock), "embed")]
[JsonDerivedType(typeof(EquationBlock), "equation")]
[JsonDerivedType(typeof(FileBlock), "file")]
[JsonDerivedType(typeof(Heading1Block), "heading_1")]
[JsonDerivedType(typeof(Heading2Block), "heading_2")]
[JsonDerivedType(typeof(Heading3Block), "heading_3")]
[JsonDerivedType(typeof(ImageBlock), "image")]
[JsonDerivedType(typeof(LinkPreviewBlock), "link_preview")]
[JsonDerivedType(typeof(LinkToPageBlock), "link_to_page")]
[JsonDerivedType(typeof(NumberedListItemBlock), "numbered_list_item")]
[JsonDerivedType(typeof(ParagraphBlock), "paragraph")]
[JsonDerivedType(typeof(PdfBlock), "pdf")]
[JsonDerivedType(typeof(QuoteBlock), "quote")]
[JsonDerivedType(typeof(SyncedBlock), "synced_block")]
[JsonDerivedType(typeof(TableBlock), "table")]
[JsonDerivedType(typeof(TableOfContentsBlock), "table_of_contents")]
[JsonDerivedType(typeof(TableRowBlock), "table_row")]
[JsonDerivedType(typeof(TemplateBlock), "template")]
[JsonDerivedType(typeof(ToDoBlock), "to_do")]
[JsonDerivedType(typeof(ToggleBlock), "toggle")]
[JsonDerivedType(typeof(UnsupportedBlock), "unsupported")]
[JsonDerivedType(typeof(VideoBlock), "video")]
public abstract class Block
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public Block() { }

    /// <summary>Gets the Notion object type identifier, always <c>"block"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "block";

    /// <summary>Gets the unique identifier of the block.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = null!;

    /// <summary>Gets the Notion API type discriminator string for this block (e.g. <c>"paragraph"</c>).</summary>
    [JsonIgnore]
    public virtual string Type => string.Empty;

    /// <summary>Gets the parent object that contains this block (a page, database, or another block).</summary>
    [JsonPropertyName("parent")]
    public Parent? Parent { get; init; }

    /// <summary>Gets the ISO 8601 date-time string of when the block was created.</summary>
    [JsonPropertyName("created_time")]
    public string? CreatedTime { get; init; }

    /// <summary>Gets the user who created the block.</summary>
    [JsonPropertyName("created_by")]
    public PartialUser? CreatedBy { get; init; }

    /// <summary>Gets the ISO 8601 date-time string of when the block was last edited.</summary>
    [JsonPropertyName("last_edited_time")]
    public string? LastEditedTime { get; init; }

    /// <summary>Gets the user who last edited the block.</summary>
    [JsonPropertyName("last_edited_by")]
    public PartialUser? LastEditedBy { get; init; }

    /// <summary>Gets a value indicating whether the block has child blocks nested beneath it.</summary>
    [JsonPropertyName("has_children")]
    public bool HasChildren { get; init; }

    /// <summary>Gets a value indicating whether the block has been archived.</summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; init; }

    /// <summary>Gets a value indicating whether the block is in the Notion trash.</summary>
    [JsonPropertyName("in_trash")]
    public bool InTrash { get; init; }
}

/// <summary>
/// Abstract base for the polymorphic link target used inside a <see cref="LinkToPageBlock"/>.
/// Concrete subtypes are <see cref="PageLinkToPage"/>, <see cref="DatabaseLinkToPage"/>, and <see cref="CommentLinkToPage"/>.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PageLinkToPage), "page_id")]
[JsonDerivedType(typeof(DatabaseLinkToPage), "database_id")]
[JsonDerivedType(typeof(CommentLinkToPage), "comment_id")]
public abstract class LinkToPageContent
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public LinkToPageContent() { }

    /// <summary>Gets the Notion API type discriminator string that identifies the kind of link target (e.g. <c>"page_id"</c>).</summary>
    [JsonIgnore]
    public virtual string LinkType => string.Empty;
}
