using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    // Lo que ademas se ALMACENA: existencias y minimo de reposicion.
    public abstract class Producto : ArticuloVendible, IInventariable
    {
        public int Existencias { get; private set; }
        public int ExistenciasMinimas { get; }

        protected Producto(string nombre, decimal precio,
            int existencias, int existenciasMinimas)
            : base(nombre, precio)
        {
            Existencias = existencias;
            ExistenciasMinimas = existenciasMinimas;
        }

        // Resta sin comprobar existencias ni signo: H-03 conservado (G0).
        // El setter privado le da el monopolio de la escritura: este es el
        // unico sitio donde se arreglara el dia que el comite lo autorice.
        public override void Despachar(int cantidad)
        {
            Existencias -= cantidad;
        }
    }
}
