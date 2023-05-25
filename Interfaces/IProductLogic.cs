using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Interfaces {
    public interface IProductLogic {
        IEnumerable<Product> GetProductForCategory (string category);
    }
}