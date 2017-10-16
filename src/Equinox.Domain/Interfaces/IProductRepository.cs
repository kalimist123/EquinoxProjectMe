using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByCode(string code);
    }
}