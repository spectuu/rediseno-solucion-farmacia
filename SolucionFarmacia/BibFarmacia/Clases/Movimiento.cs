using BibFarmacia.Enumeraciones;

namespace BibFarmacia.Clases
{
    // Acepta cualquier ArticuloVendible: ya no exige Producto.
    public sealed class Movimiento
    {
        public DateTime Fecha { get; }
        public TipoMovimiento Tipo { get; }
        public ArticuloVendible Articulo { get; }
        public int Cantidad { get; }

        public Movimiento(DateTime fecha, TipoMovimiento tipo,
            ArticuloVendible articulo, int cantidad)
        {
            Fecha = fecha;
            Tipo = tipo;
            Articulo = articulo;
            Cantidad = cantidad;
        }
    }
}
