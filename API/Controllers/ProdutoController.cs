using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProdutoController : ControllerBase
	{
		private readonly IProdutoRepository _produtoRepository;

		public ProdutoController(IProdutoRepository produtoRepository)
		{
			_produtoRepository = produtoRepository;
		}

		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var produtos = await _produtoRepository.ObterTodosAsync();
			return Ok(produtos);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> ObterPorId(Guid id)
		{
			var produto = await _produtoRepository.ObterPorIdAsync(id);
			if (produto == null)
			{
				return NotFound();
			}
			return Ok(produto);
		}

		[HttpPost]
		public async Task<ActionResult<Produto>> Criar([FromBody] Produto produto)
		{

			produto.Id = Guid.NewGuid();

			await _produtoRepository.CriarAsync(produto);
			return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Atualizar(Guid id, [FromBody] Produto produto)
		{
			if(id != produto.Id) return BadRequest("O ID da URL é diferente do corpo da requisição");

			var existente = await _produtoRepository.ObterPorIdAsync(id);

			if(existente == null) return NotFound();

			await _produtoRepository.AtualizarAsync(produto);

			return Ok(new { mensagem = "Produto atualizado com sucesso!" });
		}

		[HttpDelete("{id}")]

		public async Task<IActionResult> Deletar(Guid id)
		{
			var produto = await _produtoRepository.ObterPorIdAsync(id);

			if (produto == null) return NotFound();

			await _produtoRepository.DeletarAsync(id);
			return Ok(new { mensagem = "Produto deletado com sucesso!"});
		}
	}
}