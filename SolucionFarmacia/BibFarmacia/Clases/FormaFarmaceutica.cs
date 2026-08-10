namespace BibFarmacia.Clases
{
    // La forma es un eje INDEPENDIENTE del tipo de articulo (ADR-05):
    // composicion, no herencia. Cada forma carga solo sus propios datos.
    public abstract record FormaFarmaceutica(string Nombre);
}
