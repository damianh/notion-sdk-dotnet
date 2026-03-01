// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

public class BlockSerializationExtendedTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void ChildDatabaseBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "child_database",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "child_database": { "title": "My DB" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var childDb = block.ShouldBeOfType<ChildDatabaseBlock>();
        childDb.ChildDatabase.Title.ShouldBe("My DB");
    }

    [Fact]
    public void ChildPageBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "child_page",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "child_page": { "title": "My Page" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var childPage = block.ShouldBeOfType<ChildPageBlock>();
        childPage.ChildPage.Title.ShouldBe("My Page");
    }

    [Fact]
    public void ColumnBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "column",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "column": {}
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        block.ShouldBeOfType<ColumnBlock>();
    }

    [Fact]
    public void ColumnListBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "column_list",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "column_list": {}
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        block.ShouldBeOfType<ColumnListBlock>();
    }

    [Fact]
    public void EmbedBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "embed",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "embed": { "url": "https://example.com" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var embed = block.ShouldBeOfType<EmbedBlock>();
        embed.Embed.Url.ShouldBe("https://example.com");
    }

    [Fact]
    public void FileBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "file",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "file": {
            "type": "external",
            "external": { "url": "https://example.com/f.pdf" },
            "caption": []
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var fileBlock = block.ShouldBeOfType<FileBlock>();
        fileBlock.File.ShouldNotBeNull();
        fileBlock.File.External.ShouldNotBeNull();
        fileBlock.File.External.Url.ShouldBe("https://example.com/f.pdf");
    }

    [Fact]
    public void Heading2Block_DeserializesIsToggleable()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "heading_2",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "heading_2": {
            "rich_text": [],
            "color": "default",
            "is_toggleable": true
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var h2 = block.ShouldBeOfType<Heading2Block>();
        h2.Heading2.IsToggleable.ShouldBeTrue();
    }

    [Fact]
    public void Heading3Block_DeserializesColor()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "heading_3",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "heading_3": {
            "rich_text": [],
            "color": "red",
            "is_toggleable": false
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var h3 = block.ShouldBeOfType<Heading3Block>();
        h3.Heading3.Color.ShouldBe(ApiColor.Red);
    }

    [Fact]
    public void LinkPreviewBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "link_preview",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "link_preview": { "url": "https://github.com" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var linkPreview = block.ShouldBeOfType<LinkPreviewBlock>();
        linkPreview.LinkPreview.Url.ShouldBe("https://github.com");
    }

    [Fact]
    public void LinkToPageBlock_WithPageId_DeserializesAsPageLinkToPage()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "link_to_page",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "link_to_page": { "type": "page_id", "page_id": "abc" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var linkToPage = block.ShouldBeOfType<LinkToPageBlock>();
        var pageLinkToPage = linkToPage.LinkToPage.ShouldBeOfType<PageLinkToPage>();
        pageLinkToPage.PageId.ShouldBe("abc");
    }

    [Fact]
    public void LinkToPageBlock_WithDatabaseId_DeserializesAsDatabaseLinkToPage()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "link_to_page",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "link_to_page": { "type": "database_id", "database_id": "def" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var linkToPage = block.ShouldBeOfType<LinkToPageBlock>();
        var databaseLinkToPage = linkToPage.LinkToPage.ShouldBeOfType<DatabaseLinkToPage>();
        databaseLinkToPage.DatabaseId.ShouldBe("def");
    }

    [Fact]
    public void PdfBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "pdf",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "pdf": {
            "type": "external",
            "external": { "url": "https://example.com/doc.pdf" },
            "caption": []
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var pdf = block.ShouldBeOfType<PdfBlock>();
        pdf.Pdf.External.ShouldNotBeNull();
        pdf.Pdf.External.Url.ShouldBe("https://example.com/doc.pdf");
    }

    [Fact]
    public void SyncedBlock_Original_HasNullSyncedFrom()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "synced_block",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "synced_block": { "synced_from": null }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var synced = block.ShouldBeOfType<SyncedBlock>();
        synced.SyncedBlockContent.SyncedFrom.ShouldBeNull();
    }

    [Fact]
    public void SyncedBlock_Copy_HasPopulatedSyncedFrom()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "synced_block",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "synced_block": {
            "synced_from": { "type": "block_id", "block_id": "sync-source" }
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var synced = block.ShouldBeOfType<SyncedBlock>();
        synced.SyncedBlockContent.SyncedFrom.ShouldNotBeNull();
        synced.SyncedBlockContent.SyncedFrom.BlockId.ShouldBe("sync-source");
    }

    [Fact]
    public void TableBlock_DeserializesTableMetadata()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "table",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "table": {
            "table_width": 3,
            "has_column_header": true,
            "has_row_header": false
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var table = block.ShouldBeOfType<TableBlock>();
        table.Table.TableWidth.ShouldBe(3);
        table.Table.HasColumnHeader.ShouldBeTrue();
        table.Table.HasRowHeader.ShouldBeFalse();
    }

    [Fact]
    public void TableOfContentsBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "table_of_contents",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "table_of_contents": { "color": "default" }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var toc = block.ShouldBeOfType<TableOfContentsBlock>();
        toc.TableOfContents.Color.ShouldBe(ApiColor.Default);
    }

    [Fact]
    public void TableRowBlock_DeserializesCells()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "table_row",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "table_row": {
            "cells": [
              [{ "type": "text", "text": { "content": "A" }, "plain_text": "A" }]
            ]
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var tableRow = block.ShouldBeOfType<TableRowBlock>();
        tableRow.TableRow.Cells.ShouldHaveSingleItem();
        var cell = tableRow.TableRow.Cells[0].ShouldHaveSingleItem();
        ((TextRichTextItem)cell).Text.Content.ShouldBe("A");
    }

    [Fact]
    public void TemplateBlock_DeserializesRichText()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "template",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "template": {
            "rich_text": [
              { "type": "text", "text": { "content": "Template" }, "plain_text": "Template" }
            ]
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var template = block.ShouldBeOfType<TemplateBlock>();
        template.Template.RichText.ShouldHaveSingleItem();
        ((TextRichTextItem)template.Template.RichText[0]).Text.Content.ShouldBe("Template");
    }

    [Fact]
    public void VideoBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "test-id",
          "type": "video",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "video": {
            "type": "external",
            "external": { "url": "https://youtube.com/watch?v=test" },
            "caption": []
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var video = block.ShouldBeOfType<VideoBlock>();
        video.Video.External.ShouldNotBeNull();
        video.Video.External.Url.ShouldBe("https://youtube.com/watch?v=test");
    }
}
