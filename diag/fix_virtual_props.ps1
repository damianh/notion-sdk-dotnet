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
    # Replace [JsonPropertyName("xxx")]\n    public virtual string X => string.Empty;
    # with [JsonIgnore]\n    public virtual string X => string.Empty;
    $newContent = [regex]::Replace($content, '\[JsonPropertyName\("[^"]+"\)\]\r?\n    public virtual string \w+ => string\.Empty;', {
        param($m)
        $line = $m.Value
        # Replace [JsonPropertyName(...)] with [JsonIgnore]
        $line = [regex]::Replace($line, '\[JsonPropertyName\("[^"]+"\)\]', '[JsonIgnore]')
        $line
    })
    if ($newContent -ne $content) {
        Set-Content $f -Value $newContent -NoNewline
        Write-Host "Updated virtual props: $f"
    } else {
        Write-Host "No virtual prop changes: $f"
    }
}
