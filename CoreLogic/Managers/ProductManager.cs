using System.IO;
using System.Text.Json;
using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class ProductManager
{
    private string _path;
    private int _number;
    
    public ProductManager(string path)
    {
        _path = path;
        _number = 0;
    }

    public Product Create(string name, string tipo, int stock, string code)
    {
        if(!tipo.Equals("SOCCER") || !tipo.Equals("BASKET"))
        {
            throw new Exception("Error, tipo de producto no valido");
        }

        Product newProduct = new Product()
        {
            Name = name,
            Type = tipo,
            Stock = stock,
            Code = string.Format("{0}-{1}",tipo,_number.ToString("D3")),
            Price = 0
        };

        string jsonString = File.ReadAllText(_path);
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonString);
        products.Add(newProduct);
        string jsonStringUpdated = JsonSerializer.Serialize(products);
        File.WriteAllText(_path, jsonStringUpdated);

        return newProduct;
    }
}