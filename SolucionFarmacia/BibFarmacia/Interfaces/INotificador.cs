using BibFarmacia.Avisos;

namespace BibFarmacia.Interfaces
{
    // Un contrato para los cuatro avisos: sustituye a las cuatro clases de
    // evento y sus cuatro delegados (H-08, H-18).
    public interface INotificador
    {
        void Notificar(Aviso aviso);
    }
}
