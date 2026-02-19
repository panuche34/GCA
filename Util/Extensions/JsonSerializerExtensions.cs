using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Util.Converter;

public static class JsonSerializerExtensions
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // permite que caracteres especiais (como acentos, emojis e caracteres unicode) sejam mantidos no JSON ao invés de serem escapados
        NumberHandling = JsonNumberHandling.AllowReadingFromString, // converte de string para number
        WriteIndented = true,  // opcional, se quiser JSON formatado
        Converters =
        {
            new JsonStringEnumConverter(), // converte o int para enum
            new JsonDateTimeConverter() // converte string datat para datetime
        }
    };

    public static string SerializeWithOptions<T>(this T value)
    {
        return JsonSerializer.Serialize(value, DefaultOptions);
    }

    // Sobrecarga que permite combinar as opções padrão com opções adicionais
    public static string SerializeWithOptions<T>(this T value, Action<JsonSerializerOptions> configureOptions)
    {
        var options = new JsonSerializerOptions(DefaultOptions);
        configureOptions(options);
        return JsonSerializer.Serialize(value, options);
    }

    // Se precisar do método de deserialização também
    public static T? DeserializeWithOptions<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }
}
