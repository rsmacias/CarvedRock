using Microsoft.AspNetCore.Mvc;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuickOrderController : ControllerBase {

    private readonly ILogger<QuickOrderController> _logger;
    private readonly IQuickOrderLogic _orderLogic;

    public QuickOrderController(
        ILogger<QuickOrderController> logger,
        IQuickOrderLogic orderLogic
    ) {
        _logger = logger;
        _orderLogic = orderLogic;
    }

    [HttpPost]
    public Guid SubmitQuickOrder(QuickOrder orderInfo) {
        _logger.LogInformation($"Submitting order for {orderInfo.Quantity} of {orderInfo.ProductId}.");
        return _orderLogic.PlaceQuickOrder(orderInfo, 1234); // would get customer id from authhN system/User claims
    }

}