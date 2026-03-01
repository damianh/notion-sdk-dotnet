// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

public sealed class RichTextSerializationExtendedTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void DateMention_Deserializes()
    {
        var json = """{"type":"date","date":{"start":"2024-01-01","end":null,"time_zone":null}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var dateMention = mention.ShouldBeOfType<DateMention>();
        dateMention.Date.Start.ShouldBe("2024-01-01");
    }

    [Fact]
    public void PageMention_Deserializes()
    {
        var json = """{"type":"page","page":{"id":"page-123"}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var pageMention = mention.ShouldBeOfType<PageMention>();
        pageMention.Page.Id.ShouldBe("page-123");
    }

    [Fact]
    public void DatabaseMention_Deserializes()
    {
        var json = """{"type":"database","database":{"id":"db-123"}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var databaseMention = mention.ShouldBeOfType<DatabaseMention>();
        databaseMention.Database.Id.ShouldBe("db-123");
    }

    [Fact]
    public void LinkPreviewMention_Deserializes()
    {
        var json = """{"type":"link_preview","link_preview":{"url":"https://github.com/repo"}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var linkPreviewMention = mention.ShouldBeOfType<LinkPreviewMention>();
        linkPreviewMention.LinkPreview.Url.ShouldBe("https://github.com/repo");
    }

    [Fact]
    public void LinkMention_Deserializes()
    {
        var json = """{"type":"link_mention","link_mention":{"href":"https://example.com","title":"Example","icon_url":null,"description":null}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var linkMention = mention.ShouldBeOfType<LinkMention>();
        linkMention.LinkMentionData.Href.ShouldBe("https://example.com");
        linkMention.LinkMentionData.Title.ShouldBe("Example");
    }

    [Fact]
    public void TemplateMention_WithDate_Deserializes()
    {
        var json = """{"type":"template_mention","template_mention":{"type":"template_mention_date","template_mention_date":"today"}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var templateMention = mention.ShouldBeOfType<TemplateMention>();
        templateMention.TemplateMentionData.ShouldNotBeNull();
        var dateContent = templateMention.TemplateMentionData.ShouldBeOfType<TemplateMentionDate>();
        dateContent.TemplateMentionDateValue.ShouldBe("today");
    }

    [Fact]
    public void TemplateMention_WithUser_Deserializes()
    {
        var json = """{"type":"template_mention","template_mention":{"type":"template_mention_user","template_mention_user":"me"}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var templateMention = mention.ShouldBeOfType<TemplateMention>();
        templateMention.TemplateMentionData.ShouldNotBeNull();
        var userContent = templateMention.TemplateMentionData.ShouldBeOfType<TemplateMentionUser>();
        userContent.TemplateMentionUserValue.ShouldBe("me");
    }

    [Fact]
    public void CustomEmojiMention_Deserializes()
    {
        var json = """{"type":"custom_emoji","custom_emoji":{"id":"emoji-1","name":"tada","url":"https://example.com/emoji.png"}}""";

        var mention = JsonSerializer.Deserialize<Mention>(json, JsonOptions);
        var customEmojiMention = mention.ShouldBeOfType<CustomEmojiMention>();
        customEmojiMention.CustomEmoji.Id.ShouldBe("emoji-1");
        customEmojiMention.CustomEmoji.Name.ShouldBe("tada");
        customEmojiMention.CustomEmoji.Url.ShouldBe("https://example.com/emoji.png");
    }

    [Fact]
    public void MentionRichTextItem_WithDateMention_Deserializes()
    {
        var json = """{"type":"mention","mention":{"type":"date","date":{"start":"2024-01-01","end":null,"time_zone":null}},"plain_text":"2024-01-01"}""";

        var item = JsonSerializer.Deserialize<RichTextItem>(json, JsonOptions);
        var mentionItem = item.ShouldBeOfType<MentionRichTextItem>();
        mentionItem.PlainText.ShouldBe("2024-01-01");
        var dateMention = mentionItem.Mention.ShouldBeOfType<DateMention>();
        dateMention.Date.Start.ShouldBe("2024-01-01");
    }

    [Fact]
    public void MentionRichTextItem_WithPageMention_Deserializes()
    {
        var json = """{"type":"mention","mention":{"type":"page","page":{"id":"page-abc"}},"plain_text":"My Page"}""";

        var item = JsonSerializer.Deserialize<RichTextItem>(json, JsonOptions);
        var mentionItem = item.ShouldBeOfType<MentionRichTextItem>();
        mentionItem.PlainText.ShouldBe("My Page");
        var pageMention = mentionItem.Mention.ShouldBeOfType<PageMention>();
        pageMention.Page.Id.ShouldBe("page-abc");
    }

    [Fact]
    public void MentionRichTextItem_WithTemplateMention_Deserializes()
    {
        var json = """{"type":"mention","mention":{"type":"template_mention","template_mention":{"type":"template_mention_date","template_mention_date":"today"}},"plain_text":"@today"}""";

        var item = JsonSerializer.Deserialize<RichTextItem>(json, JsonOptions);
        var mentionItem = item.ShouldBeOfType<MentionRichTextItem>();
        mentionItem.PlainText.ShouldBe("@today");
        var templateMention = mentionItem.Mention.ShouldBeOfType<TemplateMention>();
        templateMention.TemplateMentionData.ShouldNotBeNull();
        var dateContent = templateMention.TemplateMentionData.ShouldBeOfType<TemplateMentionDate>();
        dateContent.TemplateMentionDateValue.ShouldBe("today");
    }
}
