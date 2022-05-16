﻿using System;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Autenticacao")]
    [Produces("application/json")]
    public class AutenticacaoControlador : ControllerBase
    {
        #region Atributos
        private readonly IAutenticacao _servicos;
        #endregion


        #region Construtores
        public AutenticacaoControlador(IAutenticacao servicos)
        {
            _servicos = servicos;
        }
        #endregion


        #region Métodos

        /// <summary>
        /// Pegar Autorizacao
        /// </summary>
        /// <param name="autenticacao">AutenticarDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisicao:
        ///
        ///     POST /api/Autenticacao
        ///     {
        ///        "email": "chiappone@domain.com",
        ///        "senha": "134652"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisicao</response>
        /// <response code="401">Email ou senha invalido</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AutorizacaoDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Autenticar([FromBody] AutenticarDTO autenticacao)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var autorizacao = await _servicos.PegarAutorizacaoAsync(autenticacao);
                return Ok(autorizacao);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}