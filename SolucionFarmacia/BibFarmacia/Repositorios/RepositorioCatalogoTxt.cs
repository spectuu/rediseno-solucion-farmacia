using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    // El repositorio conoce el archivo (ruta, separador, que columna trae el
    // tipo); la fabrica conoce el orden de las columnas de su propio tipo.
    // columnaDeTipo: -1 cuando el archivo no trae esa columna (capa 0).
    public sealed class RepositorioCatalogoTxt : IRepositorioCatalogo, ICargable
    {
        private readonly List<ArticuloVendible> articulos;
        private readonly LectorDeArchivoDelimitado lector;
        private readonly string ruta;
        private readonly IEnumerable<IFabricaDeArticulo> fabricas;
        private readonly int columnaDeTipo;

        public RepositorioCatalogoTxt(string ruta,
            IEnumerable<IFabricaDeArticulo> fabricas,
            int columnaDeTipo)
        {
            articulos = new List<ArticuloVendible>();
            lector = new LectorDeArchivoDelimitado();
            this.ruta = ruta;
            this.fabricas = fabricas;
            this.columnaDeTipo = columnaDeTipo;
        }

        // Carga acumulativa y parcial ante una linea mal formada:
        // H-27 y la politica de errores de H-02 conservadas (G0, R-1).
        public ResultadoDeCarga Cargar()
        {
            if (!File.Exists(ruta))
            {
                return new ResultadoDeCarga(
                    EstadoCarga.ArchivoNoEncontrado, 0, null);
            }

            int cargados = 0;

            try
            {
                foreach (string[] columnas in lector.Leer(ruta, ';'))
                {
                    string tipoDeArticulo =
                        columnaDeTipo >= 0
                            ? columnas[columnaDeTipo]
                            : string.Empty;

                    string[] datos =
                        columnaDeTipo >= 0
                            ? QuitarColumna(columnas, columnaDeTipo)
                            : columnas;

                    IFabricaDeArticulo fabrica =
                        fabricas.First(f => f.PuedeCrear(tipoDeArticulo));

                    articulos.Add(fabrica.Crear(datos));

                    cargados++;
                }

                return new ResultadoDeCarga(
                    EstadoCarga.Exitosa, cargados, null);
            }
            catch (Exception ex)
            {
                return new ResultadoDeCarga(
                    EstadoCarga.Fallo, cargados, ex.Message);
            }
        }

        public IReadOnlyList<ArticuloVendible> Todos()
        {
            return articulos;
        }

        private static string[] QuitarColumna(string[] columnas, int indice)
        {
            string[] resultado = new string[columnas.Length - 1];
            int destino = 0;

            for (int origen = 0; origen < columnas.Length; origen++)
            {
                if (origen != indice)
                {
                    resultado[destino] = columnas[origen];
                    destino++;
                }
            }

            return resultado;
        }
    }
}
