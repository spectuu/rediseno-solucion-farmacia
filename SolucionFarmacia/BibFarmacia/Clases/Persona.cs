namespace BibFarmacia.Clases
{
    // Jerarquia conservada: identidad de una persona (H-28 refutado).
    // Los cuatro campos se fijan en construccion y no se mutan (ficha LSP-1).
    public abstract class Persona
    {
        public string Nombre { get; }
        public string Cedula { get; }
        public string Telefono { get; }
        public string Correo { get; }

        protected Persona(string nombre, string cedula,
            string telefono, string correo)
        {
            Nombre = nombre;
            Cedula = cedula;
            Telefono = telefono;
            Correo = correo;
        }
    }
}
