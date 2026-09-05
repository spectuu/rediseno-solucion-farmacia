using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    // Strategy, Context: delega la verificacion de credenciales en la
    // IVerificadorCredenciales que elige el composition root. La politica ya
    // no es una clase estatica (H-05) y se sustituye sin tocar este servicio.
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
