using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjPedidos.Models;
using prjPedidos.Repositories.Interfaces;

namespace prjPedidos.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository) { 
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarProdutos()
        {
            List<ProdutoModel> produtos = await _produtoRepository.BuscarTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> BuscaPorId(int id)
        {
            ProdutoModel produto = await _produtoRepository.BuscarPorId(id);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> Cadastrar([FromBody] ProdutoModel produtoModel)
        {
            ProdutoModel produto = await _produtoRepository.AdicionarProduto(produtoModel);
            return Ok(produto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoModel>> Atualizar([FromBody] ProdutoModel produtoModel, int id)
        {
            produtoModel.Id = id;
            ProdutoModel produto = await _produtoRepository.AtualizarProduto(produtoModel, id);
            return Ok(produto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoModel>> Apagar(int id)
        {
            bool apagado = await _produtoRepository.ApagarProduto(id);
            return Ok(apagado);

        }
    }
}
