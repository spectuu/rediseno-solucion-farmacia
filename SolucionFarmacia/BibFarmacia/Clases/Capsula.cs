using BibFarmacia.Enumeraciones;

namespace BibFarmacia.Clases
{
    public sealed record Capsula(TipoRelleno Relleno)
        : FormaFarmaceutica("Cápsula");
}
