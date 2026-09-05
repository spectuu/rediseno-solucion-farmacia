using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    // Nivel intermedio que declara una sola vez la capacidad de caducar:
    // IPerecedero y FechaVencimiento suben aqui desde la hoja Medicamento y
    // las hojas de SC-1 las heredan sin repetirlas. Vale mientras la capacidad
    // ortogonal sea una; a la segunda (refrigerado, controlado) se vuelve a
    // interfaces de rol en las hojas. Sin patron: ninguno resuelve una
    // interfaz declarada a la altura equivocada.
    public abstract class ProductoPerecedero : Producto, IPerecedero
    {
        private readonly DateTime fechaVencimiento;

        public DateTime FechaVencimiento => fechaVencimiento;

        protected ProductoPerecedero(string nombre, decimal precio,
            int existencias, int existenciasMinimas,
            DateTime fechaVencimiento)
            : base(nombre, precio, existencias, existenciasMinimas)
        {
            this.fechaVencimiento = fechaVencimiento;
        }
    }
}
