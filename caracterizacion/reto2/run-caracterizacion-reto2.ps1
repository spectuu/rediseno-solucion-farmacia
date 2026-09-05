# Caracterizacion Reto 2: corre cada insumo contra el bin AS-IS (fe7dd82) y
# contra el bin TO-BE, compara la salida estandar byte a byte (SHA-256) y deja
# un diff por caso cuando difiere.
#
#   -ProductosTxt <ruta>  sustituye productos.txt en la copia del TO-BE
#                         (modo estricto: mismos datos que el AS-IS).
#
# Ambos bins se copian a una carpeta temporal para no tocar bin\ del proyecto.
# El caso 11 se corre sin productos.txt en las dos copias.

param(
    [Parameter(Mandatory = $true)][string]$BinAsis,
    [Parameter(Mandatory = $true)][string]$BinTobe,
    [Parameter(Mandatory = $true)][string]$Insumos,
    [Parameter(Mandatory = $true)][string]$Salidas,
    [string]$ProductosTxt = ''
)

$ErrorActionPreference = 'Stop'

function Preparar-Directorio([string]$ruta) {
    if (Test-Path $ruta) { Remove-Item -Recurse -Force $ruta }
    New-Item -ItemType Directory -Force $ruta | Out-Null
}

function Copiar-Bin([string]$bin, [string]$destino, [bool]$sinProductos) {
    Preparar-Directorio $destino
    Copy-Item (Join-Path $bin '*') $destino -Recurse
    if ($sinProductos) { Remove-Item (Join-Path $destino 'productos.txt') -Force }
    return $destino
}

function Ejecutar-Caso([string]$bin, [string]$insumo, [string]$salida, [string]$errores) {
    $proceso = Start-Process `
        -FilePath (Join-Path $bin 'AppFarmaciaConsola.exe') `
        -WorkingDirectory $bin `
        -RedirectStandardInput $insumo `
        -RedirectStandardOutput $salida `
        -RedirectStandardError $errores `
        -NoNewWindow -Wait -PassThru
    return $proceso.ExitCode
}

$dirA = Join-Path $Salidas 'as-is'
$dirT = Join-Path $Salidas 'to-be'
$dirD = Join-Path $Salidas 'diff'
Preparar-Directorio $dirA
Preparar-Directorio $dirT
Preparar-Directorio $dirD

$temp = Join-Path $env:TEMP ('carac-reto2-' + [System.Guid]::NewGuid().ToString('N'))
$binA = Copiar-Bin $BinAsis (Join-Path $temp 'asis') $false
$binT = Copiar-Bin $BinTobe (Join-Path $temp 'tobe') $false
if ($ProductosTxt -ne '') {
    Copy-Item $ProductosTxt (Join-Path $binT 'productos.txt') -Force
}
$binA11 = Copiar-Bin $binA (Join-Path $temp 'asis-sin-productos') $true
$binT11 = Copiar-Bin $binT (Join-Path $temp 'tobe-sin-productos') $true

$filas = @()
foreach ($insumo in (Get-ChildItem $Insumos -Filter '*.txt' | Sort-Object Name)) {
    $caso = [System.IO.Path]::GetFileNameWithoutExtension($insumo.Name)
    $bA = $binA; $bT = $binT
    if ($caso.StartsWith('11')) { $bA = $binA11; $bT = $binT11 }

    $sA = Join-Path $dirA ($caso + '.txt')
    $sT = Join-Path $dirT ($caso + '.txt')
    $eA = Join-Path $dirA ($caso + '.stderr.txt')
    $eT = Join-Path $dirT ($caso + '.stderr.txt')

    $cA = Ejecutar-Caso $bA $insumo.FullName $sA $eA
    $cT = Ejecutar-Caso $bT $insumo.FullName $sT $eT

    $hA = (Get-FileHash $sA -Algorithm SHA256).Hash
    $hT = (Get-FileHash $sT -Algorithm SHA256).Hash

    if ($hA -eq $hT) {
        $veredicto = 'IDENTICA'
    } else {
        $veredicto = 'DIFIERE'
        $d = Join-Path $dirD ($caso + '.diff')
        cmd /c ('git -c core.autocrlf=false diff --no-index --text -- "' + $sA + '" "' + $sT + '" 2>nul') | Out-File $d -Encoding utf8
    }
    $filas += [pscustomobject]@{ Caso = $caso; Salida = $veredicto; ExitAsis = $cA; ExitTobe = $cT }
    Write-Host ('{0}  {1}  exit {2}/{3}' -f $caso.PadRight(30), $veredicto, $cA, $cT)
}

if (Test-Path $temp) { Remove-Item -Recurse -Force $temp }

$md = @()
$md += '| Caso | Salida estandar | Exit AS-IS | Exit TO-BE |'
$md += '|---|---|---|---|'
foreach ($f in $filas) { $md += ('| ' + $f.Caso + ' | ' + $f.Salida + ' | ' + $f.ExitAsis + ' | ' + $f.ExitTobe + ' |') }
$md -join "`r`n" | Out-File (Join-Path $Salidas 'resumen.md') -Encoding utf8

$identicas = @($filas | Where-Object { $_.Salida -eq 'IDENTICA' }).Count
Write-Host ('')
Write-Host ('IDENTICAS: {0} / {1}' -f $identicas, $filas.Count)
