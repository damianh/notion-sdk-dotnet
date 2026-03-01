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
  if ($content -notmatch 'using Notion\.Client\.Converters;') {
    # Add using after existing using statements
    $newContent = $content -replace '(using System\.Text\.Json\.Serialization;\r?\n)', "`$1using Notion.Client.Converters;`r`n"
    if ($newContent -eq $content) {
      # If no match, add at top
      $newContent = "using Notion.Client.Converters;`r`n" + $content
    }
    Set-Content $f -Value $newContent -NoNewline
    Write-Host "Added using: $f"
  } else {
    Write-Host "Already has using: $f"
  }
}
