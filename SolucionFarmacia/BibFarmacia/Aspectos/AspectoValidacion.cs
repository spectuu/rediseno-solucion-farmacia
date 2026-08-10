using BibFarmacia.Clases;

namespace BibFarmacia.Aspectos
{
    // Residual R-4: se conserva y SIGUE MUERTA, declarado y no disimulado.
    // Conectarla añadiria validacion, que es una regla nueva (G0, H-14).
    // Solo se ajustan los nombres renombrados del dominio para que compile.
    public static class AspectoValidacion
    {
        public static string ValidarCliente(
            Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(
                cliente.Nombre))
            {
                return "Nombre inválido";
            }

            if (cliente.Cedula.Length < 3)
            {
                return "Cédula inválida";
            }

            return "Cliente válido";
        }

        public static string ValidarProducto(
            Producto producto)
        {
            if (producto.Precio <= 0)
            {
                return "Precio inválido";
            }

            if (producto.Existencias < 0)
            {
                return "Stock inválido";
            }

            return "Producto válido";
        }
    }
}
