using BibFarmacia.Avisos;
using BibFarmacia.Interfaces;

namespace AppFarmaciaConsola
{
    // Puerto de salida de INotificador para los cuatro avisos: reemplaza las
    // cuatro lambdas de suscripcion que Program.cs registraba con += (H-08,
    // H-18). No envuelve ninguna interfaz ajena ni traduce firmas: escribe.
    // Es el unico switch legitimo sobre TipoAviso (regla A-2). Sin default a
    // proposito: un TipoAviso nuevo sin color es CS8509 del compilador.
    public sealed class NotificadorConsola : INotificador
    {
        public void Notificar(Aviso aviso)
        {
            // CS8524 avisa por valores sin nombre como (TipoAviso)4, que aqui
            // no pueden ocurrir; se silencia solo ese para que CS8509 (falta
            // un miembro con nombre) siga activo, que es la garantia buscada.
#pragma warning disable CS8524
            Console.ForegroundColor = aviso.Tipo switch
            {
                TipoAviso.StockMinimo => ConsoleColor.Red,
                TipoAviso.Vencimiento => ConsoleColor.Yellow,
                TipoAviso.PuntosAcumulados => ConsoleColor.Green,
                TipoAviso.MovimientoRegistrado => ConsoleColor.Cyan
            };
#pragma warning restore CS8524

            Console.WriteLine(aviso.Mensaje);

            Console.ResetColor();
        }
    }
}
