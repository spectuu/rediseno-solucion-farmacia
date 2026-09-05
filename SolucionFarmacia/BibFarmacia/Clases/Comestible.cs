namespace BibFarmacia.Clases
{
    // SC-1: hoja perecedera que entra por la intermedia, sin volver a declarar
    // IPerecedero ni FechaVencimiento. Marca se almacena y no se imprime:
    // mostrarla seria salida nueva no autorizada.
    public sealed class Comestible : ProductoPerecedero
    {
        private readonly string marca;

        public string Marca => marca;

        public Comestible(string nombre, decimal precio,
            int existencias, int existenciasMinimas,
            DateTime fechaVencimiento,
            string marca)
            : base(nombre, precio, existencias, existenciasMinimas,
                fechaVencimiento)
        {
            this.marca = marca;
        }
    }
}
