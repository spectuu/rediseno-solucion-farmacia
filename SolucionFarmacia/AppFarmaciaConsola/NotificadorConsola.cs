using BibFarmacia.Avisos;
using BibFarmacia.Interfaces;

namespace AppFarmaciaConsola
{
    // Un adaptador de salida para los cuatro avisos: reemplaza las cuatro
    // lambdas de suscripcion que Program.cs registraba con += (H-08, H-18).
    // Es el unico switch legitimo sobre TipoAviso (regla A-2). Sin default a
    // proposito: un TipoAviso nuevo sin color es CS8509 del compilador (§8.1).
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
