using Microsoft.EntityFrameworkCore;
using prjPedidos.Data;
using prjPedidos.Models;
using prjPedidos.Repositories.Interfaces;
using prjPedidos.ViewModels;

namespace prjPedidos.Repositories
{
    public class ItemRepository : IItensPedidoRepository
    {
        private readonly SistemaPedidosDB _dbContext;

        public ItemRepository(SistemaPedidosDB sistemaPedidosDB)
        {
            _dbContext = sistemaPedidosDB;
        }

        public async Task<List<ItensPedidoModel>> BuscarItens()
        {
            return await _dbContext.ItensPedido.ToListAsync();
        }

        public async Task<ItensPedidoModel> BuscarPorId(int id)
        {
            return await _dbContext.ItensPedido.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ItensPedidoModel>> BuscarPorPedido(int idPedido)
        {
            return await _dbContext.ItensPedido
                .Where(x => x.PedidoId == idPedido)
                .ToListAsync();
        }

        public async Task<ItensPedidoModel> AdicionarItem(ItensPedidoCreateDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Cria uma nova instância de ItensPedidoModel a partir do DTO
            var item = new ItensPedidoModel
            {
                PedidoId = dto.PedidoId,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade
            };

            // Adiciona o item ao DbContext e salva as mudanças
            await _dbContext.ItensPedido.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<ItensPedidoModel> AtualizarItem(ItensPedidoUpdateDTO dto, int id)
        {
            // Encontra o item existente
            var itemExistente = await _dbContext.ItensPedido.FindAsync(id);

            if (itemExistente == null)
            {
                throw new Exception($"Item para o ID {id} não encontrado");
            }

            // Atualiza os campos do item com os valores do DTO
            itemExistente.PedidoId = dto.PedidoId;
            itemExistente.ProdutoId = dto.ProdutoId;
            itemExistente.Quantidade = dto.Quantidade;

            // Marca o item como modificado e salva as mudanças
            _dbContext.ItensPedido.Update(itemExistente);
            await _dbContext.SaveChangesAsync();

            return itemExistente;
        }

        public async Task<bool> ApagarItem(int id)
        {
            ItensPedidoModel itemPorId = await BuscarPorId(id);
            if (itemPorId == null)
            {
                throw new Exception($"Item para o ID {id} não encontrado");
            }

            _dbContext.ItensPedido.Remove(itemPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
