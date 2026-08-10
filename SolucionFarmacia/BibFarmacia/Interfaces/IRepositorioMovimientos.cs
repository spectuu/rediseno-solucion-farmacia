using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    // Solo Registrar: un Todos() sin llamadores seria trasladar el miembro
    // muerto de H-04 a la interfaz (G5). Entra el dia que algo lea la traza.
    public interface IRepositorioMovimientos
    {
        void Registrar(Movimiento movimiento);
    }
}
