// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Filters;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

public sealed class CreatePageRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void CreatePageRequest_WithParentAndProperties_SerializesCorrectly()
    {
        var request = new CreatePageRequest
        {
            Parent = new DatabaseParent { DatabaseId = "db-abc" },
            Properties = new Dictionary<string, PropertyValue>
            {
                ["Name"] = new TitlePropertyValue
                {
                    Title =
                    [
                        new TextRichTextItem
                        {
                            Text = new TextContent { Content = "New Page" },
                            PlainText = "New Page"
                        }
                    ]
                }
            }
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var parent = root.GetProperty("parent");
        parent.GetProperty("database_id").GetString().ShouldBe("db-abc");

        var properties = root.GetProperty("properties");
        properties.TryGetProperty("Name", out _).ShouldBeTrue();
    }
}

public sealed class UpdatePageRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void UpdatePageRequest_WithProperties_SerializesCorrectly()
    {
        var request = new UpdatePageRequest
        {
            Properties = new Dictionary<string, PropertyValue>
            {
                ["Status"] = new SelectPropertyValue
                {
                    Select = new SelectOption { Name = "In Progress" }
                }
            },
            Archived = false
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var properties = root.GetProperty("properties");
        properties.TryGetProperty("Status", out var statusProp).ShouldBeTrue();
        statusProp.GetProperty("type").GetString().ShouldBe("select");
        root.GetProperty("archived").GetBoolean().ShouldBeFalse();
    }
}

public sealed class CreateDatabaseRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void CreateDatabaseRequest_WithParentTitleAndProperties_SerializesCorrectly()
    {
        var request = new CreateDatabaseRequest
        {
            Parent = new PageParent { PageId = "page-xyz" },
            Title =
            [
                new TextRichTextItem
                {
                    Text = new TextContent { Content = "My Database" },
                    PlainText = "My Database"
                }
            ],
            Properties = new Dictionary<string, PropertySchema>
            {
                ["Name"] = new TitlePropertySchema(),
                ["Priority"] = new NumberPropertySchema
                {
                    Number = new NumberConfig { Format = "number" }
                }
            }
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var parent = root.GetProperty("parent");
        parent.GetProperty("page_id").GetString().ShouldBe("page-xyz");

        var title = root.GetProperty("title");
        title.GetArrayLength().ShouldBe(1);

        var properties = root.GetProperty("properties");
        properties.TryGetProperty("Name", out var nameProp).ShouldBeTrue();
        nameProp.GetProperty("type").GetString().ShouldBe("title");
        properties.TryGetProperty("Priority", out var priorityProp).ShouldBeTrue();
        priorityProp.GetProperty("type").GetString().ShouldBe("number");
    }
}

public sealed class UpdateDatabaseRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void UpdateDatabaseRequest_WithTitle_SerializesCorrectly()
    {
        var request = new UpdateDatabaseRequest
        {
            Title =
            [
                new TextRichTextItem
                {
                    Text = new TextContent { Content = "Updated Title" },
                    PlainText = "Updated Title"
                }
            ]
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var title = root.GetProperty("title");
        title.GetArrayLength().ShouldBe(1);
        title[0].GetProperty("plain_text").GetString().ShouldBe("Updated Title");
    }
}

public sealed class QueryDatabaseRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void QueryDatabaseRequest_WithFilterAndSorts_SerializesCorrectly()
    {
        var request = new QueryDatabaseRequest
        {
            Filter = new PropertyFilter
            {
                Property = "Status",
                Select = new SelectFilterCondition { EqualsValue = "Done" }
            },
            Sorts =
            [
                new Sort { Property = "Name", Direction = "ascending" }
            ],
            PageSize = 10
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        root.TryGetProperty("filter", out var filter).ShouldBeTrue();
        filter.GetProperty("property").GetString().ShouldBe("Status");

        root.TryGetProperty("sorts", out var sorts).ShouldBeTrue();
        sorts.GetArrayLength().ShouldBe(1);
        sorts[0].GetProperty("direction").GetString().ShouldBe("ascending");

        root.GetProperty("page_size").GetInt32().ShouldBe(10);
    }
}

public sealed class AppendBlockChildrenRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void AppendBlockChildrenRequest_WithChildren_SerializesCorrectly()
    {
        var request = new AppendBlockChildrenRequest
        {
            Children =
            [
                new ParagraphBlock
                {
                    Paragraph = new RichTextWithColorAndChildren
                    {
                        RichText =
                        [
                            new TextRichTextItem
                            {
                                Text = new TextContent { Content = "Hello block" },
                                PlainText = "Hello block"
                            }
                        ]
                    }
                }
            ]
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        root.TryGetProperty("children", out var children).ShouldBeTrue();
        children.GetArrayLength().ShouldBe(1);
        children[0].GetProperty("type").GetString().ShouldBe("paragraph");
    }
}

public sealed class UpdateBlockRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void UpdateBlockRequest_Empty_ThrowsWithSourceGenContext()
    {
        // UpdateBlockRequest uses [JsonExtensionData] on an init-only property.
        // STJ source generation treats init-only properties as constructor parameters,
        // but [JsonExtensionData] cannot bind to constructor parameters — this is a
        // known STJ limitation. Serialization via the source-generated context throws.
        var request = new UpdateBlockRequest();
        Should.Throw<InvalidOperationException>(() => JsonSerializer.Serialize(request, JsonOptions));
    }
}

public sealed class CreateCommentRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void CreateCommentRequest_WithParentAndRichText_SerializesCorrectly()
    {
        var request = new CreateCommentRequest
        {
            Parent = new PageParent { PageId = "page-abc" },
            RichText =
            [
                new TextRichTextItem
                {
                    Text = new TextContent { Content = "Looks good!" },
                    PlainText = "Looks good!"
                }
            ]
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var parent = root.GetProperty("parent");
        parent.GetProperty("page_id").GetString().ShouldBe("page-abc");

        var richText = root.GetProperty("rich_text");
        richText.GetArrayLength().ShouldBe(1);
        richText[0].GetProperty("plain_text").GetString().ShouldBe("Looks good!");
    }
}

public sealed class SearchRequestSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void SearchRequest_WithQueryFilterAndSort_SerializesCorrectly()
    {
        var request = new SearchRequest
        {
            Query = "My Page",
            Filter = new SearchFilter { Value = "page" },
            Sort = new SearchSort { Direction = "descending" },
            PageSize = 25
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        root.GetProperty("query").GetString().ShouldBe("My Page");

        var filter = root.GetProperty("filter");
        filter.GetProperty("value").GetString().ShouldBe("page");
        filter.GetProperty("property").GetString().ShouldBe("object");

        var sort = root.GetProperty("sort");
        sort.GetProperty("direction").GetString().ShouldBe("descending");
        sort.GetProperty("timestamp").GetString().ShouldBe("last_edited_time");

        root.GetProperty("page_size").GetInt32().ShouldBe(25);
    }
}
