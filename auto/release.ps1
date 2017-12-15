$MyDir = [IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Definition)

$releaseDir = "$MyDir\..\release"
if (!(Test-Path $releaseDir)) { $_ = mkdir $releaseDir }
$tmpDir = "$releaseDir\tmp"
if (Test-Path $tmpDir) { del $tmpDir -Recurse -Force }
$_ = mkdir $tmpDir

$pandoc = Get-Command pandoc -ErrorAction SilentlyContinue
if (!$pandoc)
{
    Write-Warning "Pandoc executable is not on the PATH."
    exit
}

& $pandoc "$MyDir\..\README.md" --template "$MyDir\template.html" "--metadata=title:Readme" -o "$tmpDir\Readme.html"
& $pandoc "$MyDir\..\LICENSE.md" --template "$MyDir\template.html" "--metadata=title:License" -o "$tmpDir\License.html"
cp "$MyDir\..\img" "$tmpDir\img" -Recurse

cp "$releaseDir\bin\DisplayMan.exe*" "$tmpDir\"

$_ = [Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem")
[IO.Compression.ZipFile]::CreateFromDirectory($tmpDir, "$releaseDir\Mastersign.DisplayManager_v.zip", "Optimal", $False)

del $tmpDir -Recurse -Force
