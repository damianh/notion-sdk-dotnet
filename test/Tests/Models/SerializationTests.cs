// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Filters;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

public class BlockSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void ParagraphBlock_RoundTrips()
    {
        var json = """
        {
          "object": "block",
          "id": "abc123",
          "type": "paragraph",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "paragraph": {
            "rich_text": [
              {
                "type": "text",
                "text": { "content": "Hello, world!" },
                "plain_text": "Hello, world!"
              }
            ],
            "color": "default"
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        block.ShouldNotBeNull();
        var para = block.ShouldBeOfType<ParagraphBlock>();
        para.Type.ShouldBe("paragraph");
        para.Paragraph.RichText.ShouldHaveSingleItem();
        ((TextRichTextItem)para.Paragraph.RichText[0]).Text.Content.ShouldBe("Hello, world!");
    }

    [Fact]
    public void Heading1Block_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "def456",
          "type": "heading_1",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "heading_1": {
            "rich_text": [],
            "color": "default",
            "is_toggleable": false
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        block.ShouldBeOfType<Heading1Block>();
    }

    [Fact]
    public void ToDoBlock_PreservesChecked()
    {
        var json = """
        {
          "object": "block",
          "id": "ghi789",
          "type": "to_do",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "to_do": {
            "rich_text": [],
            "checked": true,
            "color": "default"
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var todo = block.ShouldBeOfType<ToDoBlock>();
        todo.ToDo.Checked.ShouldBeTrue();
    }

    [Fact]
    public void CodeBlock_Deserializes()
    {
        var json = """
        {
          "object": "block",
          "id": "jkl012",
          "type": "code",
          "has_children": false,
          "archived": false,
          "in_trash": false,
          "code": {
            "rich_text": [{ "type": "text", "text": { "content": "var x = 1;" }, "plain_text": "var x = 1;" }],
            "caption": [],
            "language": "javascript"
          }
        }
        """;

        var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
        var code = block.ShouldBeOfType<CodeBlock>();
        code.Code.Language.ShouldBe("javascript");
    }

    [Fact]
    public void AllBlockTypes_DeserializeWithoutException()
    {
        var blockTypes = new[]
        {
            ("audio", """{"type":"file","file":{"url":"https://example.com/audio.mp3"},"caption":[]}""", "audio"),
            ("bookmark", """{"url":"https://example.com","caption":[]}""", "bookmark"),
            ("breadcrumb", "{}", "breadcrumb"),
            ("bulleted_list_item", """{"rich_text":[],"color":"default"}""", "bulleted_list_item"),
            ("callout", """{"rich_text":[],"color":"default"}""", "callout"),
            ("divider", "{}", "divider"),
            ("equation", """{"expression":"E=mc^2"}""", "equation"),
            ("image", """{"type":"external","external":{"url":"https://example.com/img.png"},"caption":[]}""", "image"),
            ("numbered_list_item", """{"rich_text":[],"color":"default"}""", "numbered_list_item"),
            ("quote", """{"rich_text":[],"color":"default"}""", "quote"),
            ("toggle", """{"rich_text":[],"color":"default"}""", "toggle"),
            ("unsupported", "{}", "unsupported"),
        };

        foreach (var (type, content, jsonKey) in blockTypes)
        {
            var json = $$"""
            {
              "object": "block",
              "id": "test-id",
              "type": "{{type}}",
              "has_children": false,
              "archived": false,
              "in_trash": false,
              "{{jsonKey}}": {{content}}
            }
            """;
            var block = JsonSerializer.Deserialize<Block>(json, JsonOptions);
            block.ShouldNotBeNull();
            block.Type.ShouldBe(type);
        }
    }
}

public class RichTextSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void TextRichTextItem_RoundTrips()
    {
        var json = """
        {
          "type": "text",
          "text": { "content": "Hello" },
          "plain_text": "Hello",
          "href": null
        }
        """;

        var item = JsonSerializer.Deserialize<RichTextItem>(json, JsonOptions);
        var text = item.ShouldBeOfType<TextRichTextItem>();
        text.Text.Content.ShouldBe("Hello");
        text.PlainText.ShouldBe("Hello");
    }

    [Fact]
    public void EquationRichTextItem_Deserializes()
    {
        var json = """
        {
          "type": "equation",
          "equation": { "expression": "x^2" },
          "plain_text": "x^2"
        }
        """;

        var item = JsonSerializer.Deserialize<RichTextItem>(json, JsonOptions);
        var eq = item.ShouldBeOfType<EquationRichTextItem>();
        eq.Equation.Expression.ShouldBe("x^2");
    }

    [Fact]
    public void MentionUserRichTextItem_Deserializes()
    {
        var json = """
        {
          "type": "mention",
          "mention": {
            "type": "user",
            "user": { "object": "user", "id": "user-id-123" }
          },
          "plain_text": "@Alice"
        }
        """;

        var item = JsonSerializer.Deserialize<RichTextItem>(json, JsonOptions);
        var mention = item.ShouldBeOfType<MentionRichTextItem>();
        var userMention = mention.Mention.ShouldBeOfType<UserMention>();
        userMention.User.Id.ShouldBe("user-id-123");
    }
}

