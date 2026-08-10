using BibFarmacia.Enumeraciones;

namespace BibFarmacia.Clases
{
    public sealed record Liquido(MaterialEnvase Envase, int Mililitros)
        : FormaFarmaceutica("Líquido");
}
