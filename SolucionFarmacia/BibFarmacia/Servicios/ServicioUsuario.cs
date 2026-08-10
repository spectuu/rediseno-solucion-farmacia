using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    // La politica de credenciales ya no es una clase estatica: es una
    // dependencia sustituible (H-05, DIP-5).
    public sealed class ServicioUsuario
    {
        private readonly IRepositorioUsuarios repositorio;
        private readonly IVerificadorCredenciales verificador;

        public ServicioUsuario(IRepositorioUsuarios repositorio,
            IVerificadorCredenciales verificador)
        {
            this.repositorio = repositorio;
            this.verificador = verificador;
        }

        public bool Login(string usuario, string credencial)
        {
            Usuario? encontrado =
                repositorio.BuscarPorNombreDeUsuario(usuario);

            return encontrado != null &&
                verificador.Verificar(
                    encontrado.Credencial,
                    credencial);
        }
    }
}
