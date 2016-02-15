using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace SimpleStatefulService
{
    /// <summary>
    /// Be aware that you have to implement IService for service communication
    /// </summary>
    public interface ISimpleStatefulService : IService
    {
        Task<IEnumerable<string>> GetStrings();
        Task SetString(string value);
    }
}