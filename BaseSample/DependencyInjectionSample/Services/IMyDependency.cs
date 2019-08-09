using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public interface IMyDependency
    {
         Task WriteMessage(string message);
    }
}