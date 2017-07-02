using Zzz.Core.Contracts.Services;
using Plugin.Connectivity;

namespace Zzz.Core.Services.General
{
    public class ConnectionService : IConnectionService
    {
        public bool CheckOnline()
        {
            return CrossConnectivity.Current.IsConnected(1);
        }
    }
}
