using BibFarmacia.Avisos;
using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Reglas
{
    // Actor: direccion tecnica farmaceutica (H-07). El reloj es inyectado
    // (H-10, DIP-6) y se lee como hora local, que es lo que DateTime.Now
    // devolvia: GetUtcNow() moveria el conteo de dias en UTC-5.
    public sealed class ReglaVencimiento : IReglaDeAlerta
    {
        private readonly TimeProvider reloj;
        private readonly int diasDePreaviso;

        public ReglaVencimiento(TimeProvider reloj, int diasDePreaviso)
        {
            this.reloj = reloj;
            this.diasDePreaviso = diasDePreaviso;
        }

        public IEnumerable<Aviso> Evaluar(
            IEnumerable<ArticuloVendible> catalogo)
        {
            foreach (ArticuloVendible articulo in catalogo)
            {
                if (articulo is IPerecedero perecedero)
                {
                    int dias =
                        (perecedero.FechaVencimiento -
                        reloj.GetLocalNow().DateTime).Days;

                    // Sin cota inferior: H-13 conservado (G0). Un producto
                    // ya vencido sigue saliendo como "próximo a vencer".
                    if (dias <= diasDePreaviso)
                    {
                        yield return new Aviso(
                            TipoAviso.Vencimiento,
                            $"ALERTA: {articulo.Nombre} próximo a vencer");
                    }
                }
            }
        }
    }
}
