using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    public sealed class Medicamento : Producto, IPerecedero
    {
        public Laboratorio Laboratorio { get; }
        public FormaFarmaceutica Forma { get; }
        public DateTime FechaVencimiento { get; }

        public Medicamento(string nombre, decimal precio,
            int existencias, int existenciasMinimas,
            Laboratorio laboratorio,
            FormaFarmaceutica forma,
            DateTime fechaVencimiento)
            : base(nombre, precio, existencias, existenciasMinimas)
        {
            Laboratorio = laboratorio;
            Forma = forma;
            FechaVencimiento = fechaVencimiento;
        }
    }
}
