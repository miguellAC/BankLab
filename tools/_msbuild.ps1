# Helper (dot-source): localiza MSBuild.exe de Visual Studio 2019/2022 mediante vswhere.
function Get-MSBuildPath {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $p = & $vswhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
        if ($p) { return $p }
    }
    $cmd = Get-Command msbuild.exe -ErrorAction SilentlyContinue
    if ($cmd) { return $cmd.Source }
    throw "No se encontro MSBuild.exe. Instale Visual Studio con la carga de trabajo '.NET desktop development' (y el Developer Pack de .NET Framework 4.8)."
}
