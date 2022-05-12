using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Tema</para>
    /// <para> Criado por: Erick Chiappone</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>

    public interface ITema
    {
        List<TemaModelo> PegarTodosTemas();
        Task<TemaModelo> PegarTemaPeloIdAsync(int id);
        Task<List<TemaModelo>> PegarTemasPelaDescricaoAsync(string descricao);
        Task NovoTemaAsync(NovoTemaDTO tema);
        Task AtualizarTemaAsync(AtualizarTemaDTO tema);
        Task DeletarTemaAsync(int id);
    }
}