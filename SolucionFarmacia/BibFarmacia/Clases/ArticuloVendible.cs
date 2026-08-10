namespace BibFarmacia.Clases
{
    // La raiz del catalogo (ADR-01): lo que se VENDE, separado de lo que se
    // almacena. Constructor sin validacion: H-14 congelado por el cliente (G0).
    public abstract class ArticuloVendible
    {
        public string Nombre { get; }
        public decimal Precio { get; }

        protected ArticuloVendible(string nombre, decimal precio)
        {
            Nombre = nombre;
            Precio = precio;
        }

        public abstract void Despachar(int cantidad);
    }
}
