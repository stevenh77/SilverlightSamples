using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NLog;

namespace Stocks.Service
{
    internal class AssemblyInit
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public AssemblyInit()
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(
                Assembly.GetExecutingAssembly().Location);

            logger.Info("Init: {0} {1} ",
                        Assembly.GetExecutingAssembly().GetName(),
                        fvi.ProductVersion);
        }
    }
}