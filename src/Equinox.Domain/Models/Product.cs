using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Product : Entity
    {


        public Product(Guid id, string name, string code, string category)
        {
            Id = id;
            Name = name;
            Category = category;
            Code = code;
        }

        protected Product() { }

        public string Name { get; private set; }

        public string Category { get; private set; }

        public string Code { get; private set; }
    }
}
