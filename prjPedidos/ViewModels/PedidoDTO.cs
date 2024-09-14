namespace prjPedidos.ViewModels
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemPedidoDTO> ItensPedido { get; set; }
    }
}
