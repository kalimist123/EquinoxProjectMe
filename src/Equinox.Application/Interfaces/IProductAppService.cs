using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        void Register(ProductViewModel ProductViewModel);
        IEnumerable<ProductViewModel> GetAll();
        ProductViewModel GetById(Guid id);
        void Update(ProductViewModel ProductViewModel);
        void Remove(Guid id);
        IList<ProductHistoryData> GetAllHistory(Guid id);
    }
}
