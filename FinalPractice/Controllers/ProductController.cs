using Microsoft.AspNetCore.Mvc;
using UPB.CoreLogic.Models;
using UPB.CoreLogic.Managers;

namespace UPB.FinalPractice.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
   private readonly ProductManager _productmanager;

   public ProductController(ProductManager productmanager)
   {
      _productmanager = productmanager;
   }

   [HttpPost]
   public Product Post([FromBody]Product patientToCreate)
   {
      return _productmanager.Create(patientToCreate.Name, patientToCreate.Type, patientToCreate.Stock, patientToCreate.Code);
   }
}