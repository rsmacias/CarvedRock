using CarvedRock.Api.ApiModels;
using CarvedRock.Api.Interfaces;

namespace CarvedRock.Api.Domain;

public class QuickOrderLogic : IQuickOrderLogic {
    
    public QuickOrderLogic() {
        
    }

    public Guid PlaceQuickOrder(ApiModels.QuickOrder order, int customerId) {
        return Guid.NewGuid();
    }
}