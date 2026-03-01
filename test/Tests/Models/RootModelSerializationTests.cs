// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

public sealed class PageDeserializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void Page_WithFullNestedJson_DeserializesCorrectly()
    {
        var json = """
        {
          "object": "page",
          "id": "page-123",
          "created_time": "2024-01-01T00:00:00.000Z",
          "last_edited_time": "2024-06-01T12:00:00.000Z",
          "created_by": {"object": "user", "id": "user-1"},
          "last_edited_by": {"object": "user", "id": "user-2"},
          "archived": false,
          "in_trash": false,
          "icon": {"type": "emoji", "emoji": "📄"},
          "cover": {"type": "external", "external": {"url": "https://example.com/cover.jpg"}},
          "parent": {"type": "database_id", "database_id": "db-1"},
          "url": "https://www.notion.so/page-123",
          "public_url": null,
          "properties": {
            "Name": {"id": "title", "type": "title", "title": [{"type": "text", "text": {"content": "My Page"}, "plain_text": "My Page"}]},
            "Status": {"id": "status", "type": "select", "select": {"id": "opt-1", "name": "Done", "color": "green"}},
            "Count": {"id": "num", "type": "number", "number": 42}
          }
        }
        """;

        var page = JsonSerializer.Deserialize<Page>(json, JsonOptions);

        page.ShouldNotBeNull();
        page.Id.ShouldBe("page-123");
        page.Properties.Count.ShouldBe(3);

        var name = page.Properties["Name"].ShouldBeOfType<TitlePropertyValue>();
        name.Title.ShouldHaveSingleItem();
        ((TextRichTextItem)name.Title[0]).Text.Content.ShouldBe("My Page");

        page.Properties["Status"].ShouldBeOfType<SelectPropertyValue>();
        page.Properties["Count"].ShouldBeOfType<NumberPropertyValue>();

        page.Icon.ShouldBeOfType<EmojiIcon>().Emoji.ShouldBe("📄");
        page.Cover.ShouldBeOfType<ExternalPageCover>();
        page.Parent.ShouldBeOfType<DatabaseParent>().DatabaseId.ShouldBe("db-1");
    }

    [Fact]
    public void Page_PropertiesHaveCorrectTypes()
    {
        var json = """
        {
          "object": "page",
          "id": "page-123",
          "archived": false,
          "in_trash": false,
          "properties": {
            "Name": {"id": "title", "type": "title", "title": [{"type": "text", "text": {"content": "My Page"}, "plain_text": "My Page"}]},
            "Status": {"id": "status", "type": "select", "select": {"id": "opt-1", "name": "Done", "color": "green"}},
            "Count": {"id": "num", "type": "number", "number": 42}
          }
        }
        """;

        var page = JsonSerializer.Deserialize<Page>(json, JsonOptions);

        page.ShouldNotBeNull();
        page.Properties["Name"].ShouldBeOfType<TitlePropertyValue>();
        var status = page.Properties["Status"].ShouldBeOfType<SelectPropertyValue>();
        status.Select?.Name.ShouldBe("Done");
        var count = page.Properties["Count"].ShouldBeOfType<NumberPropertyValue>();
        count.Number.ShouldBe(42);
    }
}

