namespace prjPedidos.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }

        // Propriedade de navegação para os itens do pedido
        public virtual ICollection<ItensPedidoModel> ItensPedido { get; set; } = new List<ItensPedidoModel>();
    }
}
