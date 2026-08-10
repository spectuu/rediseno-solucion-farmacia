using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    public interface IRepositorioUsuarios
    {
        Usuario? BuscarPorNombreDeUsuario(string nombre);
    }
}
