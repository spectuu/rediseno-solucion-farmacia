namespace BibFarmacia.Interfaces
{
    // Interfaz de rol (ISP): la regla de reposicion ve solo esto.
    public interface IInventariable
    {
        int Existencias { get; }
        int ExistenciasMinimas { get; }
    }
}
