using BibFarmacia.Repositorios;

namespace BibFarmacia.Interfaces
{
    // Interfaz de rol (ISP): la consume Program una sola vez al arrancar.
    // Ningun servicio la ve, asi que ninguno puede recargar a mitad de
    // sesion (cierra de rebote H-27).
    public interface ICargable
    {
        ResultadoDeCarga Cargar();
    }
}
