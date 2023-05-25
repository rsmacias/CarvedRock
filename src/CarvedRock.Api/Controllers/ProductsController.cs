using Microsoft.AspNetCore.Mvc;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.ApiModels;
using Serilog;

namespace CarvedRock.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase {

    private readonly IProductLogic _productLogic;

    public ProductsController (IProductLogic productLogic) {
        _productLogic = productLogic;
    }

    [HttpGet]
    public IEnumerable<Product> GetProducts (string category = "all") {
        Log.ForContext("Category", category)
           .Information("Starting controller action GetProducts for {category}", category);
        return _productLogic.GetProductForCategory(category);
    }

}