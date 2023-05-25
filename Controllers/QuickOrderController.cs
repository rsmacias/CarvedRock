using Microsoft.AspNetCore.Mvc;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuickOrderController : ControllerBase {

    private readonly IQuickOrderLogic _orderLogic;

    public QuickOrderController(IQuickOrderLogic orderLogic) {
        _orderLogic = orderLogic;
    }

    [HttpPost]
    public Guid SubmitQuickOrder(QuickOrder orderInfo) {
        return _orderLogic.PlaceQuickOrder(orderInfo, 1234); // would get customer id from authhN system/User claims
    }

}