﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using BlogPessoal.src.repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogPessoal.src.servicos.implementacoes
{
    public class AutenticacaoServicos : IAutenticacao
    {
        /// <summary>
        /// <para>Resumo: Classe responsavel por implementar IAutenticacao</para>
        /// <para>Criado por: Erick Chiappone</para>
        /// <para>Versão: 1.0</para>
        /// <para>Data: 16/05/2022</para>
        /// </summary>
        #region Atributos

        private readonly IUsuario _repositorio;
        public IConfiguration Configuracao { get; }

        #endregion

        #region Construtores

        public AutenticacaoServicos(IUsuario repositorio, IConfiguration configuration)
        {
            _repositorio = repositorio;
            Configuracao = configuration;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// <para>Resumo: Metodo responsavel por criptografar senha</para>
        /// </summary>
        /// <param name="senha">Senha a ser criptografada</param>
        /// <returns>string</returns>
        public string CodificarSenha(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por criar usuario sem duplicar no banco</para>
        /// </summary>
        /// <param name="dto">NovoUsuarioDTO</param>
        public async Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO dto)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(dto.Email);

            if (usuario != null) throw new Exception("Este email já está sendo utilizado");

            dto.Senha = CodificarSenha(dto.Senha);

            await _repositorio.NovoUsuarioAsync(dto);
        }

        /// <summary>
        /// <para>Resumo: Metodo responsavel por gerar token JWT</para>
        /// </summary>
        /// <param name="usuario">UsuarioModelo</param>
        /// <returns>string</returns>
        public string GerarToken(UsuarioModelo usuario)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracao["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                        new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }

        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel devolver autorizacao para usuario autenticado</para>
        /// </summary>
        /// <param name="dto">AutenticarDTO</param>
        /// <returns>AutorizacaoDTO</returns>
        /// <exception cref="Exception">Usuario nao encontrado</exception>
        /// <exception cref="Exception">Senha incorreta</exception>
        public async Task<AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO dto)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(dto.Email);

            if (usuario == null) throw new Exception("Usuário não encontrado");

            if (usuario.Senha != CodificarSenha(dto.Senha)) throw new Exception("Senha incorreta");

            return new AutorizacaoDTO(usuario.Id, usuario.Email, usuario.Tipo, GerarToken(usuario));
        }

        #endregion
    }
}