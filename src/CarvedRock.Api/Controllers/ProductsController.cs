using Microsoft.AspNetCore.Mvc;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase {

    private readonly ILogger<ProductsController> _logger;
    private readonly IProductLogic _productLogic;

    public ProductsController (
        ILogger<ProductsController> logger,
        IProductLogic productLogic
    ) {
        _logger = logger;
        _productLogic = productLogic;
    }

    [HttpGet]
    public IEnumerable<Product> GetProducts (string category = "all") {
        _logger.LogInformation("Starting controller action GetProducts for {category}", category);
        return _productLogic.GetProductForCategory(category);
    }

}