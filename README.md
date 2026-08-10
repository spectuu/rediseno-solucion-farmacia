# 03-src - Código fuente rediseñado (fase 4)

## Roles

| Rol | Quién | Responde por |
|---|---|---|
| Arquitecto de dominio | **José** | Responsabilidades y límites de cada clase (SRP), modelo del dominio, jerarquías y su validez frente a LSP |
| Arquitecto de dependencias | **David** | Mapa de dependencias, abstracciones, inversión e inyección, composition root (DIP, ISP) |
| Ingeniero de comportamiento | **Santiago** | Pruebas de caracterización, evidencia de conducta preservada, escenarios de ejecución |
| Integrador y evidencia | **Santiago** | Consistencia diagrama–código, estructura del entregable, bitácora de IA, métricas antes/después |

## Link del video

https://youtu.be/dks3HAh_PrU

## Estructura

| Carpeta | Contenido |
|---|---|
| `SolucionFarmacia/` | La solución rediseñada (capa 0 + SC-2), fiel al diagrama TO-BE de `02-diseno/uml/`. Dos proyectos, como el original (ADR-06): `BibFarmacia` y `AppFarmaciaConsola` |
| `caracterizacion/` | Los insumos (secuencias de teclas) y el runner de los casos de caracterización. La evidencia de las corridas está en `04-evidencia/caracterizacion/` |


## Compilar y ejecutar

```powershell
cd 03-src\SolucionFarmacia
dotnet build          # 0 errores, 0 advertencias
cd AppFarmaciaConsola
dotnet run
```

Credenciales de prueba (las de `usuarios.txt`, sin cambios): `admin` / `1234`.

## Programa principal de demostración

El programa principal es el mismo menú interactivo del sistema original (la
restricción del cliente congela su conducta). Para recorrer los escenarios
principales sin teclear, usa el script de demo (compila primero):

```powershell
cd caracterizacion
.\run-demo.ps1                                          # demo de SC-2
.\run-demo.ps1 -Insumo .\insumos\01-arranque-login-salir.txt
```

**No** uses `Get-Content insumo | exe`: el pipe de PowerShell corrompe el
stdin y el login falla (verificado contra ambos sistemas). El script usa
redirección de archivo, que es el mecanismo fiable.

## Solicitud de cambio implementada: SC-2 (servicios)

El delta SC-2 vive en un commit propio sobre la capa 0, y ese diff **es** la
métrica de OCP: 2 clases nuevas (`Servicio`, `FabricaServicio`), 0 clases de
negocio modificadas. El análisis completo está en
`04-evidencia/sc2/metrica-sc2.md`; la demo guiada:

```powershell
cd caracterizacion
.\run-demo.ps1
```

## Verificaciones de arquitectura

Las cuatro son un `grep`:

1. Ninguna clase de `Servicios/` ni de `Reglas/` menciona un tipo concreto de `Repositorios/`.
2. Ningún tipo de `Clases/` menciona `System.IO`, `System.Console` ni `DateTime.Now`.
3. `new` de repositorios o del notificador solo existe en `Program.cs` (composition root).
4. Ningún `switch` sobre `TipoAviso` dentro de `BibFarmacia` (regla A-2).
