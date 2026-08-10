using BibFarmacia.Avisos;
using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    // Conserva nombre y responsabilidad: consulta y fidelizacion son el
    // mismo actor (la asimetria con el catalogo es deliberada, plan §9.1).
    public sealed class ServicioCliente
    {
        private readonly IRepositorioClientes repositorio;
        private readonly INotificador notificador;

        public ServicioCliente(IRepositorioClientes repositorio,
            INotificador notificador)
        {
            this.repositorio = repositorio;
            this.notificador = notificador;
        }

        public IReadOnlyList<Cliente> Obtener()
        {
            return repositorio.Todos();
        }

        // Misma busqueda parcial que el catalogo: H-01 conservado (G0).
        public Cliente? BuscarPorNombre(string texto)
        {
            return repositorio.Todos().FirstOrDefault(c =>
                c.Nombre.ToLower()
                .Contains(texto.ToLower()));
        }

        // Delega en la entidad (cierra H-17). Sin validar signo: G0.
        public void AcumularPuntos(Cliente cliente, int puntos)
        {
            cliente.AcumularPuntos(puntos);

            notificador.Notificar(new Aviso(
                TipoAviso.PuntosAcumulados,
                $"Cliente {cliente.Nombre} acumuló {puntos} puntos"));
        }
    }
}
