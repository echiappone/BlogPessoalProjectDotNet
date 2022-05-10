using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo Tema</para>
    /// <para> Criado por: Erick Chiappone</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NovoTemaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Descricao { get; set; }

        public NovoTemaDTO(string descricao)
        {
            Descricao = descricao;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para alterar um novo Tema</para>
    /// <para> Criado por: Erick Chiappone</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AtualizarTemaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Descricao { get; set; }

        public AtualizarTemaDTO(string descricao)
        {
            Descricao = descricao;
        }
    }
}
