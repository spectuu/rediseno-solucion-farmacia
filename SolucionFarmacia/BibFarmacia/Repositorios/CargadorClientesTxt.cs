using BibFarmacia.Clases;

namespace BibFarmacia.Repositorios
{
    // Template Method, ConcreteClass de clientes: solo aporta la construccion
    // de la entidad. No lee columna de puntos: Puntos arranca en 0 en cada
    // arranque, igual que en el original (H-04 conservado).
    internal sealed class CargadorClientesTxt : CargadorTxt<Cliente>
    {
        public CargadorClientesTxt(string ruta)
            : base(ruta)
        {
        }

        protected override Cliente Construir(string[] columnas)
        {
            return new Cliente(
                columnas[0],
                columnas[1],
                columnas[2],
                columnas[3]);
        }
    }
}
