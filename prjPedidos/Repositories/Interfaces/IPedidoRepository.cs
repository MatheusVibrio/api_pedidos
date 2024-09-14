using prjPedidos.Models;
using prjPedidos.ViewModels;

namespace prjPedidos.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<List<PedidoModel>> BuscarPedidos();
        Task<PedidoDTO> BuscarPorId(int id);
        Task<PedidoModel> BuscarPorIdProduto(int id);
        Task<PedidoModel> AdicionarPedido(PedidoCreateDTO dto);
        Task<PedidoModel> AtualizarPedido(int id, PedidoUpdateDTO dto);
        Task<bool> ApagarPedido(int id);
    }
}
