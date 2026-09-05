using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    // Factory Method, variante de ambito de objeto: interfaz de creacion con
    // implementaciones registradas en el composition root y elegidas por
    // clave. Crear es el metodo de fabricacion y Tipo la clave de seleccion.
    // La instanciacion se difiere por composicion y registro, no por herencia
    // sobre el Creator, porque el repositorio varia con la fuente de datos y
    // no con el tipo de articulo. Crear recibe la fila ya sin la columna de tipo.
    public interface IFabricaDeArticulo
    {
        string Tipo { get; }
        ArticuloVendible Crear(string[] columnas);
    }
}
