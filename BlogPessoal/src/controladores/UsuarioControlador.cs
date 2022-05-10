using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UsuarioControlador : ControllerBase
    {
        #region Atributos
        private readonly IUsuario _repositorio;
        #endregion

        #region Construtores
        public UsuarioControlador(IUsuario repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos

        // Método PegarUsuarioPeloId
        [HttpGet("id/{idUsuario}")]
        public IActionResult PegarUsuarioPeloId([FromRoute] int idUsuario)
        {
            var usuario = _repositorio.PegarUsuarioPeloId(idUsuario);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // Método PegarUsuariosPeloNome
        [HttpGet]
        public IActionResult PegarUsuariosPeloNome([FromQuery] string nomeUsuario)
        {
            var usuarios = _repositorio.PegarUsuarioPeloNome(nomeUsuario);
            if (usuarios.Count < 1) return NoContent();
            return Ok(usuarios);
        }

        // Método PegarUsuarioPeloEmail
        [HttpGet("email/{emailUsuario}")]
        public IActionResult PegarUsuarioPeloEmail([FromRoute] string emailUsuario)
        {
            var usuario = _repositorio.PegarUsuarioPeloEmail(emailUsuario);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // Método NovoUsuario
        [HttpPost]
        public IActionResult NovoUsuario([FromBody] NovoUsuarioDTO usuario)
        {
            if(!ModelState.IsValid) return BadRequest();
            _repositorio.NovoUsuario(usuario);
            return Created($"api/Usuarios/{usuario.Email}", usuario);
        }

        // Método AtualizarUsuario
        [HttpPut]
        public IActionResult AtualizarUsuario([FromBody] AtualizarUsuarioDTO usuario)
        {
            if(!ModelState.IsValid) return BadRequest();
            _repositorio.AtualizarUsuario(usuario);
            return Ok(usuario);
        }

        // Método DeletarUsuario
        [HttpDelete("deletar/{idUsuario}")]
        public IActionResult DeletarUsuario([FromRoute] int idUsuario)
        {
            _repositorio.DeletarUsuario(idUsuario);
            return NoContent();
        }
        #endregion
    }
}