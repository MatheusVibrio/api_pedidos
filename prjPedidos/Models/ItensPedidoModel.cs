namespace prjPedidos.Models
{
    public class ItensPedidoModel
    {
        public int Id { get; set; }
        public int PedidoId { get; set; } 
        public virtual PedidoModel Pedido { get; set; }
        public int ProdutoId { get; set; } 
        public virtual ProdutoModel Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
