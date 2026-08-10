# Ejecuta un insumo contra el rediseño y muestra la salida.
# Por defecto corre la demo de SC-2. Usa el mismo mecanismo probado del runner
# (redireccion de archivo con Start-Process): NO usar `Get-Content | exe`,
# porque el pipe de PowerShell corrompe el stdin y el login falla.
#
# Uso:
#   .\run-demo.ps1                        # demo de SC-2
#   .\run-demo.ps1 -Insumo <otro .txt>    # cualquier otro guion

param(
    [string]$Insumo = (Join-Path $PSScriptRoot 'sc2\demo-servicios.txt'),
    [string]$Bin = (Join-Path $PSScriptRoot '..\SolucionFarmacia\AppFarmaciaConsola\bin\Debug\net8.0')
)

$ErrorActionPreference = 'Stop'

$salida = Join-Path $env:TEMP 'demo-salida.txt'
$errores = Join-Path $env:TEMP 'demo-errores.txt'

Start-Process `
    -FilePath (Join-Path $Bin 'AppFarmaciaConsola.exe') `
    -WorkingDirectory $Bin `
    -RedirectStandardInput $Insumo `
    -RedirectStandardOutput $salida `
    -RedirectStandardError $errores `
    -NoNewWindow -Wait

Get-Content $salida -Encoding UTF8
