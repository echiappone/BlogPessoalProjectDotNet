using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using BlogPessoal.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {
        #region Atributos
        
        private readonly ITema _repositorio;

        #endregion


        #region Construtores

        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Pegar todos temas
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de temas</response>
        /// <response code="204">Lista vazia</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet, Authorize]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();

            if (lista.Count < 1) return NoContent();
            
            return Ok(lista);
        }

        /// <summary>
        /// Pegar tema pelo Id
        /// </summary>
        /// <param name="idTema">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema nao existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTema}"), Authorize]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            var tema = await _repositorio.PegarTemaPeloIdAsync(idTema);

            if (tema == null) return NotFound();
            
            return Ok(tema);
        }


        /// <summary>
        /// Pegar tema pela Descricao
        /// </summary>
        /// <param name="descricaoTema">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna temas</response>
        /// <response code="204">Descricao nao existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("pesquisa"), Authorize]
        public async Task<ActionResult> PegarTemasPelaDescricaoAsync([FromQuery] string descricaoTema)
        {
            var temas = await _repositorio.PegarTemasPelaDescricaoAsync(descricaoTema);

            if (temas.Count < 1) return NoContent();
            
            return Ok(temas);
        }

        /// <summary>
        /// Criar novo Tema
        /// </summary>
        /// <param name="tema">NovoTemaDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisicao:
        ///
        ///     POST /api/Temas
        ///     {
        ///        "descricao": "CSHARP",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Erro na requisicao</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Authorize]
        public async Task<ActionResult> NovoTemaAsync([FromBody] NovoTemaDTO tema)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repositorio.NovoTemaAsync(tema);
            
            return Created($"api/Temas", tema);
        }

        /// <summary>
        /// Atualizar Tema
        /// </summary>
        /// <param name="tema">AtualizarTemaDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisicao:
        ///
        ///     PUT /api/Temas
        ///     {
        ///        "id": 1,    
        ///        "descricao": "CSHARP"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna tema atualizado</response>
        /// <response code="400">Erro na requisicao</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut, Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarTema([FromBody] AtualizarTemaDTO tema)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repositorio.AtualizarTemaAsync(tema);
            
            return Ok(tema);
        }

        /// <summary>
        /// Deletar tema pelo Id
        /// </summary>
        /// <param name="idTema">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Tema deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarTema([FromRoute] int idTema)
        {
            await _repositorio.DeletarTemaAsync(idTema);
            return NoContent();
        }

        #endregion
    }
}