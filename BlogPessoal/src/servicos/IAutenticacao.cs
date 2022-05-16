using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;

namespace BlogPessoal.src.servicos
{
    public interface IAutenticacao
    {
        /// <summary>
        /// <para>Resumo: Interface Responsavel por representar ações de autenticacao</para>
        /// <para>Criado por: Erick Chiappone</para>
        /// <para>Versao: 1.0</para>
        /// <para>Data: 16/05/2022</para>
        /// </summary>
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO dto);
        string GerarToken(UsuarioModelo usuario);
        Task<AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO dto);
    }
}