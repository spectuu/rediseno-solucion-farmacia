namespace BibFarmacia.Interfaces
{
    // Interfaz de rol (ISP): la regla de vencimiento ve solo esto.
    public interface IPerecedero
    {
        DateTime FechaVencimiento { get; }
    }
}
