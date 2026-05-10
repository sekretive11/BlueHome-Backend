using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
    }
}
