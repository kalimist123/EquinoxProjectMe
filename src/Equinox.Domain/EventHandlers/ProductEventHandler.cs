using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class ProductEventHandler :
        INotificationHandler<ProductRegisteredEvent>,
        INotificationHandler<ProductUpdatedEvent>,
        INotificationHandler<ProductRemovedEvent>
    {
        public void Handle(ProductUpdatedEvent message)
        {
            // Send some notification e-mail
        }

        public void Handle(ProductRegisteredEvent message)
        {
            // Send some greetings e-mail
        }

        public void Handle(ProductRemovedEvent message)
        {
            // Send some see you soon e-mail
        }
    }
}