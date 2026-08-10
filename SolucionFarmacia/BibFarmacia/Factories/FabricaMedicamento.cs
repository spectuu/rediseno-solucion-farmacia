using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    // En que se transforma ProductoFactory (seccion 3.3): sobreviven la idea
    // y el paquete, no el codigo. Columnas del tipo medicamento:
    // nombre;precio;existencias;existenciasMinimas;fechaVencimiento;laboratorio
    public sealed class FabricaMedicamento : IFabricaDeArticulo
    {
        public bool PuedeCrear(string tipoDeArticulo)
        {
            // La cadena vacia es el archivo sin columna de tipo (capa 0),
            // donde toda linea es un medicamento.
            return tipoDeArticulo == "medicamento"
                || tipoDeArticulo.Length == 0;
        }

        public ArticuloVendible Crear(string[] columnas)
        {
            // Laboratorio con datos fijos y toda linea como capsula de gel:
            // H-06 conservado tal cual lo escribe hoy CargarDesdeArchivo (G0).
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
