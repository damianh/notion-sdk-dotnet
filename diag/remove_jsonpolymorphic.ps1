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
  # Remove [JsonPolymorphic(...)] lines (including multiline form)
  $newContent = [regex]::Replace($content, '\[JsonPolymorphic\([^\]]*\)\]\r?\n', '')
  if ($newContent -ne $content) {
    Set-Content $f -Value $newContent -NoNewline
    Write-Host "Removed [JsonPolymorphic]: $f"
  } else {
    Write-Host "No [JsonPolymorphic] found: $f"
  }
}
