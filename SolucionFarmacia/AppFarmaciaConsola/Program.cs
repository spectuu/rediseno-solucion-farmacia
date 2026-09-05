using AppFarmaciaConsola;
using BibFarmacia.Aspectos;
using BibFarmacia.Clases;
using BibFarmacia.Enumeraciones;
using BibFarmacia.Factories;
using BibFarmacia.Interfaces;
using BibFarmacia.Reglas;
using BibFarmacia.Repositorios;
using BibFarmacia.Servicios;

Console.Title = "Sistema Farmacia";

// ========== COMPOSITION ROOT (§12): unico bloque con new de dependencias ==========

var reloj = TimeProvider.System;              // DIP-6: se lee GetLocalNow()
var notificador = new NotificadorConsola();   // DIP-7: los colores viven dentro

// Que tipos de articulo sabe leer el catalogo (DIP-9).
// En capa 0 hay UNO. SC-1 y SC-2 se resuelven añadiendo lineas AQUI.
var fabricas = new IFabricaDeArticulo[]
{
    new FabricaMedicamento(),
    new FabricaCosmetico(),        // SC-1: la linea de registro
    new FabricaComestible(),       // SC-1: la linea de registro
    new FabricaServicio()          // SC-2: la linea de registro (vista D2)
};

// Repositorios (DIP-1 a DIP-4). Sin escritura a disco: H-04 se conserva.
// SC-2: productos.txt gana la columna de tipo en la posicion 0 (era -1).
var repositorioCatalogo =
    new RepositorioCatalogoTxt(
        "productos.txt",
        fabricas,
        columnaDeTipo: 0);

var repositorioClientes =
    new RepositorioClientesTxt("clientes.txt");

var repositorioUsuarios =
    new RepositorioUsuariosTxt("usuarios.txt");

var repositorioMovimientos =
    new RepositorioMovimientosEnMemoria();

// Politica de acceso (DIP-5): la unica linea a cambiar el dia que se
// autorice H-05 (-> new VerificadorPbkdf2()).
var verificador = new VerificadorTextoPlano();

// Reglas de alerta (DIP-8): una alerta nueva se registra aqui.
var reglas = new IReglaDeAlerta[]
{
    new ReglaStockMinimo(),
    new ReglaVencimiento(reloj, diasDePreaviso: 30)
};

// Servicios: solo reciben abstracciones.
var servicioCatalogo =
    new ServicioCatalogo(repositorioCatalogo);

var servicioAlertas =
    new ServicioAlertas(repositorioCatalogo, reglas, notificador);

var servicioVenta =
    new ServicioVenta(repositorioMovimientos, reloj, notificador);

var servicioCliente =
    new ServicioCliente(repositorioClientes, notificador);

var servicioUsuario =
    new ServicioUsuario(repositorioUsuarios, verificador);

// ================= CARGA TXT =================

Console.ForegroundColor =
    ConsoleColor.DarkGreen;

Console.WriteLine(
    "Cargando información del sistema...\n");

Console.ResetColor();

// La carga se dispara aqui y solo aqui (ICargable, §8): ningun servicio
// puede recargar un archivo a mitad de sesion.
var cargables = new (string Etiqueta, ICargable Origen)[]
{
    ("Productos", repositorioCatalogo),
    ("Clientes", repositorioClientes),
    ("Usuarios", repositorioUsuarios)
};

foreach (var (etiqueta, origen) in cargables)
{
    Console.WriteLine(
        Describir(etiqueta, origen.Cargar()));
}

Console.WriteLine();

// ================= LOGIN =================

Console.ForegroundColor =
    ConsoleColor.Blue;

Console.WriteLine(
    "=========== LOGIN ===========");

Console.ResetColor();

Console.Write("Usuario: ");
string user =
    Console.ReadLine()!;

Console.Write("Contraseña: ");
string password =
    Console.ReadLine()!;

bool login =
    servicioUsuario.Login(
        user,
        password);

if (!login)
{
    Console.ForegroundColor =
        ConsoleColor.Red;

    Console.WriteLine(
        "\nAcceso denegado");

    Console.ResetColor();

    return;
}

Console.ForegroundColor =
    ConsoleColor.Green;

Console.WriteLine(
    "\nLogin correcto");

Console.ResetColor();

// ================= ALERTAS =================

servicioAlertas.Revisar();

// ================= MENÚ =================

int opcion = 0;

