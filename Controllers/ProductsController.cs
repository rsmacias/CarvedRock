using Microsoft.AspNetCore.Mvc;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.ApiModels;

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
        return _productLogic.GetProductForCategory(category);
    }

}