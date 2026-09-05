namespace BibFarmacia.Clases
{
    // Se construye una sola vez, en FabricaMedicamento, con direccion y
    // telefono fijos (H-06, G0), y no se muta despues: los tres datos son de
    // solo lectura. Direccion y Telefono se almacenan y no se imprimen.
    public class Laboratorio
    {
        private readonly string nombre;
        private readonly string direccion;
        private readonly string telefono;

        public string Nombre => nombre;
        public string Direccion => direccion;
        public string Telefono => telefono;

        public Laboratorio(string nombre,
            string direccion,
            string telefono)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }
    }
}