public sealed class DatabaseDeserializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void Database_WithPropertySchemas_DeserializesCorrectly()
    {
        var json = """
        {
          "object": "database",
          "id": "db-123",
          "created_time": "2024-01-01T00:00:00.000Z",
          "last_edited_time": "2024-06-01T12:00:00.000Z",
          "title": [{"type": "text", "text": {"content": "Task Tracker"}, "plain_text": "Task Tracker"}],
          "description": [],
          "icon": {"type": "emoji", "emoji": "📋"},
          "cover": null,
          "archived": false,
          "in_trash": false,
          "is_inline": false,
          "parent": {"type": "page_id", "page_id": "parent-page-1"},
          "url": "https://www.notion.so/db-123",
          "public_url": null,
          "properties": {
            "Name": {"id": "title", "name": "Name", "type": "title", "title": {}},
            "Priority": {"id": "num", "name": "Priority", "type": "number", "number": {"format": "number"}},
            "Tags": {"id": "ms", "name": "Tags", "type": "multi_select", "multi_select": {"options": [{"id": "a", "name": "Bug", "color": "red"}]}}
          }
        }
        """;

        var db = JsonSerializer.Deserialize<Database>(json, JsonOptions);

        db.ShouldNotBeNull();
        db.Id.ShouldBe("db-123");
        db.Properties.Count.ShouldBe(3);

        db.Properties["Name"].ShouldBeOfType<TitlePropertySchema>();
        db.Properties["Priority"].ShouldBeOfType<NumberPropertySchema>();
        var tags = db.Properties["Tags"].ShouldBeOfType<MultiSelectPropertySchema>();
        tags.MultiSelect?.Options.ShouldHaveSingleItem();

        db.Title.ShouldHaveSingleItem();
        ((TextRichTextItem)db.Title[0]).Text.Content.ShouldBe("Task Tracker");

        db.Parent.ShouldBeOfType<PageParent>().PageId.ShouldBe("parent-page-1");
        db.Icon.ShouldBeOfType<EmojiIcon>().Emoji.ShouldBe("📋");
    }
}

public sealed class CommentDeserializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void Comment_WithRichText_DeserializesCorrectly()
    {
        var json = """
        {
          "object": "comment",
          "id": "comment-1",
          "parent": {"type": "page_id", "page_id": "page-1"},
          "discussion_id": "disc-1",
          "created_time": "2024-03-15T10:30:00.000Z",
          "created_by": {"object": "user", "id": "user-1"},
          "last_edited_time": "2024-03-15T10:30:00.000Z",
          "rich_text": [{"type": "text", "text": {"content": "Great work!"}, "plain_text": "Great work!"}]
        }
        """;

        var comment = JsonSerializer.Deserialize<Comment>(json, JsonOptions);

        comment.ShouldNotBeNull();
        comment.Id.ShouldBe("comment-1");
        comment.DiscussionId.ShouldBe("disc-1");
        comment.RichText.ShouldHaveSingleItem();
        ((TextRichTextItem)comment.RichText[0]).Text.Content.ShouldBe("Great work!");
        comment.Parent.ShouldBeOfType<PageParent>().PageId.ShouldBe("page-1");
    }
}

public sealed class PaginatedListDeserializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void PaginatedList_OfPage_DeserializesCorrectly()
    {
        var json = """
        {
          "object": "list",
          "results": [
            {"object": "page", "id": "p1", "archived": false, "in_trash": false, "properties": {}},
            {"object": "page", "id": "p2", "archived": false, "in_trash": false, "properties": {}}
          ],
          "next_cursor": "cursor-xyz",
          "has_more": true,
          "type": "page_or_database"
        }
        """;

        var list = JsonSerializer.Deserialize<PaginatedList<Page>>(json, JsonOptions);

        list.ShouldNotBeNull();
        list.Results.Count.ShouldBe(2);
        list.NextCursor.ShouldBe("cursor-xyz");
        list.HasMore.ShouldBeTrue();
        list.Results[0].Id.ShouldBe("p1");
        list.Results[1].Id.ShouldBe("p2");
    }

    [Fact]
    public void PaginatedList_OfSearchResult_DeserializesPolymorphicItems()
    {
        var json = """
        {
          "object": "list",
          "results": [
            {"object": "page", "id": "sr-1"},
            {"object": "database", "id": "sr-2"}
          ],
          "next_cursor": null,
          "has_more": false,
          "type": "page_or_database"
        }
        """;

        var list = JsonSerializer.Deserialize<PaginatedList<SearchResult>>(json, JsonOptions);

        list.ShouldNotBeNull();
        list.Results.Count.ShouldBe(2);
        list.HasMore.ShouldBeFalse();
        list.NextCursor.ShouldBeNull();
        list.Results[0].ShouldBeOfType<PageSearchResult>().Id.ShouldBe("sr-1");
        list.Results[1].ShouldBeOfType<DatabaseSearchResult>().Id.ShouldBe("sr-2");
    }
}
