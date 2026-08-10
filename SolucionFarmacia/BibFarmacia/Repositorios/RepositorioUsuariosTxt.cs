using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public sealed class RepositorioUsuariosTxt : IRepositorioUsuarios, ICargable
    {
        private readonly List<Usuario> usuarios;
        private readonly LectorDeArchivoDelimitado lector;
        private readonly string ruta;

        public RepositorioUsuariosTxt(string ruta)
        {
            usuarios = new List<Usuario>();
            lector = new LectorDeArchivoDelimitado();
            this.ruta = ruta;
        }

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
                    usuarios.Add(new Usuario(
                        datos[0],
                        datos[1],
                        datos[2],
                        datos[3],
                        datos[4],
                        datos[5]));

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

        public Usuario? BuscarPorNombreDeUsuario(string nombre)
        {
            return usuarios.FirstOrDefault(u =>
                u.NombreDeUsuario == nombre);
        }
    }
}
