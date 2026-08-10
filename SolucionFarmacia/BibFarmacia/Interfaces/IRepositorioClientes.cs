using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    public interface IRepositorioClientes
    {
        IReadOnlyList<Cliente> Todos();
    }
}
