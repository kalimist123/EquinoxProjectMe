using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class ProductCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Code { get; protected set; }

        public string Category { get; protected set; }


    }
}