namespace BibFarmacia.Interfaces
{
    // Interfaz de rol (ISP): la venta ve solo esto. Contrato: descuenta
    // cantidad del inventario del articulo. Separada de IInventariable a
    // proposito: aquella es de solo lectura y la consumen las reglas de alerta.
    public interface IDespachable
    {
        void Despachar(int cantidad);
    }
}
