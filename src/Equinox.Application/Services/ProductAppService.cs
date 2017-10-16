using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;

namespace Equinox.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _ProductRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public ProductAppService(IMapper mapper,
                                  IProductRepository ProductRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _ProductRepository = ProductRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return _ProductRepository.GetAll().ProjectTo<ProductViewModel>();
        }

        public ProductViewModel GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(_ProductRepository.GetById(id));
        }

        public void Register(ProductViewModel ProductViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewProductCommand>(ProductViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(ProductViewModel ProductViewModel)
        {
            var updateCommand = _mapper.Map<UpdateProductCommand>(ProductViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveProductCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<ProductHistoryData> GetAllHistory(Guid id)
        {
            return ProductHistory.ToJavaScriptProductHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
