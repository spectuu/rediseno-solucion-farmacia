# Runner de caracterizacion (fase 4): ejecuta cada insumo de .\insumos contra
# el sistema ORIGINAL y contra el REDISEÑADO, guarda ambas salidas en
# 04-evidencia/caracterizacion y las compara byte a byte (SHA-256).
#
# Uso:
#   .\run-caracterizacion.ps1 -BinOriginal <bin del original> -BinRediseno <bin del rediseño>
#
# Cada carpeta bin debe contener AppFarmaciaConsola.exe y los tres .txt de
# datos (es la carpeta bin\Debug\net8.0 que produce dotnet build).
#
# El caso 11 (archivo faltante) se ejecuta en una copia temporal del bin sin
# productos.txt, para probar la rama "Archivo no encontrado" en ambos lados.
#
# La salida estandar es el criterio de comparacion. El error estandar se
# guarda pero no se compara: en el caso 10 (crash por entrada no numerica)
# el stack trace incluye rutas de compilacion distintas por construccion.

param(
    [Parameter(Mandatory = $true)][string]$BinOriginal,
    [Parameter(Mandatory = $true)][string]$BinRediseno,
    [string]$CarpetaInsumos = (Join-Path $PSScriptRoot 'insumos'),
    [string]$CarpetaSalidas = (Join-Path $PSScriptRoot '..\..\04-evidencia\caracterizacion')
)

$ErrorActionPreference = 'Stop'

function Preparar-Directorio([string]$ruta) {
    if (Test-Path $ruta) { Remove-Item -Recurse -Force $ruta }
    New-Item -ItemType Directory -Force $ruta | Out-Null
}

function Preparar-BinSinProductos([string]$bin, [string]$destino) {
    Preparar-Directorio $destino
    Copy-Item (Join-Path $bin '*') $destino -Recurse
    Remove-Item (Join-Path $destino 'productos.txt') -Force
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

$dirOriginal = Join-Path $CarpetaSalidas 'original'
$dirRediseno = Join-Path $CarpetaSalidas 'rediseno'
Preparar-Directorio $dirOriginal
Preparar-Directorio $dirRediseno

$temp = Join-Path $env:TEMP ('caracterizacion-' + [System.Guid]::NewGuid().ToString('N'))
$filas = @()

foreach ($insumo in (Get-ChildItem $CarpetaInsumos -Filter '*.txt' | Sort-Object Name)) {
    $caso = [System.IO.Path]::GetFileNameWithoutExtension($insumo.Name)

    $binO = $BinOriginal
    $binR = $BinRediseno

    if ($caso.StartsWith('11')) {
        $binO = Preparar-BinSinProductos $BinOriginal (Join-Path $temp 'original-sin-productos')
        $binR = Preparar-BinSinProductos $BinRediseno (Join-Path $temp 'rediseno-sin-productos')
    }

    $salidaO = Join-Path $dirOriginal ($caso + '.txt')
    $salidaR = Join-Path $dirRediseno ($caso + '.txt')
    $erroresO = Join-Path $dirOriginal ($caso + '.stderr.txt')
    $erroresR = Join-Path $dirRediseno ($caso + '.stderr.txt')

    $codigoO = Ejecutar-Caso $binO $insumo.FullName $salidaO $erroresO
    $codigoR = Ejecutar-Caso $binR $insumo.FullName $salidaR $erroresR

    $hashO = (Get-FileHash $salidaO -Algorithm SHA256).Hash
    $hashR = (Get-FileHash $salidaR -Algorithm SHA256).Hash

    if ($hashO -eq $hashR) { $veredicto = 'IDENTICA' } else { $veredicto = 'DIFIERE' }

    $filas += [pscustomobject]@{
        Caso           = $caso
        SalidaEstandar = $veredicto
        ExitOriginal   = $codigoO
        ExitRediseno   = $codigoR
    }

    Write-Host ('{0}  {1}' -f $caso.PadRight(30), $veredicto)
}

if (Test-Path $temp) { Remove-Item -Recurse -Force $temp }

$filas | Format-Table -AutoSize

$md = @()
$md += '# Comparacion de salidas: sistema original vs rediseño (capa 0)'
$md += ''
$md += ('Ejecutado: ' + (Get-Date -Format 'yyyy-MM-dd HH:mm') + '. Comparacion byte a byte (SHA-256) de la salida estandar.')
$md += ''
$md += '| Caso | Salida estandar | Exit original | Exit rediseño |'
$md += '|---|---|---|---|'
foreach ($fila in $filas) {
    $md += ('| ' + $fila.Caso + ' | ' + $fila.SalidaEstandar + ' | ' + $fila.ExitOriginal + ' | ' + $fila.ExitRediseno + ' |')
}
$md -join "`r`n" | Out-File (Join-Path $CarpetaSalidas 'resumen-comparacion.md') -Encoding utf8

Write-Host ''
Write-Host ('Salidas guardadas en: ' + $CarpetaSalidas)
