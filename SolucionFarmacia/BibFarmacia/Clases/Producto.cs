using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    // Lo que ademas se ALMACENA: existencias y minimo de reposicion. Es el
    // nivel donde el inventario existe, y por eso el que realiza IDespachable.
    public abstract class Producto : ArticuloVendible, IInventariable, IDespachable
    {
        private readonly int existenciasMinimas;

        public int Existencias { get; private set; }
        public int ExistenciasMinimas => existenciasMinimas;

        protected Producto(string nombre, decimal precio,
            int existencias, int existenciasMinimas)
            : base(nombre, precio)
        {
            Existencias = existencias;
            this.existenciasMinimas = existenciasMinimas;
        }

        // Resta sin comprobar existencias ni signo: H-03 conservado (G0).
        // El setter privado le da el monopolio de la escritura: este es el
        // unico sitio donde se arreglara el dia que el comite lo autorice.
        public void Despachar(int cantidad)
        {
            Existencias -= cantidad;
        }
    }
}
