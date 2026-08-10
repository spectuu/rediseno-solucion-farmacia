using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    // Duena de la regla de identificacion que Program.cs triplicaba (H-01).
    public sealed class ServicioCatalogo
    {
        private readonly IRepositorioCatalogo repositorio;

        public ServicioCatalogo(IRepositorioCatalogo repositorio)
        {
            this.repositorio = repositorio;
        }

        public IReadOnlyList<ArticuloVendible> Obtener()
        {
            return repositorio.Todos();
        }

        // Coincidencia parcial en minusculas, primera que encaje:
        // la MISMA expresion de hoy, H-01 conservado (G0).
        public ArticuloVendible? BuscarPorNombre(string texto)
        {
            return repositorio.Todos().FirstOrDefault(a =>
                a.Nombre.ToLower()
                .Contains(texto.ToLower()));
        }
    }
}
