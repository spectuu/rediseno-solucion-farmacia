using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    // SC-1: implementacion de IFabricaDeArticulo (Factory Method en ambito de
    // objeto) para la clave "comestible". Columnas del tipo, espejo de
    // medicamento con marca en lugar de laboratorio:
    // nombre;precio;existencias;existenciasMinimas;fechaVencimiento;marca
    public sealed class FabricaComestible : IFabricaDeArticulo
    {
        public string Tipo => "comestible";

        public ArticuloVendible Crear(string[] columnas)
        {
            return new Comestible(
                columnas[0],
                decimal.Parse(columnas[1]),
                int.Parse(columnas[2]),
                int.Parse(columnas[3]),
                DateTime.Parse(columnas[4]),
                columnas[5]);
        }
    }
}
