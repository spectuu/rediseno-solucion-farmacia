using BibFarmacia.Clases;

namespace BibFarmacia.Repositorios
{
    // Template Method, ConcreteClass de usuarios: solo aporta la construccion
    // de la entidad, con los mismos indices de columna del original.
    internal sealed class CargadorUsuariosTxt : CargadorTxt<Usuario>
    {
        public CargadorUsuariosTxt(string ruta)
            : base(ruta)
        {
        }

        protected override Usuario Construir(string[] columnas)
        {
            return new Usuario(
                columnas[0],
                columnas[1],
                columnas[2],
                columnas[3],
                columnas[4],
                columnas[5]);
        }
    }
}
