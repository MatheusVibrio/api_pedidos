using prjPedidos.Models;
using prjPedidos.ViewModels;

namespace prjPedidos.Repositories.Interfaces
{
    public interface IItensPedidoRepository
    {
        Task<List<ItensPedidoModel>> BuscarItens();
        Task<ItensPedidoModel> BuscarPorId(int id);
        Task<List<ItensPedidoModel>> BuscarPorPedido(int idPedido);
        Task<ItensPedidoModel> AdicionarItem(ItensPedidoCreateDTO dto);
        Task<ItensPedidoModel> AtualizarItem(ItensPedidoUpdateDTO dto, int id);
        Task<bool> ApagarItem(int id);
    }
}
