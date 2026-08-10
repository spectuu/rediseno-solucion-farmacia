using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public sealed class RepositorioClientesTxt : IRepositorioClientes, ICargable
    {
        private readonly List<Cliente> clientes;
        private readonly LectorDeArchivoDelimitado lector;
        private readonly string ruta;

        public RepositorioClientesTxt(string ruta)
        {
            clientes = new List<Cliente>();
            lector = new LectorDeArchivoDelimitado();
            this.ruta = ruta;
        }

        // No lee columna de puntos: Puntos arranca en 0 en cada arranque,
        // igual que hoy (H-04 conservado).
        public ResultadoDeCarga Cargar()
        {
            if (!File.Exists(ruta))
            {
                return new ResultadoDeCarga(
                    EstadoCarga.ArchivoNoEncontrado, 0, null);
            }

            int cargados = 0;

            try
            {
                foreach (string[] datos in lector.Leer(ruta, ';'))
                {
                    clientes.Add(new Cliente(
                        datos[0],
                        datos[1],
                        datos[2],
                        datos[3]));

                    cargados++;
                }

                return new ResultadoDeCarga(
                    EstadoCarga.Exitosa, cargados, null);
            }
            catch (Exception ex)
            {
                return new ResultadoDeCarga(
                    EstadoCarga.Fallo, cargados, ex.Message);
            }
        }

        public IReadOnlyList<Cliente> Todos()
        {
            return clientes;
        }
    }
}
