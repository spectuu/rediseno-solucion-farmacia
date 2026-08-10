namespace BibFarmacia.Clases
{
    // Credencial en vez de Password: el contenido es opaco y quien lo
    // interpreta es el verificador (DIP-5).
    public sealed class Usuario : Persona
    {
        public string NombreDeUsuario { get; }
        public string Credencial { get; }

        public Usuario(string nombre, string cedula,
            string telefono, string correo,
            string nombreDeUsuario, string credencial)
            : base(nombre, cedula, telefono, correo)
        {
            NombreDeUsuario = nombreDeUsuario;
            Credencial = credencial;
        }
    }
}
