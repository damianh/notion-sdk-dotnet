// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using DamianH.NotionCli.Commands;

namespace DamianH.NotionCli;

public sealed class CommandParsingTests
{
    private static RootCommand BuildRootCommand(out Option<string?> tokenOption, out Option<bool> verboseOption, out Option<bool> noIndentOption)
    {
        var root = new RootCommand("Notion API CLI");

        tokenOption = new Option<string?>("--token") { Description = "Notion API bearer token." };
        tokenOption.Recursive = true;
        root.Options.Add(tokenOption);

        verboseOption = new Option<bool>("--verbose") { Description = "Verbose output." };
        verboseOption.Recursive = true;
        root.Options.Add(verboseOption);

        noIndentOption = new Option<bool>("--no-indent") { Description = "Compact JSON." };
        noIndentOption.Recursive = true;
        root.Options.Add(noIndentOption);

        root.Subcommands.Add(BlocksCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(DatabasesCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(PagesCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(UsersCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(CommentsCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(SearchCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(FileUploadsCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(DataSourcesCommands.Build(tokenOption, verboseOption, noIndentOption));
        root.Subcommands.Add(OAuthCommands.Build(noIndentOption));

        return root;
    }

    [Fact]
    public void RootCommand_HasAllNineCommandGroups()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var names = root.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("blocks");
        names.ShouldContain("databases");
        names.ShouldContain("pages");
        names.ShouldContain("users");
        names.ShouldContain("comments");
        names.ShouldContain("search");
        names.ShouldContain("file-uploads");
        names.ShouldContain("data-sources");
        names.ShouldContain("oauth");
    }

    [Fact]
    public void BlocksCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var blocks = root.Subcommands.Single(c => c.Name == "blocks");
        var names = blocks.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("get");
        names.ShouldContain("update");
        names.ShouldContain("delete");
        names.ShouldContain("list-children");
        names.ShouldContain("append-children");
    }

    [Fact]
    public void DatabasesCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var databases = root.Subcommands.Single(c => c.Name == "databases");
        var names = databases.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("get");
        names.ShouldContain("create");
        names.ShouldContain("update");
        names.ShouldContain("query");
    }

    [Fact]
    public void PagesCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var pages = root.Subcommands.Single(c => c.Name == "pages");
        var names = pages.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("get");
        names.ShouldContain("create");
        names.ShouldContain("update");
        names.ShouldContain("get-property");
    }

    [Fact]
    public void UsersCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var users = root.Subcommands.Single(c => c.Name == "users");
        var names = users.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("get");
        names.ShouldContain("get-self");
        names.ShouldContain("list");
    }

    [Fact]
    public void CommentsCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var comments = root.Subcommands.Single(c => c.Name == "comments");
        var names = comments.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("create");
        names.ShouldContain("list");
    }

    [Fact]
    public void FileUploadsCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var fileUploads = root.Subcommands.Single(c => c.Name == "file-uploads");
        var names = fileUploads.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("create");
        names.ShouldContain("get");
        names.ShouldContain("send-part");
        names.ShouldContain("complete");
        names.ShouldContain("upload");
    }

    [Fact]
    public void DataSourcesCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var dataSources = root.Subcommands.Single(c => c.Name == "data-sources");
        var names = dataSources.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("get");
        names.ShouldContain("create");
        names.ShouldContain("update");
        names.ShouldContain("query");
    }

    [Fact]
    public void OAuthCommand_HasExpectedSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var oauth = root.Subcommands.Single(c => c.Name == "oauth");
        var names = oauth.Subcommands.Select(c => c.Name).ToHashSet();
        names.ShouldContain("exchange-token");
        names.ShouldContain("revoke");
        names.ShouldContain("introspect");
    }

    [Fact]
    public void BlocksGetCommand_ParsesBlockIdArgument()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var parseResult = root.Parse(["blocks", "get", "some-block-id"]);
        parseResult.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void SearchCommand_ParsesQueryOption()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var parseResult = root.Parse(["search", "--query", "Meeting notes"]);
        parseResult.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void GlobalTokenOption_IsAvailableToSubcommands()
    {
        var root = BuildRootCommand(out var tokenOption, out _, out _);
        var parseResult = root.Parse(["--token", "secret", "users", "get-self"]);
        parseResult.Errors.ShouldBeEmpty();
        parseResult.GetValue(tokenOption).ShouldBe("secret");
    }

    [Fact]
    public void GlobalNoIndentOption_IsAvailableToSubcommands()
    {
        var root = BuildRootCommand(out _, out _, out var noIndentOption);
        var parseResult = root.Parse(["--no-indent", "users", "get-self"]);
        parseResult.Errors.ShouldBeEmpty();
        parseResult.GetValue(noIndentOption).ShouldBeTrue();
    }

    [Fact]
    public void OAuthExchangeTokenCommand_ParsesAllRequiredOptions()
    {
        var root = BuildRootCommand(out _, out _, out _);
        var parseResult = root.Parse([
            "oauth", "exchange-token",
            "--client-id", "my-client",
            "--client-secret", "my-secret",
            "--code", "auth-code",
        ]);
        parseResult.Errors.ShouldBeEmpty();
    }
}
