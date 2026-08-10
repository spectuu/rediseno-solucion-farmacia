using BibFarmacia.Avisos;
using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    // La venta sale del switch del menu y gana dueno (H-03). Lo que NO hace,
    // por G0: no comprueba existencias ni signo, no reevalua alertas tras
    // vender, no pregunta el cliente ni aplica descuento (H-22, llega con SC-3).
    public sealed class ServicioVenta
    {
        private readonly IRepositorioMovimientos movimientos;
        private readonly TimeProvider reloj;
        private readonly INotificador notificador;

        public ServicioVenta(IRepositorioMovimientos movimientos,
            TimeProvider reloj,
            INotificador notificador)
        {
            this.movimientos = movimientos;
            this.reloj = reloj;
            this.notificador = notificador;
        }

        public Movimiento RegistrarVenta(ArticuloVendible articulo, int cantidad)
        {
            articulo.Despachar(cantidad);

            Movimiento movimiento = new Movimiento(
                reloj.GetLocalNow().DateTime,
                TipoMovimiento.Venta,
                articulo,
                cantidad);

            movimientos.Registrar(movimiento);

            notificador.Notificar(new Aviso(
                TipoAviso.MovimientoRegistrado,
                $"Movimiento registrado: {movimiento.Tipo}"));

            return movimiento;
        }
    }
}
