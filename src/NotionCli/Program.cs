// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using DamianH.NotionCli.Commands;

var rootCommand = new RootCommand("Notion API CLI — access all Notion endpoints from the command line.");

var tokenOption = new Option<string?>("--token") { Description = "Notion API bearer token (overrides NOTION_TOKEN env var and config file)." };
tokenOption.Recursive = true;
rootCommand.Options.Add(tokenOption);

var verboseOption = new Option<bool>("--verbose") { Description = "Show diagnostic output on stderr." };
verboseOption.Recursive = true;
rootCommand.Options.Add(verboseOption);

var noIndentOption = new Option<bool>("--no-indent") { Description = "Output compact (non-indented) JSON." };
noIndentOption.Recursive = true;
rootCommand.Options.Add(noIndentOption);

rootCommand.Subcommands.Add(BlocksCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(DatabasesCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(PagesCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(UsersCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(CommentsCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(SearchCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(FileUploadsCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(DataSourcesCommands.Build(tokenOption, verboseOption, noIndentOption));
rootCommand.Subcommands.Add(OAuthCommands.Build(noIndentOption));

return await rootCommand.Parse(args).InvokeAsync();
