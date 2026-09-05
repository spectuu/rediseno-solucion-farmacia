using BibFarmacia.Interfaces;

namespace BibFarmacia.Aspectos
{
    // Strategy, ConcreteStrategy: conserva la politica actual, comparacion en
    // texto plano (G0). Pasar a hash es registrar otra implementacion en el
    // composition root, el dia que se autorice H-05.
    public sealed class VerificadorTextoPlano : IVerificadorCredenciales
    {
        public bool Verificar(string almacenada, string presentada)
        {
            return almacenada == presentada;
        }
    }
}
