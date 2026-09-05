using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    // Template Method, ConcreteClass del catalogo. Es tambien el cliente del
    // Factory Method: conoce el archivo (que columna trae el tipo) y difiere
    // la instanciacion a la fabrica cuya clave coincide; la fabrica conoce el
    // orden de las columnas de su propio tipo. columnaDeTipo es un indice
    // valido del archivo: la rama -1 de capa 0 salio con SC-2.
    internal sealed class CargadorCatalogoTxt : CargadorTxt<ArticuloVendible>
    {
        private readonly IEnumerable<IFabricaDeArticulo> fabricas;
        private readonly int columnaDeTipo;

        public CargadorCatalogoTxt(string ruta,
            IEnumerable<IFabricaDeArticulo> fabricas,
            int columnaDeTipo)
            : base(ruta)
        {
            this.fabricas = fabricas;
            this.columnaDeTipo = columnaDeTipo;
        }

        protected override ArticuloVendible Construir(string[] columnas)
        {
            string tipoDeArticulo = columnas[columnaDeTipo];

            string[] datos = QuitarColumna(columnas, columnaDeTipo);

            // Seleccion de la fabrica por clave. Se conserva
            // Enumerable.First: una fila con tipo sin fabrica sigue
            // produciendo la misma InvalidOperationException (G0).
            IFabricaDeArticulo fabrica =
                fabricas.First(f => f.Tipo == tipoDeArticulo);

            return fabrica.Crear(datos);
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
