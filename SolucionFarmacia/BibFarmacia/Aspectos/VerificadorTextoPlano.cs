using BibFarmacia.Interfaces;

namespace BibFarmacia.Aspectos
{
    // Conserva la politica actual: comparacion en texto plano (G0).
    // Pasar a hash es sustituir esta clase en el composition root (DIP-5),
    // el dia que se autorice H-05.
    public sealed class VerificadorTextoPlano : IVerificadorCredenciales
    {
        public bool Verificar(string almacenada, string presentada)
        {
            return almacenada == presentada;
        }
    }
}
