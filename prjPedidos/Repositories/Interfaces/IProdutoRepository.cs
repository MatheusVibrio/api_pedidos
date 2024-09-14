using prjPedidos.Models;

namespace prjPedidos.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoModel>> BuscarTodosProdutos();
        Task<ProdutoModel> BuscarPorId(int id);
        Task<ProdutoModel> AdicionarProduto(ProdutoModel produto);
        Task<ProdutoModel> AtualizarProduto(ProdutoModel produto, int id);
        Task<bool> ApagarProduto(int id);

    }
}