public class PropertyValueSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void TitlePropertyValue_Deserializes()
    {
        var json = """
        {
          "id": "title",
          "type": "title",
          "title": [
            { "type": "text", "text": { "content": "My Page" }, "plain_text": "My Page" }
          ]
        }
        """;

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var title = prop.ShouldBeOfType<TitlePropertyValue>();
        title.Title.ShouldHaveSingleItem();
    }

    [Fact]
    public void CheckboxPropertyValue_Deserializes()
    {
        var json = """{"id":"cb","type":"checkbox","checkbox":true}""";
        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var cb = prop.ShouldBeOfType<CheckboxPropertyValue>();
        cb.Checkbox.ShouldBeTrue();
    }

    [Fact]
    public void NumberPropertyValue_Deserializes()
    {
        var json = """{"id":"num","type":"number","number":42.5}""";
        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var num = prop.ShouldBeOfType<NumberPropertyValue>();
        num.Number.ShouldBe(42.5);
    }

    [Fact]
    public void SelectPropertyValue_Deserializes()
    {
        var json = """
        {
          "id": "sel",
          "type": "select",
          "select": { "id": "opt-1", "name": "Option A", "color": "blue" }
        }
        """;
        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var sel = prop.ShouldBeOfType<SelectPropertyValue>();
        sel.Select?.Name.ShouldBe("Option A");
    }

    [Fact]
    public void DatePropertyValue_WithEnd_Deserializes()
    {
        var json = """
        {
          "id": "date",
          "type": "date",
          "date": {
            "start": "2024-01-01",
            "end": "2024-01-31",
            "time_zone": null
          }
        }
        """;
        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var date = prop.ShouldBeOfType<DatePropertyValue>();
        date.Date?.Start.ShouldBe("2024-01-01");
        date.Date?.End.ShouldBe("2024-01-31");
    }

    [Fact]
    public void UniqueIdPropertyValue_Deserializes()
    {
        var json = """
        {
          "id": "uid",
          "type": "unique_id",
          "unique_id": { "number": 42, "prefix": "TASK" }
        }
        """;
        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var uid = prop.ShouldBeOfType<UniqueIdPropertyValue>();
        uid.UniqueId.Number.ShouldBe(42);
        uid.UniqueId.Prefix.ShouldBe("TASK");
    }
}

public class PropertySchemaSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void SelectPropertySchema_DeserializesOptions()
    {
        var json = """
        {
          "id": "sel",
          "name": "Status",
          "type": "select",
          "select": {
            "options": [
              { "id": "a", "name": "To Do", "color": "red" },
              { "id": "b", "name": "Done", "color": "green" }
            ]
          }
        }
        """;
        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var sel = schema.ShouldBeOfType<SelectPropertySchema>();
        sel.Select?.Options.Count.ShouldBe(2);
    }

    [Fact]
    public void NumberPropertySchema_DeserializesFormat()
    {
        var json = """
        {
          "id": "num",
          "name": "Price",
          "type": "number",
          "number": { "format": "dollar" }
        }
        """;
        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var num = schema.ShouldBeOfType<NumberPropertySchema>();
        num.Number?.Format.ShouldBe("dollar");
    }

    [Fact]
    public void FormulaPropertySchema_DeserializesExpression()
    {
        var json = """
        {
          "id": "formula",
          "name": "Computed",
          "type": "formula",
          "formula": { "expression": "prop(\"Name\")" }
        }
        """;
        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var formula = schema.ShouldBeOfType<FormulaPropertySchema>();
        formula.Formula?.Expression.ShouldBe("prop(\"Name\")");
    }
}

public class FilterSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void CompoundFilter_And_SerializesCorrectly()
    {
        var filter = new CompoundFilter
        {
            And = new[]
            {
                new PropertyFilter
                {
                    Property = "Status",
                    Select = new SelectFilterCondition { EqualsValue = "Done" }
                },
                new PropertyFilter
                {
                    Property = "Priority",
                    Number = new NumberFilterCondition { EqualsValue = 1 }
                }
            }
        };

        var json = JsonSerializer.Serialize(filter, JsonOptions);
        json.ShouldContain("\"and\"");
        json.ShouldContain("\"Status\"");
    }

    [Fact]
    public void PropertyFilter_Text_SerializesCorrectly()
    {
        var filter = new PropertyFilter
        {
            Property = "Name",
            Title = new TextFilterCondition { Contains = "Hello" }
        };

        var json = JsonSerializer.Serialize(filter, JsonOptions);
        json.ShouldContain("\"Name\"");
        json.ShouldContain("\"contains\"");
        json.ShouldContain("\"Hello\"");
    }
}

public class PaginatedListSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void PaginatedList_Deserializes()
    {
        var json = """
        {
          "object": "list",
          "results": [],
          "next_cursor": "cursor-abc",
          "has_more": true,
          "type": "block"
        }
        """;

        var list = JsonSerializer.Deserialize<PaginatedList<Block>>(json, JsonOptions);
        list.ShouldNotBeNull();
        list.HasMore.ShouldBeTrue();
        list.NextCursor.ShouldBe("cursor-abc");
        list.Results.ShouldBeEmpty();
    }
}
