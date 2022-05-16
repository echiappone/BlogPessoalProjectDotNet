using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilidades
{
    /// <summary>
    /// <para>Resumo: Metodo assincrono responsavel devolver autorizaçao para usuario autenticado</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }
}