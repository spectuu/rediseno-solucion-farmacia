namespace BibFarmacia.Clases
{
    // La raiz del catalogo: lo que se VENDE, separado de lo que se almacena.
    // Sin comportamiento: despachar es una capacidad (IDespachable) que solo
    // tiene lo que se almacena. Constructor sin validacion: H-14 congelado (G0).
    public abstract class ArticuloVendible
    {
        private readonly string nombre;
        private readonly decimal precio;

        public string Nombre => nombre;
        public decimal Precio => precio;

        protected ArticuloVendible(string nombre, decimal precio)
        {
            this.nombre = nombre;
            this.precio = precio;
        }
    }
}
