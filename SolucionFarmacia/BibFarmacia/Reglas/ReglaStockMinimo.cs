using BibFarmacia.Avisos;
using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Reglas
{
    // Actor: jefe de compras / reposicion (H-07). El filtro por
    // IInventariable es de capacidad, sin rama alternativa (R-6): lo que no
    // es inventariable no pertenece al dominio de la regla.
    public sealed class ReglaStockMinimo : IReglaDeAlerta
    {
        public IEnumerable<Aviso> Evaluar(
            IEnumerable<ArticuloVendible> catalogo)
        {
            foreach (ArticuloVendible articulo in catalogo)
            {
                if (articulo is IInventariable inventariable &&
                    inventariable.Existencias <=
                    inventariable.ExistenciasMinimas)
                {
                    yield return new Aviso(
                        TipoAviso.StockMinimo,
                        $"ALERTA: stock mínimo de {articulo.Nombre}");
                }
            }
        }
    }
}
