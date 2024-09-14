using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjPedidos.Models;
using prjPedidos.Repositories.Interfaces;
using prjPedidos.ViewModels;

namespace prjPedidos.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDTO>> BuscaPorId(int id)
        {
            // Chama o método do repositório para obter o pedido como PedidoDTO
            var pedidoDTO = await _pedidoRepository.BuscarPorId(id);

            // Verifica se o pedido é nulo
            if (pedidoDTO == null)
                return NotFound();

            // Retorna o PedidoDTO diretamente
            return Ok(pedidoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoModel>> AdicionarPedido([FromBody] PedidoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Chama o método do repositório para adicionar o pedido
            var pedido = await _pedidoRepository.AdicionarPedido(dto);

            // Retorna o pedido criado com o status 201 Created
            return CreatedAtAction(nameof(BuscaPorId), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(int id, [FromBody] PedidoUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pedidoAtualizado = await _pedidoRepository.AtualizarPedido(id, dto);

                // Retorna 204 No Content se a atualização foi bem-sucedida
                return NoContent();
            }
            catch (Exception ex)
            {
                // Retorna 404 Not Found se o pedido não foi encontrado
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<PedidoModel>> Apagar(int id)
        {
            bool apagado = await _pedidoRepository.ApagarPedido(id);
            return Ok(apagado);

        }
    }
}
