using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    public interface IRepositorioCatalogo
    {
        IReadOnlyList<ArticuloVendible> Todos();
    }
}
