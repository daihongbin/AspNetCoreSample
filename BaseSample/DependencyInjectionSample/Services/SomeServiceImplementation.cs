using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class Service1 : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Service1注销");
        }
    }

    public class Service2 : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Service2注销");
        }
    }

    public class Service3 : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Service3注销");
        }
    }

    public interface ISomeService
    {

    }

    public class SomeServiceImplementation : ISomeService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("SomeServiceImplementation注销");
        }
    }
}
