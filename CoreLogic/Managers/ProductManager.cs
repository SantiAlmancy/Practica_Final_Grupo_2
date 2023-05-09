using System.IO;
using System.Text.Json;
using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class ProductManager
{
    private string _path;
    
    public ProductManager(string path)
    {
        _path = path;
    }

    public Product Create(string name, string tipo, int stock, string code)
    {
        if(!tipo.Equals("SOCCER") && !tipo.Equals("BASKET"))
        {
            throw new Exception("Error, tipo de producto no valido");
        }

        string jsonString = File.ReadAllText(_path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonString,options);
        // Asignar nuevo código
        // Encontrar el código más alto en la categoría
        int maxCode =0;
        if(products.Any() && products.Find(p => p.Type == tipo) != null)
        {
            maxCode = products.Where(p => p.Type == tipo)
                .Select(p => int.Parse(p.Code.Split('-')[1]))
                .Max();
        }

        // Generar un nuevo código único para el nuevo producto
        string newCode = $"{tipo}-{(maxCode + 1).ToString("000")}";

        Product newProduct = new Product()
        {
            Code = newCode,
            Name = name,
            Type = tipo,
            Stock = stock,
            Price = 0
        };

        products.Add(newProduct);
        string jsonStringUpdated = JsonSerializer.Serialize(products,options);
        File.WriteAllText(_path, jsonStringUpdated);

        return newProduct;
    }

    public List<Product> GetAll()
    {
        if (!File.Exists(_path))
        {
            return new List<Product>();
        }

        string json = File.ReadAllText(_path);
        JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;
        List<Product> products = new List<Product>();

         if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement element in root.EnumerateArray())
            {
                Product product = new Product
                {
                    Name = element.GetProperty("Name").GetString(),
                    Type = element.GetProperty("Type").GetString(),
                    Stock = element.GetProperty("Stock").GetInt32(),
                    Code = element.GetProperty("Code").GetString(),
                    Price = element.GetProperty("Price").GetDouble()
                };
                products.Add(product);
            }
        }

        return products;
    }

    public Product GetByCode(string code)
    {
        if (code.Length != 10)
        {
            throw new Exception ("Error, el codigo no es valido");
        }

        string json = File.ReadAllText(_path);
        JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;

        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement element in root.EnumerateArray())
            {
                if (element.GetProperty("Code").GetString() == code)
                {
                    return new Product
                    {
                        Name = element.GetProperty("Name").GetString(),
                        Type = element.GetProperty("Type").GetString(),
                        Stock = element.GetProperty("Stock").GetInt32(),
                        Code = element.GetProperty("Code").GetString(),
                        Price = element.GetProperty("Price").GetDouble()
                    };
                }
            }
        }

        throw new Exception("Error, no se encontro un producto con el code: " + code);
    }
    
}