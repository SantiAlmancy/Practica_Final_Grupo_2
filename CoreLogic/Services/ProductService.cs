using System.Net.Http;
using System.Text.Json;

namespace UPB.CoreLogic.Services;

public class ProductService
{
    public ProductService()
    {

    }

    public async Task<double> getRandom()
    {
        HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://random-data-api.com/api/number/random_number"),
        };

        //Respuesta
        using HttpResponseMessage response = await sharedClient.GetAsync("");

        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var element = JsonSerializer.Deserialize<JsonElement>(json);
        var decimalValue = element.GetProperty("decimal").GetDouble();
        return decimalValue;
    }
}