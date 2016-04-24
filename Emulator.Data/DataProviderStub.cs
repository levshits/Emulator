using Common.Logging;
using Emulator.Common.Interfaces;

namespace Emulator.Data
{
    public class DataProviderStub: IDataProvider
    {
        private static readonly ILog Log = LogManager.GetLogger<IDataProvider>();
        public void Reset()
        {
            Log.Debug("Reset data implemented");
        }
    }
}