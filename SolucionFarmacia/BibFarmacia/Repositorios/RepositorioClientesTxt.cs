using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    // Cliente del Template Method: compone un cargador y le delega la carga.
    // No hereda de nadie, conserva ICargable y la firma de su constructor, asi
    // que el composition root no cambia. Lo suyo es exponer los clientes.
    public sealed class RepositorioClientesTxt : IRepositorioClientes, ICargable
    {
        private readonly CargadorTxt<Cliente> cargador;

        public RepositorioClientesTxt(string ruta)
        {
            cargador = new CargadorClientesTxt(ruta);
        }

        public ResultadoDeCarga Cargar() => cargador.Cargar();

        public IReadOnlyList<Cliente> Todos() => cargador.Elementos;
    }
}
