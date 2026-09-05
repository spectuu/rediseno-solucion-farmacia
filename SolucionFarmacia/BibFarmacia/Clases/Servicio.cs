namespace BibFarmacia.Clases
{
    // SC-2: un servicio se VENDE pero no se ALMACENA. No hereda de Producto
    // y no implementa IInventariable, IPerecedero ni IDespachable: las reglas
    // de alerta y la venta lo filtran por capacidad, asi que no necesita un
    // Despachar vacio para cumplir un contrato que no le aplica.
    public sealed class Servicio : ArticuloVendible
    {
        private readonly int duracionMinutos;

        public int DuracionMinutos => duracionMinutos;

        public Servicio(string nombre, decimal precio, int duracionMinutos)
            : base(nombre, precio)
        {
            this.duracionMinutos = duracionMinutos;
        }
    }
}
