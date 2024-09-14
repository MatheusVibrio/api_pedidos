using Microsoft.EntityFrameworkCore;
using prjPedidos.Data;
using prjPedidos.Models;
using prjPedidos.Repositories.Interfaces;
using prjPedidos.ViewModels;

namespace prjPedidos.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly SistemaPedidosDB _dbContext;
        public PedidoRepository(SistemaPedidosDB sistemaPedidosDB)
        {
            _dbContext = sistemaPedidosDB;
        }

        public async Task<PedidoModel> BuscarPorIdProduto(int id)
        {
            return await _dbContext.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PedidoDTO> BuscarPorId(int id)
        {
            // Obtém o pedido e seus itens
            var pedido = await _dbContext.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(i => i.Produto) // Inclui produto para obter o nome e valor
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return null;

            // Mapeia o pedido para o DTO
            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                NomeCliente = pedido.NomeCliente,
                EmailCliente = pedido.EmailCliente,
                Pago = pedido.Pago,
                ValorTotal = pedido.ItensPedido.Sum(i => i.Quantidade * i.Produto.Valor),
                ItensPedido = pedido.ItensPedido.Select(i => new ItemPedidoDTO
                {
                    Id = i.Id,
                    IdProduto = i.Id, 
                    NomeProduto = i.Produto.NomeProduto,
                    ValorUnitario = i.Produto.Valor,
                    Quantidade = i.Quantidade
                }).ToList()
            };

            return pedidoDTO;
        }


        public async Task<List<PedidoModel>> BuscarPedidos()
        {
            return await _dbContext.Pedidos.ToListAsync();
        }

        public async Task<PedidoModel> AdicionarPedido(PedidoCreateDTO dto)
        {
            // Cria uma nova instância de PedidoModel a partir do DTO
            var pedido = new PedidoModel
            {
                NomeCliente = dto.NomeCliente,
                EmailCliente = dto.EmailCliente,
                DataCriacao = DateTime.UtcNow, // Define a data de criação
                Pago = dto.Pago
            };

            // Adiciona o pedido ao DbContext e salva as mudanças
            await _dbContext.Pedidos.AddAsync(pedido);
            await _dbContext.SaveChangesAsync();

            return pedido;
        }

        public async Task<PedidoModel> AtualizarPedido(int id, PedidoUpdateDTO dto)
        {
            // Encontra o pedido existente
            var pedidoExistente = await _dbContext.Pedidos.FindAsync(id);

            if (pedidoExistente == null)
            {
                throw new Exception($"Pedido para o ID {id} não encontrado");
            }

            // Atualiza os campos do pedido com os valores do DTO
            pedidoExistente.NomeCliente = dto.NomeCliente;
            pedidoExistente.EmailCliente = dto.EmailCliente;
            pedidoExistente.Pago = dto.Pago;

            // Você pode não querer atualizar DataCriacao se ele deve permanecer constante após a criação
            // pedidoExistente.DataCriacao = dto.DataCriacao; // Comente ou remova esta linha se DataCriacao não deve ser atualizada

            // Marca o pedido como modificado e salva as mudanças
            _dbContext.Pedidos.Update(pedidoExistente);
            await _dbContext.SaveChangesAsync();

            return pedidoExistente;
        }

        public async Task<bool> ApagarPedido(int id)
        {
            PedidoModel pedidoPorId = await BuscarPorIdProduto(id);
            if (pedidoPorId == null)
            {
                throw new Exception($"Pedido para o ID {id} não encontrado");
            }

            _dbContext.Pedidos.Remove(pedidoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

     
    }
}
