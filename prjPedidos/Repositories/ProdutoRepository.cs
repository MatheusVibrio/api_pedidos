using Microsoft.EntityFrameworkCore;
using prjPedidos.Data;
using prjPedidos.Models;
using prjPedidos.Repositories.Interfaces;

namespace prjPedidos.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly SistemaPedidosDB _dbContext;

        public ProdutoRepository(SistemaPedidosDB sistemaPedidosDB)
        {
            _dbContext = sistemaPedidosDB;
        }

        public async Task<ProdutoModel> BuscarPorId(int id)
        {
            return await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        public async Task<ProdutoModel> AdicionarProduto(ProdutoModel produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            return produto;
        }

        public async Task<ProdutoModel> AtualizarProduto(ProdutoModel produto, int id)
        {
            ProdutoModel produtoPorId = await BuscarPorId(id);
            if (produtoPorId == null)
            {
                throw new Exception($"Produto para o ID {id} não encontrado");
            }

            produtoPorId.NomeProduto = produto.NomeProduto;
            produtoPorId.Valor = produto.Valor;

            _dbContext.Produtos.Update(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return produtoPorId;
        }

        public async Task<bool> ApagarProduto(int id)
        {
            ProdutoModel produtoPorId = await BuscarPorId(id);
            if (produtoPorId == null)
            {
                throw new Exception($"Produto para o ID {id} não encontrado");
            }

            _dbContext.Produtos.Remove(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
