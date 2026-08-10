namespace BibFarmacia.Avisos
{
    // Regla A-2: ninguna clase de BibFarmacia puede hacer switch sobre este enum.
    // El unico switch legitimo vive en NotificadorConsola (presentacion).
    public enum TipoAviso
    {
        StockMinimo,
        Vencimiento,
        PuntosAcumulados,
        MovimientoRegistrado
    }
}
