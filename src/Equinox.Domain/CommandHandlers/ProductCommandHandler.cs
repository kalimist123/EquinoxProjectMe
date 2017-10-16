using System;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using MediatR;

namespace Equinox.Domain.CommandHandlers
{
    public class ProductCommandHandler : CommandHandler,
        INotificationHandler<RegisterNewProductCommand>,
        INotificationHandler<UpdateProductCommand>,
        INotificationHandler<RemoveProductCommand>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMediatorHandler Bus;

        public ProductCommandHandler(IProductRepository ProductRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _ProductRepository = ProductRepository;
            Bus = bus;
        }

        public void Handle(RegisterNewProductCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var Product = new Product(Guid.NewGuid(), message.Name, message.Code, message.Category);

            if (_ProductRepository.GetByCode(Product.Code) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The Product code has already been taken."));
                return;
            }
            
            _ProductRepository.Add(Product);

            if (Commit())
            {
                Bus.RaiseEvent(new ProductRegisteredEvent(Product.Id, Product.Name, Product.Category, Product.Code));
            }
        }

        public void Handle(UpdateProductCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var Product = new Product(message.Id, message.Name, message.Category, message.Code);
            var existingProduct = _ProductRepository.GetByCode(Product.Code);

            if (existingProduct != null && existingProduct.Id != Product.Id)
            {
                if (!existingProduct.Equals(Product))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType,"The Product e-Code has already been taken."));
                    return;
                }
            }

            _ProductRepository.Update(Product);

            if (Commit())
            {
                Bus.RaiseEvent(new ProductUpdatedEvent(Product.Id, Product.Name, Product.Category, Product.Code));
            }
        }

        public void Handle(RemoveProductCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            _ProductRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new ProductRemovedEvent(message.Id));
            }
        }

        public void Dispose()
        {
            _ProductRepository.Dispose();
        }
    }
}