using BibFarmacia.Avisos;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    // No contiene ninguna regla: solo las recorre en orden de registro y
    // notifica cada aviso. Su actor es quien decide cuando se revisan.
    public sealed class ServicioAlertas
    {
        private readonly IRepositorioCatalogo catalogo;
        private readonly IEnumerable<IReglaDeAlerta> reglas;
        private readonly INotificador notificador;

        public ServicioAlertas(IRepositorioCatalogo catalogo,
            IEnumerable<IReglaDeAlerta> reglas,
            INotificador notificador)
        {
            this.catalogo = catalogo;
            this.reglas = reglas;
            this.notificador = notificador;
        }

        public void Revisar()
        {
            foreach (IReglaDeAlerta regla in reglas)
            {
                foreach (Aviso aviso in regla.Evaluar(catalogo.Todos()))
                {
                    notificador.Notificar(aviso);
                }
            }
        }
    }
}
