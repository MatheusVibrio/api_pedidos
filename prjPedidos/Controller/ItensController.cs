using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjPedidos.Models;
using prjPedidos.Repositories;
using prjPedidos.Repositories.Interfaces;
using prjPedidos.ViewModels;

namespace prjPedidos.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensController : ControllerBase
    {
        private readonly IItensPedidoRepository _itensRepository;

        public ItensController(IItensPedidoRepository itensRepository) {
            _itensRepository = itensRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItensPedidoModel>>> BuscarItens()
        {
            List<ItensPedidoModel> itens = await _itensRepository.BuscarItens();
            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItensPedidoModel>> BuscaPorId(int id)
        {
            ItensPedidoModel item = await _itensRepository.BuscarPorId(id);
            return Ok(item);
        }

        [HttpGet("pedido/{idPedido}")]
        public async Task<ActionResult<List<ItensPedidoModel>>> BuscaPorPedido(int idPedido)
        {
            List<ItensPedidoModel> itens = await _itensRepository.BuscarPorPedido(idPedido);
            return Ok(itens);
        }

        [HttpPost]
        public async Task<ActionResult<ItensPedidoModel>> Cadastrar([FromBody] ItensPedidoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Adiciona o item ao repositório
            var item = await _itensRepository.AdicionarItem(dto);

            return CreatedAtAction(nameof(BuscaPorId), new { id = item.Id }, item);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ItensPedidoModel>> Atualizar(int id, [FromBody] ItensPedidoUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Atualiza o item usando o DTO
                var itemAtualizado = await _itensRepository.AtualizarItem(dto, id);

                // Retorna o item atualizado
                return Ok(itemAtualizado);
            }
            catch (Exception ex)
            {
                // Retorna 404 Not Found se o item não for encontrado
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ItensPedidoModel>> Apagar(int id)
        {
            bool apagado = await _itensRepository.ApagarItem(id);
            return Ok(apagado);

        }
    }
}
