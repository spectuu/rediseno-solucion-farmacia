namespace BibFarmacia.Clases
{
    // SC-2: un servicio se VENDE pero no se ALMACENA. No hereda de Producto
    // y no implementa IInventariable ni IPerecedero: queda fuera del dominio
    // de las dos reglas de alerta por construccion, no por un if que alguien
    // pueda olvidar (vista D2).
    public sealed class Servicio : ArticuloVendible
    {
        public int DuracionMinutos { get; }

        public Servicio(string nombre, decimal precio, int duracionMinutos)
            : base(nombre, precio)
        {
            DuracionMinutos = duracionMinutos;
        }

        // Ficha LSP-2: no hay existencias que reflejar y la postcondicion se
        // satisface con la semantica "despachar un servicio no consume
        // inventario". No es un metodo degenerado: no lanza ni miente.
        public override void Despachar(int cantidad)
        {
        }
    }
}
