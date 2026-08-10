using BibFarmacia.Enumeraciones;

namespace BibFarmacia.Repositorios
{
    // Objeto de valor que reemplaza el string de exito/error de las cargas
    // (H-02): quien redacta el mensaje pasa a ser la interfaz de usuario.
    public sealed class ResultadoDeCarga
    {
        public EstadoCarga Estado { get; }
        public int Cargados { get; }
        public string? Detalle { get; }

        public ResultadoDeCarga(EstadoCarga estado, int cargados, string? detalle)
        {
            Estado = estado;
            Cargados = cargados;
            Detalle = detalle;
        }
    }
}
