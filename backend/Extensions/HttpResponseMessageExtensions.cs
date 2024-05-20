using Newtonsoft.Json;

namespace backend.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> ConvertIntoObject<T>(this HttpResponseMessage message) where T : class
    {
        var responseString = await message.Content.ReadAsStringAsync();
        
        var converted = JsonConvert.DeserializeObject<T>(responseString);

        if (converted is null)
        {
            // TODO: custom exception
            throw new Exception("shits broken");
        }

        return converted;
    }
}