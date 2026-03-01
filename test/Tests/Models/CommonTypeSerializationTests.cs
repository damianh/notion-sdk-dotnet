// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models;

public class CommonTypeSerializationTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void Parent_DatabaseId_DeserializesAsDatabaseParent()
    {
        var json = """{"type":"database_id","database_id":"db-id-123"}""";

        var parent = JsonSerializer.Deserialize<Parent>(json, JsonOptions);
        var dbParent = parent.ShouldBeOfType<DatabaseParent>();
        dbParent.DatabaseId.ShouldBe("db-id-123");
    }

    [Fact]
    public void Parent_PageId_DeserializesAsPageParent()
    {
        var json = """{"type":"page_id","page_id":"pg-id-123"}""";

        var parent = JsonSerializer.Deserialize<Parent>(json, JsonOptions);
        var pageParent = parent.ShouldBeOfType<PageParent>();
        pageParent.PageId.ShouldBe("pg-id-123");
    }

    [Fact]
    public void Parent_BlockId_DeserializesAsBlockParent()
    {
        var json = """{"type":"block_id","block_id":"blk-id-123"}""";

        var parent = JsonSerializer.Deserialize<Parent>(json, JsonOptions);
        var blockParent = parent.ShouldBeOfType<BlockParent>();
        blockParent.BlockId.ShouldBe("blk-id-123");
    }

    [Fact]
    public void Parent_Workspace_DeserializesAsWorkspaceParent()
    {
        var json = """{"type":"workspace","workspace":true}""";

        var parent = JsonSerializer.Deserialize<Parent>(json, JsonOptions);
        var workspaceParent = parent.ShouldBeOfType<WorkspaceParent>();
        workspaceParent.Workspace.ShouldBeTrue();
    }

    [Fact]
    public void Icon_Emoji_DeserializesAsEmojiIcon()
    {
        var json = """{"type":"emoji","emoji":"🚀"}""";

        var icon = JsonSerializer.Deserialize<Icon>(json, JsonOptions);
        var emojiIcon = icon.ShouldBeOfType<EmojiIcon>();
        emojiIcon.Emoji.ShouldBe("🚀");
    }

    [Fact]
    public void Icon_External_DeserializesAsExternalIcon()
    {
        var json = """{"type":"external","external":{"url":"https://example.com/icon.png"}}""";

        var icon = JsonSerializer.Deserialize<Icon>(json, JsonOptions);
        var externalIcon = icon.ShouldBeOfType<ExternalIcon>();
        externalIcon.External.Url.ShouldBe("https://example.com/icon.png");
    }

    [Fact]
    public void Icon_File_DeserializesAsFileIcon()
    {
        var json = """{"type":"file","file":{"url":"https://s3.example.com/icon.png","expiry_time":"2025-01-01T00:00:00.000Z"}}""";

        var icon = JsonSerializer.Deserialize<Icon>(json, JsonOptions);
        var fileIcon = icon.ShouldBeOfType<FileIcon>();
        fileIcon.File.Url.ShouldBe("https://s3.example.com/icon.png");
    }

    [Fact]
    public void Icon_CustomEmoji_DeserializesAsCustomEmojiIcon()
    {
        var json = """{"type":"custom_emoji","custom_emoji":{"id":"emoji-id","name":"thumbsup","url":"https://example.com/emoji.png"}}""";

        var icon = JsonSerializer.Deserialize<Icon>(json, JsonOptions);
        var customEmojiIcon = icon.ShouldBeOfType<CustomEmojiIcon>();
        customEmojiIcon.CustomEmoji.Id.ShouldBe("emoji-id");
        customEmojiIcon.CustomEmoji.Name.ShouldBe("thumbsup");
        customEmojiIcon.CustomEmoji.Url.ShouldBe("https://example.com/emoji.png");
    }

    [Fact]
    public void PageCover_External_DeserializesAsExternalPageCover()
    {
        var json = """{"type":"external","external":{"url":"https://example.com/cover.png"}}""";

        var cover = JsonSerializer.Deserialize<PageCover>(json, JsonOptions);
        var externalCover = cover.ShouldBeOfType<ExternalPageCover>();
        externalCover.External.Url.ShouldBe("https://example.com/cover.png");
    }

    [Fact]
    public void PageCover_File_DeserializesAsFilePageCover()
    {
        var json = """{"type":"file","file":{"url":"https://s3.example.com/cover.png","expiry_time":"2025-01-01T00:00:00.000Z"}}""";

        var cover = JsonSerializer.Deserialize<PageCover>(json, JsonOptions);
        var fileCover = cover.ShouldBeOfType<FilePageCover>();
        fileCover.File.Url.ShouldBe("https://s3.example.com/cover.png");
    }

    [Fact]
    public void User_Person_DeserializesAsPersonUser()
    {
        var json = """
        {
          "object": "user",
          "id": "user-1",
          "type": "person",
          "name": "Alice",
          "person": { "email": "alice@example.com" }
        }
        """;

        var user = JsonSerializer.Deserialize<User>(json, JsonOptions);
        var personUser = user.ShouldBeOfType<PersonUser>();
        personUser.Person.ShouldNotBeNull();
        personUser.Person.Email.ShouldBe("alice@example.com");
    }

    [Fact]
    public void User_Bot_DeserializesAsBotUser()
    {
        var json = """
        {
          "object": "user",
          "id": "bot-1",
          "type": "bot",
          "name": "My Bot",
          "bot": { "owner": { "type": "workspace", "workspace": true } }
        }
        """;

        var user = JsonSerializer.Deserialize<User>(json, JsonOptions);
        var botUser = user.ShouldBeOfType<BotUser>();
        botUser.Bot.ShouldNotBeNull();
        botUser.Bot.Owner.ShouldNotBeNull();
        botUser.Bot.Owner.ShouldBeOfType<WorkspaceBotOwner>();
    }

    [Fact]
    public void BotOwner_Workspace_DeserializesAsWorkspaceBotOwner()
    {
        var json = """{"type":"workspace","workspace":true}""";

        var owner = JsonSerializer.Deserialize<BotOwner>(json, JsonOptions);
        var workspaceOwner = owner.ShouldBeOfType<WorkspaceBotOwner>();
        workspaceOwner.Workspace.ShouldBeTrue();
    }

    [Fact]
    public void BotOwner_User_DeserializesAsUserBotOwner()
    {
        var json = """{"type":"user","user":{"object":"user","id":"owner-user-1"}}""";

        var owner = JsonSerializer.Deserialize<BotOwner>(json, JsonOptions);
        owner.ShouldBeOfType<UserBotOwner>();
    }

    [Fact]
    public void SearchResult_Page_DeserializesAsPageSearchResult()
    {
        var json = """{"object":"page","id":"page-1"}""";

        var result = JsonSerializer.Deserialize<SearchResult>(json, JsonOptions);
        var pageResult = result.ShouldBeOfType<PageSearchResult>();
        pageResult.Id.ShouldBe("page-1");
    }

    [Fact]
    public void SearchResult_Database_DeserializesAsDatabaseSearchResult()
    {
        var json = """{"object":"database","id":"db-1"}""";

        var result = JsonSerializer.Deserialize<SearchResult>(json, JsonOptions);
        var dbResult = result.ShouldBeOfType<DatabaseSearchResult>();
        dbResult.Id.ShouldBe("db-1");
    }
}
