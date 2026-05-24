using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _provider;

        public DomainEventDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
        {
            Console.WriteLine("DISPATCH CALLED");

            foreach (var domainEvent in events)
            {
                Console.WriteLine($"EVENT: {domainEvent.GetType().Name}");

                var eventType = domainEvent.GetType();
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
                var handlers = _provider.GetServices(handlerType);

                foreach (var handler in handlers)
                {

                    var method = handlerType.GetMethod("HandleAsync");

                    if (method == null)
                    {
                        continue;
                    }

                    await (Task)method.Invoke(handler, new object[] { domainEvent })!;
                }
            }
        }
    }
}
