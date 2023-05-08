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
}