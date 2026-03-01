$files = @(
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\Blocks\Block.cs',
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\RichText\RichTextItem.cs',
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\Properties\Values\PropertyValue.cs',
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\Properties\Schema\PropertySchema.cs',
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\NotionObjects.cs',
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\Common\Icon.cs',
  'D:\repos\damianh\notion-sdk-dotnet\src\Notion.Client\Models\Common\Parent.cs'
)
foreach ($f in $files) {
    $content = Get-Content $f -Raw
    # Add [JsonIgnore] before any 'public override string X => "value";' not already having it
    $newContent = [regex]::Replace($content, '(?<!\[JsonIgnore\]\r?\n    )    public override string \w+ => "[^"]+";', {
        param($m)
        '    [JsonIgnore]' + "`r`n" + $m.Value
    })
    if ($newContent -ne $content) {
        Set-Content $f -Value $newContent -NoNewline
        Write-Host "Updated: $f"
    } else {
        Write-Host "No changes: $f"
    }
}
