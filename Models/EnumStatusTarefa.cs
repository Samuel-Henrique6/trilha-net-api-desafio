using System.Text.Json.Serialization;

namespace TrilhaApiDesafio.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter<EnumStatusTarefa>))] // Converter para string, para ser exibido no Swagger um select de opções
    public enum EnumStatusTarefa
    {
        Pendente,
        Finalizado
    }
}