using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    // Solo en memoria: la traza se pierde al salir, igual que hoy.
    // Persistirla seria conducta nueva (H-04 conservado, G0).
    public sealed class RepositorioMovimientosEnMemoria : IRepositorioMovimientos
    {
        private readonly List<Movimiento> movimientos;

        public RepositorioMovimientosEnMemoria()
        {
            movimientos = new List<Movimiento>();
        }

        public void Registrar(Movimiento movimiento)
        {
            movimientos.Add(movimiento);
        }
    }
}
