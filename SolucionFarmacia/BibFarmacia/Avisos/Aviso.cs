namespace BibFarmacia.Avisos
{
    // Reglas A-1 a A-4 (plan §8.1): sin comportamiento, sin referencias a
    // presentacion, inmutable y sin herencia (un struct no se puede heredar).
    public readonly record struct Aviso(TipoAviso Tipo, string Mensaje);
}
