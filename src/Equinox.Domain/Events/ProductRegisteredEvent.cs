using System;
using System.Data.Common;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class ProductRegisteredEvent : Event
    {
        public ProductRegisteredEvent(Guid id, string name, string category, string code)
        {
            Id = id;
            Name = name;
            Category = category;
            Code = code;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public string Category { get; private set; }
    }
}