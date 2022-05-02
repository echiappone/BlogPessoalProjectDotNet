using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de postagem</para>
    /// <para> Criado por: Erick Chiappone</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPostagem
    {
        void NovaPostagem(NovaPostagemDTO postagem);
        void AtualizarPostagem(AtualizarPostagemDTO postagem);
        void DeletarPostagem(int Id);
        PostagemModelo PegarPostagemPeloId(int id);
        List <PostagemModelo> PegarTodasPostagens();
        List<PostagemModelo> PegarPostagensPeloTitilo(string titulo);
        List<PostagemModelo> PegarPostagensPelaDescricao(string descricao);

    }
}
