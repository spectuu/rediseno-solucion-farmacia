using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    // Punto de extension OCP (ADR-04): cada fabrica se autoselecciona con
    // PuedeCrear y el registro vive en el composition root. Crear recibe un
    // registro posicional de campos ya sin la columna de tipo: la fabrica
    // conoce el orden de las columnas de su propio tipo, el repositorio
    // conoce el archivo.
    public interface IFabricaDeArticulo
    {
        bool PuedeCrear(string tipoDeArticulo);
        ArticuloVendible Crear(string[] columnas);
    }
}
