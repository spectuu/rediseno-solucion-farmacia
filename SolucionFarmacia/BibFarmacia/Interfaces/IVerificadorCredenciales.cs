namespace BibFarmacia.Interfaces
{
    // Strategy: la politica de verificacion de credenciales, intercambiable
    // desde el composition root. Recibe dos cadenas, no la lista de usuarios
    // (cierra H-23).
    public interface IVerificadorCredenciales
    {
        bool Verificar(string almacenada, string presentada);
    }
}
