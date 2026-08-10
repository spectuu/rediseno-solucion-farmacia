using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    // SC-2: la extension entra por el punto que ya existia en capa 0
    // (IFabricaDeArticulo, DIP-9). Columnas del tipo servicio:
    // nombre;precio;duracionMinutos
    public sealed class FabricaServicio : IFabricaDeArticulo
    {
        public bool PuedeCrear(string tipoDeArticulo)
        {
            return tipoDeArticulo == "servicio";
        }

        public ArticuloVendible Crear(string[] columnas)
        {
            return new Servicio(
                columnas[0],
                decimal.Parse(columnas[1]),
                int.Parse(columnas[2]));
        }
    }
}
