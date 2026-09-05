using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    // Implementacion de IFabricaDeArticulo (Factory Method en ambito de
    // objeto) para la clave "medicamento": del ProductoFactory original
    // sobreviven la idea y el paquete, no el codigo. Columnas del tipo:
    // nombre;precio;existencias;existenciasMinimas;fechaVencimiento;laboratorio
    public sealed class FabricaMedicamento : IFabricaDeArticulo
    {
        public string Tipo => "medicamento";

        public ArticuloVendible Crear(string[] columnas)
        {
            // Laboratorio con datos fijos y toda linea como capsula de gel:
            // H-06 conservado tal cual lo escribia CargarDesdeArchivo (G0).
            Laboratorio laboratorio = new Laboratorio(
                columnas[5],
                "Medellin",
                "4444444");

            return new Medicamento(
                columnas[0],
                decimal.Parse(columnas[1]),
                int.Parse(columnas[2]),
                int.Parse(columnas[3]),
                laboratorio,
                new Capsula(TipoRelleno.Gel),
                DateTime.Parse(columnas[4]));
        }
    }
}
