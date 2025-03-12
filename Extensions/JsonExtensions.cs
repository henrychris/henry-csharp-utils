using System.Text.Json;

namespace HenryUtils.Extensions;

public static class JsonExtensions
{
    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}
