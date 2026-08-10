namespace BibFarmacia.Interfaces
{
    // Recibe dos cadenas, no la lista de usuarios (cierra H-23).
    public interface IVerificadorCredenciales
    {
        bool Verificar(string almacenada, string presentada);
    }
}
