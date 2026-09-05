using BibFarmacia.Enumeraciones;

namespace BibFarmacia.Repositorios
{
    // Template Method, AbstractClass: la unica duena del algoritmo de carga
    // TXT que antes vivia tres veces en los repositorios. Cargar es el metodo
    // plantilla, sin virtual; Construir es la unica operacion primitiva y no
    // hay ganchos con cuerpo por defecto. Coleccion, lector y ruta son
    // privados: una subclase solo construye una entidad a partir de una fila.
    internal abstract class CargadorTxt<T>
    {
        private readonly List<T> elementos;
        private readonly LectorDeArchivoDelimitado lector;
        private readonly string ruta;

        protected CargadorTxt(string ruta)
        {
            elementos = new List<T>();
            lector = new LectorDeArchivoDelimitado();
            this.ruta = ruta;
        }

        // Vista de solo lectura sobre la lista viva: es lo que los
        // repositorios devuelven en Todos() y recorren en sus busquedas.
        public IReadOnlyList<T> Elementos => elementos;

        // Metodo plantilla: fija la secuencia completa y ninguna subclase
        // puede redefinirla. Carga acumulativa y parcial ante una linea mal
        // formada: H-27 y la politica de errores de H-02 conservadas (G0, R-1).
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
                    elementos.Add(Construir(columnas));

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

        // Unica operacion primitiva: construye la entidad de una fila. Es el
        // unico paso que variaba entre las tres cargas.
        protected abstract T Construir(string[] columnas);
    }
}
