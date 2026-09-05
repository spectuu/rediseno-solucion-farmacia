using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    // SC-2: implementacion de IFabricaDeArticulo (Factory Method en ambito de
    // objeto) para la clave "servicio". Entro por el punto de extension que ya
    // existia, sin tocar a quien elige la fabrica. Columnas del tipo:
    // nombre;precio;duracionMinutos
    public sealed class FabricaServicio : IFabricaDeArticulo
    {
        public string Tipo => "servicio";

        public ArticuloVendible Crear(string[] columnas)
        {
            return new Servicio(
                columnas[0],
                decimal.Parse(columnas[1]),
                int.Parse(columnas[2]));
        }
    }
}
