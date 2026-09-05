using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    // Cliente del Template Method: compone un cargador y le delega la carga.
    // No hereda de nadie, conserva ICargable y la firma de su constructor, asi
    // que el composition root no cambia. Lo suyo es buscar usuarios.
    public sealed class RepositorioUsuariosTxt : IRepositorioUsuarios, ICargable
    {
        private readonly CargadorTxt<Usuario> cargador;

        public RepositorioUsuariosTxt(string ruta)
        {
            cargador = new CargadorUsuariosTxt(ruta);
        }

        public ResultadoDeCarga Cargar() => cargador.Cargar();

        public Usuario? BuscarPorNombreDeUsuario(string nombre)
        {
            return cargador.Elementos.FirstOrDefault(u =>
                u.NombreDeUsuario == nombre);
        }
    }
}