while (opcion != 8)
{
    Console.ForegroundColor =
        ConsoleColor.Magenta;

    Console.WriteLine("\n==============================");
    Console.WriteLine("      SISTEMA FARMACIA");
    Console.WriteLine("==============================");

    Console.ResetColor();

    // SC-2 (decision del equipo, 2026-08-09): los servicios ganan la opcion 2
    // y las demas se corren. Reorganiza lineas del menu original: amparado en
    // la autorizacion de SC-2 y declarado en 04-evidencia/sc2/metrica-sc2.md.
    Console.WriteLine("1. Ver productos");
    Console.WriteLine("2. Ver servicios");
    Console.WriteLine("3. Ver clientes");
    Console.WriteLine("4. Buscar producto");
    Console.WriteLine("5. Registrar venta");
    Console.WriteLine("6. Acumular puntos");
    Console.WriteLine("7. Ver alertas");
    Console.WriteLine("8. Salir");

    Console.Write("\nSeleccione opción: ");

    opcion =
        int.Parse(Console.ReadLine()!);

    switch (opcion)
    {
        case 1:

            Console.ForegroundColor =
                ConsoleColor.Cyan;

            Console.WriteLine(
                "\n===== PRODUCTOS =====");

            Console.ResetColor();

            Console.WriteLine(
                "Nombre\t\tStock\tPrecio");

            Console.WriteLine(
                "-----------------------------------");

            foreach (var articulo in
                servicioCatalogo.Obtener())
            {
                if (articulo is
                    IInventariable inventariable)
                {
                    Console.WriteLine(
                        $"{articulo.Nombre}\t\t" +
                        $"{inventariable.Existencias}\t" +
                        $"{articulo.Precio}");
                }
            }

            break;

        case 2:

            // SC-2: los servicios tienen su propia opcion de menu, con
            // duracion en vez de existencias (salida nueva autorizada).
            Console.ForegroundColor =
                ConsoleColor.Cyan;

            Console.WriteLine(
                "\n===== SERVICIOS =====");

            Console.ResetColor();

            Console.WriteLine(
                "Nombre\t\tDuración\tPrecio");

            Console.WriteLine(
                "-----------------------------------");

            foreach (var servicio in
                servicioCatalogo.Obtener()
                .OfType<Servicio>())
            {
                Console.WriteLine(
                    $"{servicio.Nombre}\t\t" +
                    $"{servicio.DuracionMinutos} min\t\t" +
                    $"{servicio.Precio}");
            }

            break;

        case 3:

            Console.ForegroundColor =
                ConsoleColor.Green;

            Console.WriteLine(
                "\n===== CLIENTES =====");

            Console.ResetColor();

            foreach (var cliente in
                servicioCliente.Obtener())
            {
                Console.WriteLine(
                    $"{cliente.Nombre} - " +
                    $"Puntos: {cliente.Puntos}");
            }

            break;

        case 4:

            Console.Write(
                "\nIngrese nombre producto: ");

            string nombre =
                Console.ReadLine()!;

            var articuloBuscado =
                servicioCatalogo
                .BuscarPorNombre(nombre);

            if (articuloBuscado != null)
            {
                Console.WriteLine(
                    $"\nProducto: " +
                    $"{articuloBuscado.Nombre}");

                Console.WriteLine(
                    $"Precio: " +
                    $"{articuloBuscado.Precio}");

                if (articuloBuscado is
                    IInventariable inventario)
                {
                    Console.WriteLine(
                        $"Stock: " +
                        $"{inventario.Existencias}");
                }
                else if (articuloBuscado is
                    Servicio servicioBuscado)
                {
                    // SC-2: salida nueva autorizada para el tipo nuevo.
                    Console.WriteLine(
                        $"Duración: " +
                        $"{servicioBuscado.DuracionMinutos} min");
                }
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }

            break;

        case 5:

            Console.Write(
                "\nNombre producto: ");

            string nombreVenta =
                Console.ReadLine()!;

            var articuloVenta =
                servicioCatalogo
                .BuscarPorNombre(nombreVenta);

            if (articuloVenta != null)
            {
                Console.Write(
                    "Cantidad: ");

                int cantidad =
                    int.Parse(
                        Console.ReadLine()!);

                servicioVenta
                    .RegistrarVenta(
                        articuloVenta,
                        cantidad);

                Console.WriteLine(
                    "\nVenta registrada");
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }

            break;

        case 6:

            Console.Write(
                "\nNombre cliente: ");

            string nombreCliente =
                Console.ReadLine()!;

            var clientePuntos =
                servicioCliente
                .BuscarPorNombre(nombreCliente);

            if (clientePuntos != null)
            {
                Console.Write(
                    "Puntos: ");

                int puntos =
                    int.Parse(
                        Console.ReadLine()!);

                servicioCliente
                    .AcumularPuntos(
                        clientePuntos,
                        puntos);
            }
            else
            {
                Console.WriteLine(
                    "\nCliente no encontrado");
            }

            break;

        case 7:

            Console.WriteLine(
                "\nVerificando alertas...");

            servicioAlertas.Revisar();

            break;

        case 8:

            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "\nSaliendo del sistema...");

            Console.ResetColor();

            break;

        default:

            Console.WriteLine(
                "\nOpción inválida");

            break;
    }
}

Console.WriteLine(
    "\nFIN DEL SISTEMA");

// Traduce el resultado de carga a las MISMAS tres cadenas de hoy (§16):
// el actor que redacta los mensajes ya no vive dentro del repositorio (H-02).
static string Describir(string etiqueta, ResultadoDeCarga resultado)
{
    return resultado.Estado switch
    {
        EstadoCarga.Exitosa => $"{etiqueta} cargados",
        EstadoCarga.ArchivoNoEncontrado => "Archivo no encontrado",
        _ => resultado.Detalle ?? string.Empty
    };
}
