using BibFarmacia.Avisos;
using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    // Punto de extension OCP: una alerta nueva es una implementacion nueva
    // registrada en el composition root, no una edicion de lo existente.
    // Contrato (ficha LSP-3): devuelve secuencia posiblemente vacia, nunca
    // null; no muta el catalogo; preserva el orden de recorrido.
    public interface IReglaDeAlerta
    {
        IEnumerable<Aviso> Evaluar(IEnumerable<ArticuloVendible> catalogo);
    }
}
