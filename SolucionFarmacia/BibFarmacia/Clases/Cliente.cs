namespace BibFarmacia.Clases
{
    public sealed class Cliente : Persona
    {
        public int Puntos { get; private set; }

        public Cliente(string nombre, string cedula,
            string telefono, string correo)
            : base(nombre, cedula, telefono, correo)
        {
            Puntos = 0;
        }

        // Unica ruta de escritura del saldo (H-17). Sin validar signo: G0.
        public void AcumularPuntos(int puntos)
        {
            Puntos += puntos;
        }
    }
}
