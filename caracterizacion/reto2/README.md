# Caracterización del Reto 2: AS-IS (fe7dd82) frente al TO-BE

Compara la salida estándar del código AS-IS, el commit `fe7dd82` del Reto 1 con SC-2,
contra el TO-BE del Reto 2, byte a byte (SHA-256), con los mismos insumos.

## Insumos

Los 11 casos del Reto 1 (`../insumos/`) se capturaron contra la capa 0, donde el menú
tenía siete opciones y `7` era «Salir». Desde SC-2 el menú tiene ocho y cada tecla
cambió de sitio, así que `insumos/` trae los mismos 11 guiones con las teclas
traducidas, más dos casos de SC-2 (12 y 13).

| Opción | Tecla capa 0 | Tecla desde SC-2 |
|---|---|---|
| Ver productos | 1 | 1 |
| Ver servicios | no existía | 2 |
| Ver clientes | 2 | 3 |
| Buscar producto | 3 | 4 |
| Registrar venta | 4 | 5 |
| Acumular puntos | 5 | 6 |
| Ver alertas | 6 | 7 |
| Salir | 7 | 8 |

## Cómo se corrió

```powershell
# bin del AS-IS: compilar fe7dd82 aparte (git worktree) y copiar bin\Debug\net8.0
# bin del TO-BE: dotnet build de este repositorio

# corrida estricta: el TO-BE con el productos.txt del AS-IS (13 filas)
.\run-caracterizacion-reto2.ps1 -BinAsis <bin fe7dd82> -BinTobe <bin TO-BE> `
    -Insumos .\insumos -Salidas .\salidas\estricto -ProductosTxt <bin fe7dd82>\productos.txt

# corrida SC-1: el TO-BE con su productos.txt (19 filas)
.\run-caracterizacion-reto2.ps1 -BinAsis <bin fe7dd82> -BinTobe <bin TO-BE> `
    -Insumos .\insumos -Salidas .\salidas\sc1
```

## Resultado (4 de septiembre de 2026)

- **Estricta:** 13 de 13 salidas idénticas y mismos códigos de salida. Se repitió tras
  cada uno de los seis bloques de implementación con el mismo resultado.
- **SC-1:** 2 idénticas (login fallido y archivo faltante) y 11 que difieren solo por
  líneas añadidas: las seis filas nuevas en «Ver productos» y dos alertas
  (`stock mínimo de CremaHidratante`, `Helado próximo a vencer`). Los diffs están en
  `salidas/sc1/diff/`; no hay ninguna línea eliminada ni modificada.

El error estándar se guarda y no se compara: en el caso 10 el stack trace trae rutas de
compilación.
