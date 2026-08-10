namespace BibFarmacia.Repositorios
{
    // Lector unico para las tres cargas TXT: es DRY (H-19), no un principio.
    // Conserva la semantica de File.ReadAllLines + Split del original.
    internal sealed class LectorDeArchivoDelimitado
    {
        public IEnumerable<string[]> Leer(string ruta, char separador)
        {
            string[] lineas = File.ReadAllLines(ruta);

            foreach (string linea in lineas)
            {
                yield return linea.Split(separador);
            }
        }
    }
}
