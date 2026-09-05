using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    // Cliente del Template Method: compone un cargador y le delega la carga.
    // No hereda de nadie, conserva ICargable y la firma de su constructor, asi
    // que el composition root no cambia. Lo suyo es exponer el catalogo.
    public sealed class RepositorioCatalogoTxt : IRepositorioCatalogo, ICargable
    {
        private readonly CargadorTxt<ArticuloVendible> cargador;

        public RepositorioCatalogoTxt(string ruta,
            IEnumerable<IFabricaDeArticulo> fabricas,
            int columnaDeTipo)
        {
            cargador = new CargadorCatalogoTxt(ruta, fabricas, columnaDeTipo);
        }

        public ResultadoDeCarga Cargar() => cargador.Cargar();

        public IReadOnlyList<ArticuloVendible> Todos() => cargador.Elementos;
    }
}
