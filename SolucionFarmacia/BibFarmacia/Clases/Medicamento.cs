namespace BibFarmacia.Clases
{
    // Hoja perecedera: la capacidad de caducar viene de ProductoPerecedero.
    // La firma del constructor no cambia y fechaVencimiento se reenvia a la
    // base. Forma y Laboratorio se almacenan y no se imprimen (H-06, G0).
    public sealed class Medicamento : ProductoPerecedero
    {
        private readonly Laboratorio laboratorio;
        private readonly FormaFarmaceutica forma;

        public Laboratorio Laboratorio => laboratorio;
        public FormaFarmaceutica Forma => forma;

        public Medicamento(string nombre, decimal precio,
            int existencias, int existenciasMinimas,
            Laboratorio laboratorio,
            FormaFarmaceutica forma,
            DateTime fechaVencimiento)
            : base(nombre, precio, existencias, existenciasMinimas,
                fechaVencimiento)
        {
            this.laboratorio = laboratorio;
            this.forma = forma;
        }
    }
}
