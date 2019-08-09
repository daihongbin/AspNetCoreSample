using System;

namespace DependencyInjectionSample.Services
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton, IOperationSingletonInstance
    {
        public Guid OperationId { get; private set; }

        public Operation() : this(Guid.NewGuid())
        {

        }

        public Operation(Guid id)
        {
            OperationId = id;
        }
    }
}
